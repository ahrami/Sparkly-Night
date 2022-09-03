using System;
using UnityEngine.Audio;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    private readonly float _fadeCoeff = 5f;

    [SerializeField] private Sound[] _sounds;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
		}

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in _sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
            sound.source.volume = 0f;
            sound.source.outputAudioMixerGroup = sound.mixerGroup;
		}
    }

    public void PlayMenuTheme() {
        FadeIn("Menu", 10f);
    }

    public void PlayTownTheme() {
        FadeOut("Menu", _fadeCoeff);
        FadeIn("Town", _fadeCoeff);
    }

    public void PlayHillTheme() {
        FadeOut("Town", _fadeCoeff);
        FadeIn("Hill", _fadeCoeff);
    }

    public void StopHillTheme() {
        FadeOut("Hill", 1.5f);
    }

    public void PlayLakeTheme() {
        FadeIn("Lake", 10f);
    }

    public void StopLakeTheme() {
        FadeOut("Lake", 2f);
    }

    public void PlayEndTheme() {
        FadeIn("End", 2f);
    }

    public void StopEndTheme() {
        FadeOut("End", 2f);
    }

    private void FadeIn(string name, float coeff) {
        StartCoroutine(AudioFade.FadeIn(Array.Find(_sounds, sound => sound.name == name), coeff));
    }

    private void FadeOut(string name, float coeff) {
        StartCoroutine(AudioFade.FadeOut(Array.Find(_sounds, sound => sound.name == name), coeff));
    }

}
