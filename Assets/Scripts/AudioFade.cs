using UnityEngine;
using System.Collections;

public static class AudioFade {

    public static IEnumerator FadeOut(Sound sound, float coeff) {

        while (sound.source.volume > 0) {

            sound.source.volume -= sound.source.volume * Time.deltaTime * coeff;

            yield return null;
        }

        sound.source.volume = 0f;

        sound.source.Stop();
    }

    public static IEnumerator FadeIn(Sound sound, float coeff) {

        sound.source.Play();

        sound.source.volume = 0.005f;

        while (sound.source.volume < sound.volume) {
            sound.source.volume += sound.source.volume * Time.deltaTime * coeff;

			if (sound.source.volume > sound.volume) {
                sound.source.volume = sound.volume;
            }

            yield return null;
        }
    }

}
