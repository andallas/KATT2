using UnityEngine;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    public float masterVolume { set { AudioManager.Instance.masterVolume = value; } }
    public float sfxVolume { set { AudioManager.Instance.sfxVolume = value; } }
    public float bgmVolume { set { AudioManager.Instance.bgmVolume = value; } }

    public GameObject currentPanel { get { return _currentPanel; } set { _currentPanel = value; } }
    public List<GameObject> menuList;

    private GameObject _currentPanel;
    [SerializeField]
    private GameObject _HUD;
    private Dictionary<string, GameObject> menuPanels;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnLevelWasLoaded(int level)
    {
        CloseAllMenus();
        if(level == 1)
        {
            _HUD.SetActive(false);
            currentPanel = GetPanel("Main Panel");
            currentPanel.SetActive(true);
        }
        else if(level > 1)
        {
            _HUD.SetActive(true);
        }
    }

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
    }

    public GameObject GetPanel(string menu)
    {
        return menuPanels[menu];
    }

	public void StartGame()
    {
        Application.LoadLevel("Level_01");
    }

    public void SwitchMenu(string menu)
    {
        currentPanel.SetActive(false);
        currentPanel = GetPanel(menu);
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