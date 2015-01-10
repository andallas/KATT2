using UnityEngine;
using System.Collections.Generic;

public class StoreMenuManager : MonoBehaviour
{
    public GameObject currentPanel { get { return _currentPanel; } set { _currentPanel = value; } }
    public List<GameObject> menuList;

    private GameObject _currentPanel;
    private Dictionary<string, GameObject> menuPanels;

    void Awake()
    {
        menuPanels = new Dictionary<string, GameObject>();

        int length = menuList.Count;
        for (int i = 0; i < length; i++)
        {
            menuList[i].SetActive(false);
            menuPanels.Add(menuList[i].name, menuList[i]);
        }

        currentPanel = menuList[0];
    }

    public void SwitchPanel(string panel)
    {
        currentPanel.SetActive(false);
        currentPanel = GetPanel(panel);
        currentPanel.SetActive(true);
    }

    public GameObject GetPanel(string menu)
    {
        return menuPanels[menu];
    }
}