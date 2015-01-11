using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Helper;

public class StoreMenuManager : MonoBehaviour
{
    public GameObject currentPanel { get { return _currentPanel; } set { _currentPanel = value; } }
    public List<GameObject> menuList, upgradesList, weaponsList;
    public List<Sprite> buttons, stars;

    private GameObject _currentPanel;
    private Dictionary<string, GameObject> menuPanels, upgrades, weapons;

    void Awake()
    {
        menuPanels = new Dictionary<string, GameObject>();
        upgrades = new Dictionary<string, GameObject>();
        weapons = new Dictionary<string, GameObject>();

        int menuLength = menuList.Count;
        for (int i = 0; i < menuLength; i++)
        {
            menuList[i].SetActive(false);
            menuPanels.Add(menuList[i].name, menuList[i]);
        }

        int upgradesLength = upgradesList.Count;
        for (int j = 0; j < upgradesLength; j++)
        {
            upgrades.Add(upgradesList[j].name.Replace(" Upgrade", ""), upgradesList[j]);
        }

        int weaponsLength = weaponsList.Count;
        for (int k = 0; k < weaponsLength; k++)
        {
            weapons.Add(weaponsList[k].name, weaponsList[k]);
        }

        currentPanel = menuList[0];
    }

    void OnEnable()
    {
        foreach (KeyValuePair<string, GameObject> upgrade in upgrades)
        {
            string itemName = upgrade.Value.transform.GetChild(1).GetComponent<Text>().text;
            upgrade.Value.transform.GetChild(2).GetComponent<Text>().text = GameManager.Instance.GetItem(itemName).description;
            upgrade.Value.transform.GetChild(3).GetComponent<Text>().text = "$" + GameManager.Instance.GetItem(itemName).price;
        }

        foreach (KeyValuePair<string, GameObject> weapon in weapons)
        {
            string itemName = weapon.Value.transform.GetChild(1).GetComponent<Text>().text;
            weapon.Value.transform.GetChild(2).GetComponent<Text>().text = GameManager.Instance.GetItem(itemName).description;
            weapon.Value.transform.GetChild(3).GetComponent<Text>().text = "$" + GameManager.Instance.GetItem(itemName).price;
        }
        ActivateCurrentPanel(currentPanel.name);
    }

    public void SwitchPanel(string panel)
    {
        currentPanel.SetActive(false);
        ActivateCurrentPanel(panel);
    }

    public void PlayGame()
    {
        Application.LoadLevel(GameManager.Instance.currentLevel + 1);
    }

    public void PurchaseUpgrade(string upgrade)
    {
        if(GameManager.Instance.CanAffordItem(upgrade))
        {
            GameManager.Instance.PurchaseItem(upgrade);
            UpdateAllPurchaseButtons(upgrades);
            UpdatePurchasePrice(upgrade, upgrades);
            UpdatePurchaseLevel(upgrade, upgrades);
        }
    }

    public void PurchaseWeapon(string weapon)
    {
        if (GameManager.Instance.CanAffordItem(weapon))
        {
            GameManager.Instance.PurchaseItem(weapon);
            UpdateAllPurchaseButtons(weapons);
            UpdatePurchasePrice(weapon, weapons);
            UpdatePurchaseLevel(weapon, weapons);
        }
    }

    private GameObject GetPanel(string menu)
    {
        return menuPanels[menu];
    }

    private void ActivateCurrentPanel(string panel)
    {
        currentPanel = GetPanel(panel);
        switch(panel)
        {
            case "Upgrades Panel":
                UpdateAllPurchaseButtons(upgrades);
                break;
            case "Weapons Panel":
                UpdateAllPurchaseButtons(weapons);
                break;
            case "Missions Panel":
                Debug.LogWarning("Mission panel not implemented yet");
                break;
            default:
                Debug.LogError("Invalid panel: " + panel);
                break;
        }
        currentPanel.SetActive(true);
    }

    private void UpdateAllPurchaseButtons(Dictionary<string, GameObject> gameObjects)
    {
        foreach (KeyValuePair<string, GameObject> go in gameObjects)
        {
            if (GameManager.Instance.GetItem(go.Key).level >= 3)
            {
                go.Value.transform.GetChild(0).GetComponent<Image>().sprite = buttons[1];
                go.Value.transform.GetChild(0).GetComponent<Button>().interactable = false;
            }
            else
            {
                if (GameManager.Instance.CanAffordItem(go.Key))
                {
                    go.Value.transform.GetChild(0).GetComponent<Image>().sprite = buttons[0];
                    go.Value.transform.GetChild(0).GetComponent<Button>().interactable = true;
                }
                else
                {
                    go.Value.transform.GetChild(0).GetComponent<Image>().sprite = buttons[1];
                    go.Value.transform.GetChild(0).GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    private void UpdatePurchasePrice(string itemName, Dictionary<string, GameObject> items)
    {
        string priceText = "$";

        Item item = GameManager.Instance.GetItem(itemName);
        if(item.level >= 3)
        {
            priceText = "Sold Out";
        }
        else
        {
            priceText += item.price;
        }
        items[itemName].transform.GetChild(3).GetComponent<Text>().text = priceText;
    }

    private void UpdatePurchaseLevel(string itemName, Dictionary<string, GameObject> items)
    {
        Item item = GameManager.Instance.GetItem(itemName);
        GameObject star = items[itemName].transform.GetChild(4).gameObject;
        if(item.level > 0)
        {
            star.SetActive(true);
            star.GetComponent<Image>().sprite = stars[item.level - 1];
        }
        else
        {
            star.GetComponent<Image>().sprite = stars[item.level];
        }
    }
}