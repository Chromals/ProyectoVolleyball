using System;
using UnityEngine;

[Serializable]
public class Sound 
{
    [SerializeField]
    private string name;

    [SerializeField]
    AudioClip audio;

    public string GetName()
    {
        return name;
    }

    public AudioClip GetAudio()
    {
        return audio;
    }
}
