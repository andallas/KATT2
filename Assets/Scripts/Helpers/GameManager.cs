using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPaused { get { return _isPaused; } }
    public bool levelActive { get { return _levelActive; } }
    public int score { get { return _score; } }
    public int highScore { get { return _highScore; } }
    public int extraLives { get { return _extraLives; } }

    public static GameManager Instance { get { return _instance; } }

    private static GameManager _instance = null;
    private bool _isPaused = false;
    private bool _levelActive = true;
    private int _score = 0;
    private int _highScore = 0;
    private int _extraLives = 3;
    private GameObject playerObject;
    private Player player;
    private GameObject UIManager;
    private GameObject respawnPanel;
    private GameObject gameOverPanel;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnLevelWasLoaded(int level)
    {
        if(level > 1)
        {
            InitObjectReferences();
        }
    }

    public void LoseLife()
    {
        _levelActive = false;
        _extraLives -= 1;
        player.DeathSequence();
        if (_extraLives < 0)
        {
            _extraLives = 0;
            gameOverPanel.SetActive(true);
            return;
        }
        respawnPanel.SetActive(true);
    }

    public void RespawnPlayer()
    {
        _levelActive = true;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Pause()
    {
        _isPaused = !_isPaused;
    }

    public void ResetGame()
    {
        if(_score > _highScore)
        {
            _highScore = _score;
        }
        _score = 0;
        _extraLives = 3;
        _isPaused = false;
        _levelActive = true;
        InitObjectReferences();
        Application.LoadLevel(1);
    }

    private void InitObjectReferences()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
        
        UIManager = GameObject.Find("~UIManager");
        respawnPanel = RecursiveFind(UIManager.transform, "Respawn Panel").gameObject;
        gameOverPanel = RecursiveFind(UIManager.transform, "Game Over Panel").gameObject;
    }

    private Transform RecursiveFind(Transform parent, string name)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            if (child.gameObject.name == name)
            {
                return child;
            }

            if (child.childCount != 0)
            {
                child = RecursiveFind(child, name);
            }

            if (child && child.gameObject.name == name)
            {
                return child;
            }
        }

        return null;
    }
}