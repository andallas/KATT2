using UnityEngine;
using UnityEngine.UI;

public class ToggleInitialization : MonoBehaviour
{
    private Toggle toggle;

    void Awake()
    {
        toggle = gameObject.GetComponent<Toggle>();
    }

    void Start()
    {
        switch (toggle.gameObject.name)
        {
            case "Mute Toggle":
                toggle.isOn = AudioManager.Instance.isMuted;
            break;
            default:
                Debug.LogError("ERROR: Unrecognized Toggle name!");
            break;
        }
    }
}