using UnityEngine;
using UnityEngine.UI;

public class MusicTrackHelper : MonoBehaviour
{
    private string currentTrack;
    private Text nowPlaying;

    void Start()
    {
        currentTrack = AudioManager.Instance.currentTrack;
        nowPlaying = gameObject.GetComponent<Text>();
        nowPlaying.text = currentTrack;
    }

    void Update()
    {
        if (currentTrack != AudioManager.Instance.currentTrack)
        {
            currentTrack = AudioManager.Instance.currentTrack;
            nowPlaying.text = currentTrack;
        }
    }
}