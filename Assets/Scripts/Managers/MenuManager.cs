using UnityEngine;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    public HeadsUpDisplay HUD { get { return _HUD; } }

    public GameObject currentPanel { get { return _currentPanel; } set { _currentPanel = value; } }
    public List<GameObject> menuList;
    public GameObject HUDObject;
    public static MenuManager Instance { get { return _instance; } }

    private float masterVolume { set { AudioManager.Instance.masterVolume = value; } }
    private float sfxVolume { set { AudioManager.Instance.sfxVolume = value; } }
    private float bgmVolume { set { AudioManager.Instance.bgmVolume = value; } }
    private GameObject _currentPanel;
    private Dictionary<string, GameObject> menuPanels;
    private HeadsUpDisplay _HUD;
    private static MenuManager _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        menuPanels = new Dictionary<string, GameObject>();

        int length = menuList.Count;
        for (int i = 0; i < length; i++)
        {
            menuList[i].SetActive(false);
            menuPanels.Add(menuList[i].name, menuList[i]);
        }
        HUDObject.SetActive(false);

        currentPanel = menuList[0];
        _HUD = HUDObject.GetComponent<HeadsUpDisplay>();
    }

    void OnLevelWasLoaded(int level)
    {
        CloseAllMenus();
        if(level == 1) // Main menu
        {
            HUDObject.SetActive(false);
            currentPanel = GetPanel("Main Panel");
            currentPanel.SetActive(true);
        }
        else if(level == 2) // Store
        {
            HUDObject.SetActive(false);
            currentPanel = GetPanel("Store Panel");
            currentPanel.SetActive(true);
        }
        else if(level > 1) // Levels
        {
            HUDObject.SetActive(true);
        }
    }

    public GameObject GetPanel(string menu)
    {
        return menuPanels[menu];
    }

    public void SetPanel(string menu)
    {
        currentPanel = GetPanel(menu);
    }

	public void StartGame()
    {
        Application.LoadLevel(2);
    }

    public void SwitchMenu(string menu)
    {
        currentPanel.SetActive(false);
        currentPanel = GetPanel(menu);
        currentPanel.SetActive(true);
        switch(menu)
        {
            case "Graphics Panel":
            case "Game Panel":
                Debug.LogWarning(menu + " not implemented yet");
                break;
            default: break;
        }
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

    public void CloseOrSwitchToMain()
    {
        if(Application.loadedLevel == 1)
        {
            SwitchMenu("Main Panel");
        }
        else
        {
            CloseAllMenus();
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