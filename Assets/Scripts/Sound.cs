using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;

    public AudioMixerGroup mixerGroup;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
