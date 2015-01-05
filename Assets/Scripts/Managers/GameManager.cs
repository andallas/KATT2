using UnityEngine;
using SimpleJSON;
using System;
using System.IO;

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

    private JSONClass saveData;
    private const string VERSION = "v0.0.2";
    private string savePath;

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

        savePath = Application.persistentDataPath + "/data.katt";
        Build();
    }

    void Start()
    {
        Load();
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

    public void AddInvulnerability(Pickup.PickupType type)
    {
        switch (type)
        {
            case Pickup.PickupType.InvulnerabilityBronze:
                player.Invulnerable(5f);
            break;
            case Pickup.PickupType.InvulnerabilitySilver:
                player.Invulnerable(10f);
            break;
            case Pickup.PickupType.InvulnerabilityGold:
                player.Invulnerable(15f);
            break;
            default:
                Debug.LogError("Wrong pickup type sent, should be an Invulnerable powerup type.");
            break;
        }
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

    public void Save()
    {
        saveData = new JSONClass();
        saveData["version"] = VERSION;
        saveData["playerSettings"]["highScore"].AsInt = _highScore;
        saveData["audioSettings"]["isMuted"].AsBool = AudioManager.Instance.isMuted;
        saveData["audioSettings"]["masterVolume"].AsFloat = AudioManager.Instance.masterVolume;
        saveData["audioSettings"]["sfxVolume"].AsFloat = AudioManager.Instance.sfxVolume;
        saveData["audioSettings"]["bgmVolume"].AsFloat = AudioManager.Instance.bgmVolume;

        SaveToFile();
    }

    public void Load()
    {
        saveData = (JSONClass)JSONNode.Parse(LoadFromFile());
        
        if (saveData["version"].ToString() == VERSION)
        {
            _highScore = saveData["playerSettings"]["highScore"].AsInt;
            AudioManager.Instance.isMuted = saveData["audioSettings"]["isMuted"].AsBool;
            AudioManager.Instance.masterVolume = saveData["audioSettings"]["masterVolume"].AsFloat;
            AudioManager.Instance.sfxVolume = saveData["audioSettings"]["sfxVolume"].AsFloat;
            AudioManager.Instance.bgmVolume = saveData["audioSettings"]["bgmVolume"].AsFloat;
        }
        else
        {
            Debug.LogError("Version mismatch, cannot load file from version: " + saveData["version"] + " Current Version: " + VERSION);
        }
    }

    private void SaveToFile()
    {
        using (FileStream fs = new FileStream(savePath, FileMode.Create))
        {
            BinaryWriter fileWriter = new BinaryWriter(fs);
            fileWriter.Write(saveData.ToString(""));
            fs.Close();
        }
    }

    private string LoadFromFile()
    {
        string data = "";

        using (FileStream fs = new FileStream(savePath, FileMode.Open))
        {
            BinaryReader fileReader = new BinaryReader(fs);
            data = fileReader.ReadString();
            fs.Close();
        }

        return data;
    }

    private void Build()
    {
        saveData = new JSONClass();
        saveData["version"] = "v0.0.2";
        saveData["playerSettings"]["highScore"] = "";
        saveData["audioSettings"]["isMuted"] = "";
        saveData["audioSettings"]["masterVolume"] = "";
        saveData["audioSettings"]["sfxVolume"] = "";
        saveData["audioSettings"]["bgmVolume"] = "";

        SaveToFile();
    }

    private void InitObjectReferences()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        _player = playerObject.GetComponent<Player>();
    }
}