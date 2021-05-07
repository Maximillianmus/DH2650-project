using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AudioManager : MonoBehaviour
{
    public float fadingTime = 1.5f;
    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;
    public List<Sound> playingSounds = new List<Sound>();

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    public void Play(string sound, bool exclusive=true)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        if(!exclusive)
        {
            StartCoroutine(AudioFade.FadeIn(s, fadingTime, Mathf.SmoothStep));
        }
        else
        {
            StartCoroutine(stopAndPlay(s, exclusive));
        }
        playingSounds.Add(s);
    }

    private IEnumerator stopAndPlay(Sound s, bool exclusive)
    {
        if (exclusive)
        {
            for (int i = playingSounds.Count - 1; i >= 0; i--)
            {
                Sound sound = playingSounds[i];
                if (sound == s)
                    continue;
                yield return new WaitUntil(() => !sound.fadingIn && !sound.fadingOut);
                sound.source.loop = false;
                yield return AudioFade.FadeOut(sound, fadingTime, Mathf.SmoothStep);
                yield return new WaitForSeconds(Mathf.Max(0, fadingTime));
                this.playingSounds.RemoveAt(i);
            }
        }
        
        yield return AudioFade.FadeIn(s, fadingTime, Mathf.SmoothStep);
    }
}
