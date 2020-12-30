using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    public void PlayMix()
    {
        foreach (Sound s in sounds)
        {
            if (s.enabled == true)
                s.source.Play();
        }
    }

    public void StopMix()
    {
        foreach (Sound s in sounds)
        {
            if (s.enabled == true)
                s.source.Stop();
        }
    }

    public void ToggleTrack(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.enabled = !s.enabled;
    }
}
