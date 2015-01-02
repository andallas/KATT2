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

    public Sprite GetSprite(int index)
    {
        if(0 > index || index >= sprites.Length)
        {
            Debug.LogError("ERROR: Invalid sprite index: " + index + " - Returning index 0");
            return sprites[0];
        }
        return sprites[index];
    }
}