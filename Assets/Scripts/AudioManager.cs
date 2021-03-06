using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public Sound[] levelMusic;
    public static AudioManager instance;

    void Awake()
    {
        //singleton design pattern ensures only one instance of audio manager
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //makes sure the audio manager persists between scenes
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            setSounds(s);
        }

        foreach (Sound s in levelMusic)
        {
            setSounds(s);
        }
    }

    void setSounds(Sound s)
    {
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
    }

    void Start()
    {
        //Play("Music");
        levelMusic[SceneManager.GetActiveScene().buildIndex].source.Play();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        getSound(name).source.Stop();

    }

    public void Pause(string name)
    {
        getSound(name).source.Pause();

    }

    public Sound getSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        return s;
    }

    public void stopAll()
    {
        foreach (Sound s in levelMusic)
        {
            s.source.Stop();
        }

    }
}
