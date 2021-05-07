using UnityEngine;
using System.Collections;
using System;

// https://stackoverflow.com/questions/57527257/audio-fade-in-out-with-c-sharp-in-unity

public class AudioFade
{
    public static IEnumerator FadeOut(Sound sound, float fadingTime, Func<float, float, float, float> Interpolate)
    {
        var length = sound.clip.length;
        float startVolume = sound.source.volume;
        float frameCount = fadingTime / Time.deltaTime;
        float framesPassed = 0;

        sound.fadingOut = true;
        while (framesPassed <= frameCount)
        {
            var t = framesPassed++ / frameCount;
            sound.source.volume = Interpolate(startVolume, 0, t);
            yield return null;
        }

        sound.source.volume = 0;
        
        sound.fadingOut = false;
        if (!sound.source.loop)
        {
            sound.source.Stop();
            Debug.Log("stop music");
            yield break;
        }
        sound.source.Pause();
        yield return new WaitForSeconds(fadingTime);
        yield return AudioFade.FadeIn(sound, fadingTime, Mathf.SmoothStep);
    }
    public static IEnumerator FadeIn(Sound sound, float fadingTime, Func<float, float, float, float> Interpolate)
    {
        var length = sound.clip.length;
        sound.source.Play();
        sound.source.volume = 0;

        float resultVolume = sound.volume;
        float frameCount = fadingTime / Time.deltaTime;
        float framesPassed = 0;
        sound.fadingIn = true;
        while (framesPassed <= frameCount)
        {
            var t = framesPassed++ / frameCount;
            sound.source.volume = Interpolate(0, resultVolume, t);
            yield return null;
        }

        sound.source.volume = resultVolume;
        sound.fadingIn = false;
        if (!sound.source.loop) yield break;

        yield return new WaitForSeconds(Mathf.Max(0, length - fadingTime * 2));
        if (sound.source.isPlaying)
        {
            yield return AudioFade.FadeOut(sound, fadingTime, Mathf.SmoothStep);
        }
    }
}