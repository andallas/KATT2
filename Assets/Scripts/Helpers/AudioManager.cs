using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public struct AudioList
    {
        public string title;
        public AudioClip clip;
    }

    public static AudioManager Instance;
    public List<AudioList> SFX;
    public List<AudioList> BGM;
    public string currentTrack;
    public float masterVolume
    {
        get { return _masterVolume; }
        set
        {
            _masterVolume = value;
            if (source1Active)
            {
                source1.volume = bgmVolume * _masterVolume;
            }
            else
            {
                source2.volume = bgmVolume * _masterVolume;
            }
        }
    }
    public float sfxVolume
    {
        get { return _sfxVolume; }
        set { _sfxVolume = value; }
    }
    public float bgmVolume
    {
        get { return _bgmVolume; }
        set
        {
            _bgmVolume = value;
            if (source1Active)
            {
                source1.volume = _bgmVolume * masterVolume;
            }
            else
            {
                source2.volume = _bgmVolume * masterVolume;
            }
        }
    }

    private AudioSource source1;
    private AudioSource source2;
    private bool source1Active = true;
    private bool isMuted = false;
    private float fadeTime = 2.0f;
    private float _masterVolume = 1.0f;
    private float _sfxVolume = 1.0f;
    private float _bgmVolume = 1.0f;
    
    
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("ERROR: Multiple instances of AudioManager.");
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        source1 = gameObject.AddComponent<AudioSource>();
        source2 = gameObject.AddComponent<AudioSource>();
        PlayBGM(BGM[0].title);
    }

    public void PlaySFX(string clipName)
    {
        foreach(AudioList list in SFX)
        {
            if(list.title == clipName)
            {
                AudioSource.PlayClipAtPoint(list.clip, transform.position, sfxVolume * masterVolume);
                return;
            }
        }
    }

    public void PlayBGM(string clipName)
    {
        AudioList track = GetTrack(clipName);
        if (source1Active)
        {
            currentTrack = track.title;
            if(source1.isPlaying)
            {
                CrossFade(source1, source2, track.clip);
                source1Active = false;
            }
            else
            {
                source1.clip = track.clip;
                source1.volume = bgmVolume * masterVolume;
                source1.Play();
                source1Active = true;
            }
        }
        else
        {
            currentTrack = track.title;
            if (source2.isPlaying)
            {
                CrossFade(source2, source1, track.clip);
            }
            else
            {
                source2.clip = track.clip;
                source2.volume = bgmVolume * masterVolume;
                source2.Play();
            }
        }
    }

    public void Pause()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    public void Mute()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0.0f : masterVolume;
    }

    private AudioList GetTrack(string clipName)
    {
        AudioList track = new AudioList();
        foreach (AudioList list in BGM)
        {
            if (list.title == clipName)
            {
                track = list;
            }
        }
        return track;
    }

    private IEnumerator CrossFade(AudioSource oldSource, AudioSource newSource, AudioClip clip)
    {
        newSource.volume = 0.0f;
        newSource.clip = clip;
        newSource.Play();

        float requestedVolume = bgmVolume * masterVolume;
        float fadeLength = 0.0f;
        while(fadeLength < 1.0f)
        {
            fadeLength += Time.deltaTime / fadeTime;
            newSource.volume = requestedVolume * fadeLength;
            oldSource.volume = oldSource.volume * (1.0f - fadeLength);
            yield return new WaitForFixedUpdate();
        }
        newSource.volume = requestedVolume;
        oldSource.Stop();
    }
}