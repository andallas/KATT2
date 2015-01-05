using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class AudioList
    {
        public string title;
        public AudioClip clip;
    }

    public static AudioManager Instance { get { return _instance; } }
    public List<AudioList> SFX;
    public List<AudioList> BGM;
    public string currentTrack;
    public bool shuffle;
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
    public bool isMuted { get { return _isMuted; } }

    private static AudioManager _instance = null;
    private AudioSource source1;
    private AudioSource source2;
    private bool source1Active = true;
    private bool _isMuted = false;
    private float fadeTime = 2.0f;
    private bool isFading = false;
    private float _masterVolume = 1.0f;
    private float _sfxVolume = 1.0f;
    private float _bgmVolume = 1.0f;

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

        source1 = gameObject.AddComponent<AudioSource>();
        source2 = gameObject.AddComponent<AudioSource>();
        PlayBGM(BGM[0].title);
        Mute();
    }

    void Update()
    {
        if(!GameManager.Instance.isPaused)
        {
            if(source1Active)
            {
                if(source1.time >= source1.clip.length)
                {
                    if(shuffle)
                    {
                        PlayRandomTrack();
                    }
                    else
                    {
                        PlayNextTrack();
                    }
                }
            }
            else
            {
                if (source2.time >= source2.clip.length)
                {
                    if (shuffle)
                    {
                        PlayRandomTrack();
                    }
                    else
                    {
                        PlayNextTrack();
                    }
                }
            }
        }
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
                
                StartCoroutine(CrossFade(source1, source2, track.clip));
            }
            else
            {
                source1.clip = track.clip;
                source1.volume = bgmVolume * masterVolume;
                source1.Play();
            }
        }
        else
        {
            currentTrack = track.title;
            if (source2.isPlaying)
            {
                StartCoroutine(CrossFade(source2, source1, track.clip));
            }
            else
            {
                source2.clip = track.clip;
                source2.volume = bgmVolume * masterVolume;
                source2.Play();
            }
        }
    }

    public void PlayNextTrack()
    {
        PlayBGM(GetNextTrack().title);
    }

    public void PlayLastTrack()
    {
        PlayBGM(GetLastPlayedTrack().title);
    }

    public void GoBackOneTrack()
    {
        PlayBGM(GetLastTrack().title);
    }

    public void PlayRandomTrack()
    {
        PlayBGM(GetRandomTrack().title);
    }

    public void Pause()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    public void Mute()
    {
        _isMuted = !_isMuted;
        AudioListener.volume = _isMuted ? 0.0f : masterVolume;
    }

    private IEnumerator CrossFade(AudioSource oldSource, AudioSource newSource, AudioClip clip)
    {
        if (isFading)
        {
            yield return new WaitForSeconds(fadeTime);
            StartCoroutine(Fade(oldSource, newSource, clip));
        }
        else
        {
            StartCoroutine(Fade(oldSource, newSource, clip));
        }
    }

    private IEnumerator Fade(AudioSource oldSource, AudioSource newSource, AudioClip clip)
    {
        isFading = true;
        newSource.volume = 0.0f;
        newSource.clip = clip;
        newSource.Play();

        float requestedVolume = bgmVolume * masterVolume;
        float fadeLength = 0.0f;
        while (fadeLength < 1.0f)
        {
            fadeLength += Time.deltaTime / fadeTime;
            newSource.volume = requestedVolume * fadeLength;
            oldSource.volume = oldSource.volume * (1.0f - fadeLength);
            yield return new WaitForFixedUpdate();
        }
        source1Active = !source1Active;
        newSource.volume = requestedVolume;
        oldSource.Stop();
        isFading = false;
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

    private AudioList GetNextTrack()
    {
        int index = BGM.FindIndex(a => a.title == currentTrack) + 1;
        index = index >= BGM.Count ? 0 : index;
        return BGM[index];
    }

    private AudioList GetLastPlayedTrack()
    {
        int index;
        if(source2.clip && source1Active)
        {
            index = BGM.FindIndex(a => a.title == source2.clip.name);
        }
        else
        {
            index = BGM.FindIndex(a => a.title == source1.clip.name);
        }
        index = index < 0 ? BGM.Count - 1 : index;
        return BGM[index];
    }

    private AudioList GetLastTrack()
    {
        int index = BGM.FindIndex(a => a.title == currentTrack) - 1;
        index = index < 0 ? BGM.Count - 1 : index;
        return BGM[index];
    }

    private AudioList GetRandomTrack()
    {
        int index = Random.Range(0, BGM.Count);
        return BGM[index];
    }
}