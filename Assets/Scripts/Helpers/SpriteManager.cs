using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public Sprite[] sprites;
    public static SpriteManager Instance { get { return _instance; } }
    
    private string[] spriteNames;
    private static SpriteManager _instance;

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

        int count = sprites.Length;
        spriteNames = new string[count];
        
        for(int i = 0; i < count; i++)
        {
            spriteNames[i] = sprites[i].name;
        }
    }

    public Sprite GetSprite(string name)
    {
        return sprites[System.Array.IndexOf(spriteNames, name)];
    }
}