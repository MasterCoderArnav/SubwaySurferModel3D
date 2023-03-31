using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public sound[] sounds;
    private void Start()
    {
        foreach(sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
        playSound(sounds[0].name);
    }
    public void playSound(string name)
    {
        foreach (sound s in sounds)
        {
            if (s.name == name)
            {
                s.source.Play();
            }
        }
    }
    public void stopSound(string name)
    {
        foreach(sound s in sounds)
        {
            if (s.name == name)
            {
                s.source.Stop();
            }
        }
    }
}
