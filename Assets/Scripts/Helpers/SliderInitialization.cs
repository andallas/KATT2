using UnityEngine;
using UnityEngine.UI;

public class SliderInitialization : MonoBehaviour
{
    private Slider slider;

    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    void Start()
    {
        switch(slider.gameObject.name)
        {
            case "Master Volume":
                slider.normalizedValue = AudioManager.Instance.masterVolume;
            break;
            case "SFX Volume":
                slider.normalizedValue = AudioManager.Instance.sfxVolume;
            break;
            case "BGM Volume":
                slider.normalizedValue = AudioManager.Instance.bgmVolume;
            break;
            default:
                Debug.LogError("ERROR: Unrecognized Slider name!");
            break;
        }
    }
}