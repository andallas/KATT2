using UnityEngine;
using UnityEngine.UI;

public class TrackText : MonoBehaviour
{
    private string currentTrack;
    private Text nowPlaying;

    void Start()
    {
        currentTrack = AudioManager.Instance.currentTrack;
        nowPlaying = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        string curTrack = AudioManager.Instance.currentTrack;
        if(currentTrack != curTrack)
        {
            currentTrack = curTrack;
            nowPlaying.text = currentTrack;
        }
    }
}