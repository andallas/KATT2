using UnityEngine;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    public float masterVolume { set { AudioManager.Instance.masterVolume = value; } }
    public float sfxVolume { set { AudioManager.Instance.sfxVolume = value; } }
    public float bgmVolume { set { AudioManager.Instance.bgmVolume = value; } }
    
    public List<GameObject> menuList;
    public bool startOpen = true;

    private GameObject currentPanel;
    private Dictionary<string, GameObject> menuPanels;

    void Start()
    {
        menuPanels = new Dictionary<string, GameObject>();

        int length = menuList.Count;
        for (int i = 0; i < length; i++)
        {
            menuList[i].SetActive(false);
            menuPanels.Add(menuList[i].name, menuList[i]);
        }

        currentPanel = menuList[0];

        if (startOpen)
        {
            currentPanel.SetActive(true);
        }
    }

    public GameObject GetCurrentPanel()
    {
        return currentPanel;
    }

	public void StartGame()
    {
        Application.LoadLevel("Level_01");
    }

    public void SwitchMenu(string menu)
    {
        currentPanel.SetActive(false);
        currentPanel = menuPanels[menu];
        currentPanel.SetActive(true);
    }

    public void CloseAllMenus()
    {
        if(GameManager.Instance.isPaused)
        {
            GameManager.Instance.Pause();
        }

        foreach(KeyValuePair<string, GameObject> panel in menuPanels)
        {
            panel.Value.SetActive(false);
        }
    }

    public void Respawn()
    {
        GameManager.Instance.RespawnPlayer();
    }

    public void QuitToMainMenu()
    {
        GameManager.Instance.ResetGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Audio
    public void Mute()
    {
        AudioManager.Instance.Mute();
    }

    public void GoBackOneTrack()
    {
        AudioManager.Instance.GoBackOneTrack();
    }

    public void PlayNextTrack()
    {
        AudioManager.Instance.PlayNextTrack();
    }
}