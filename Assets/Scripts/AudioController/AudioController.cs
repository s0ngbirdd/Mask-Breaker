using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [SerializeField] private bool _isAudioEnabled = true;
    [SerializeField] private bool _isGameLoopEnabled = true;
    [SerializeField] private bool _isSoundEnabled = true;
    [SerializeField] private string _gameLoopName = "GameLoop";
    [SerializeField] private Sound[] _sounds;

    private int _slapCount;
    
    public bool IsSoundEnabled => _isSoundEnabled;
    public bool IsGameLoopEnabled => _isGameLoopEnabled;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound sound in _sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.playOnAwake = sound.playOnAwake;
            sound.source.loop = sound.loop;
            sound.source.volume = sound.volume;
        }
        
        if (_isAudioEnabled && _isGameLoopEnabled)
            Play(_gameLoopName);
    }

    public void Play(string name)
    {
        if (!_isAudioEnabled) return;
        
        Sound snd = Array.Find(_sounds, sound => sound.name == name);
        
        if (snd == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        snd.source.Play();
    }

    public void Stop(string name)
    {
        Sound snd = Array.Find(_sounds, sound => sound.name == name);
        
        if (snd == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        snd.source.Stop();
    }

    public void PlayOneShot(string name)
    {
        if (!_isAudioEnabled) return;
        
        Sound snd = Array.Find(_sounds, sound => sound.name == name);
        
        if (snd == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        snd.currentPlayTime = Time.time;
        
        if (snd.currentPlayTime - snd.lastPlayTime >= 0.001f)
        {
            snd.lastPlayTime = snd.currentPlayTime;
            snd.source.PlayOneShot(snd.clip);
        }
        
        //snd.source.PlayOneShot(snd.clip);
    }

    public AudioSource ReturnAudioSource(string name)
    {
        Sound snd = Array.Find(_sounds, sound => sound.name == name);
        return snd.source;
    }
    
    public void PlaySlapOneShot()
    {
        if (!_isAudioEnabled) return;
        
        Sound snd;// = Array.Find(_sounds, sound => sound.name == name);
        
        /*if (snd == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }*/
        
        if (_slapCount / 2f == 0)
            snd = Array.Find(_sounds, sound => sound.name == "Slap1");
        else
            snd = Array.Find(_sounds, sound => sound.name == "Slap2");
        
        snd.currentPlayTime = Time.time;
        
        if (snd.currentPlayTime - snd.lastPlayTime >= 0.001f)
        {
            snd.lastPlayTime = snd.currentPlayTime;
            snd.source.PlayOneShot(snd.clip);
        }
        
        //snd.source.PlayOneShot(snd.clip);
        
        _slapCount++;
    }
    
    /*public void EnableDisableGameLoopVolume()
    {
        Sound[] sounds = _sounds.Where(item => item.name.Equals(_gameLoopName)).ToArray();
        
        if (_isGameLoopEnabled)
        {
            foreach (Sound sound in sounds)
                sound.source.volume = 0;

            _isGameLoopEnabled = false;
        }
        else if (!_isGameLoopEnabled)
        {
            foreach (Sound sound in sounds)
                sound.source.volume = sound.volume;

            _isGameLoopEnabled = true;
        }
    }

    public void EnableDisableSoundVolume()
    {
        Sound[] sounds = _sounds.Where(item => !item.name.Equals(_gameLoopName)).ToArray();
        
        if (_isSoundEnabled)
        {
            foreach (Sound sound in sounds)
                sound.source.volume = 0;

            _isSoundEnabled = false;
        }
        else if (!_isSoundEnabled)
        {
            foreach (Sound sound in sounds)
                sound.source.volume = sound.volume;

            _isSoundEnabled = true;
        }
    }*/
}
