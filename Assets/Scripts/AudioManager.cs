using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    private bool isThemePlaying;
    private void Awake()
    {
        isThemePlaying = false;
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds) 
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop; 
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();

        if (name == "Theme")
            isThemePlaying = true;
       
    }

    public void Stop()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }

        isThemePlaying= false;
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();

        if (name == "Theme")
            isThemePlaying = false;
    }

    public bool IsThemePlaying() => isThemePlaying;
}
