using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPaused { get { return _isPaused; } }
    public bool levelActive { get { return _levelActive; } }
    public int score { get { return _score; } }
    public int highScore { get { return _highScore; } }
    public int extraLives { get { return _extraLives; } }
    public Player player { get { return _player; } }

    public static GameManager Instance { get { return _instance; } }

    private static GameManager _instance = null;
    private bool _isPaused = false;
    private bool _levelActive = true;
    private int _score = 0;
    private int _scoreMultiplier = 1;
    private int _highScore = 0;
    private int _extraLives = 3;
    private GameObject playerObject;
    private Player _player;
    private int maxLives = 5;
    private int extraLifeBonus = 10000;

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
            MenuManager.Instance.HUD.SetScore(_score);
            MenuManager.Instance.HUD.SetMultiplier(_scoreMultiplier);
            MenuManager.Instance.HUD.SetLives(_extraLives);
        }
    }

    public void LoseLife()
    {
        _levelActive = false;
        _extraLives -= 1;
        player.DeathSequence();
        ResetMultiplier();
        if (_extraLives < 0)
        {
            _extraLives = 0;
            MenuManager.Instance.SwitchMenu("Game Over Panel");
            return;
        }
        MenuManager.Instance.HUD.SetLives(_extraLives);
        MenuManager.Instance.SwitchMenu("Respawn Panel");
    }

    public void AddLife()
    {
        if(_extraLives >= maxLives)
        {
            AddScore(extraLifeBonus);
            return;
        }
        _extraLives++;
        MenuManager.Instance.HUD.SetLives(_extraLives);
    }

    public void AddScore(int score)
    {
        if(score < 0)
        {
            Debug.LogWarning("Attempted to add a negative amount to score.");
            return;
        }
        _score += score * _scoreMultiplier;
        MenuManager.Instance.HUD.SetScore(_score);
    }

    public void AddMultiplier(int multipler)
    {
        if (multipler < 0)
        {
            Debug.LogWarning("Attempted to add a negative amount to score multiplier.");
            return;
        }
        _scoreMultiplier += multipler;
        MenuManager.Instance.HUD.SetMultiplier(_scoreMultiplier);
    }

    public void ResetMultiplier()
    {
        _scoreMultiplier = 1;
        MenuManager.Instance.HUD.SetMultiplier(_scoreMultiplier);
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
        _scoreMultiplier = 1;
        _extraLives = 3;
        _isPaused = false;
        _levelActive = true;
        InitObjectReferences();
        MenuManager.Instance.CloseAllMenus();
        MenuManager.Instance.SetPanel("Main Panel");

        Application.LoadLevel(1);
    }

    private void InitObjectReferences()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        _player = playerObject.GetComponent<Player>();
    }
}