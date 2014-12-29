using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public GameObject graphicsPanel;
    public GameObject audioPanel;
    public GameObject gamePanel;

    private GameObject currentPanel;

    void Start()
    {
        currentPanel = mainMenuPanel;
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        graphicsPanel.SetActive(false);
        audioPanel.SetActive(false);
        gamePanel.SetActive(false);
    }

	public void StartGame()
    {
        Application.LoadLevel("Level_01");
    }

    public void SwitchMenu(string menu)
    {
        currentPanel.SetActive(false);
        switch(menu)
        {
            case "mainMenu":
                currentPanel = mainMenuPanel;
            break;
            case "options":
                currentPanel = optionsPanel;
            break;
            case "graphics":
                currentPanel = graphicsPanel;
            break;
            case "audio":
                currentPanel = audioPanel;
            break;
            case "game":
                currentPanel = gamePanel;
            break;
            default:
                currentPanel = mainMenuPanel;
            break;
        }
        currentPanel.SetActive(true);
    }

}