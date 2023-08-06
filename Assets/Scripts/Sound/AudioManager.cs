using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] sounds;
    public AudioSource soundSource;

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
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound cannot found!");
        }
        else
        {
            soundSource.clip = s.clip;
            soundSource.Play();
        }
    }
}
