/*******************************************************************************
File:      AudioManager.cs
Author:    Joseph Crump
DP Email:  joseph.crump@digipen.edu
Date:      
Course:    DES302
Section:   A

Description:
    
    © 2021 DigiPen Institute of Technology, all rights reserved

*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float SoundCrossfadeTime = 1f;

    public List<AudioEntry> AudioEntries = new List<AudioEntry>();
    private Dictionary<AudioEntry, AudioSourceGroup> AudioSources;

    public static AudioManager Instance { get; set; }

    private void Awake()
    {
        VerifyInstance();
        Initialize();
    }

    private void VerifyInstance()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize()
    {
        // Initialize the AudioSources Dictionary
        AudioSources = new Dictionary<AudioEntry, AudioSourceGroup>();

        foreach (var entry in AudioEntries)
        {
            AddEntry(entry);
        }
    }

    private void AddEntry(AudioEntry entry)
    {
        var key = entry;
        var sourceGroup = InitializeEntry(entry);

        AudioSources.Add(key, sourceGroup);

        if (entry.PlayOnAwake)
        {
            PlaySound(entry);
        }
    }

    private AudioSourceGroup InitializeEntry(AudioEntry entry)
    {
        AudioSourceGroup audioSources = new AudioSourceGroup();

        // Create the AudioSource components
        foreach (var variation in entry.Variations)
        {
            var source = variation.CreateAudioSource(gameObject, entry);
            audioSources.Add(source);
        }

        return audioSources;
    }

    public void PlaySound(AudioEntry entry, int index)
    {
        if (entry == null)
            AddEntry(entry);

        AudioSourceGroup sourceGroup = GetAudioSourceGroup(entry);
        if (sourceGroup == null)
            return;

        AudioSource source = sourceGroup[index];

        if (source == null)
        {
            Debug.LogError("Unable to play audio source \"" + entry + "\". Audio source not found.");
            return;
        }

        // Calculate pitch variance
        float pitchDown = entry.RandomPitchDown;
        float pitchUp = entry.RandomPitchUp;
        float pitchVariance = UnityEngine.Random.Range(pitchDown, pitchUp);

        // Modify source pitch
        source.pitch = entry.BasePitch;
        source.pitch += pitchVariance;

        source.Play();
    }

    // Overload method, chooses a random sound
    public void PlaySound(AudioEntry entry)
    {
        int random = UnityEngine.Random.Range(0, entry.Count);
        PlaySound(entry, random);
    }

    public void StopSound(AudioEntry key)
    {
        if (key == null)
            return;

        AudioSourceGroup sourceGroup = GetAudioSourceGroup(key);
        if (sourceGroup == null)
            return;

        // Stop all sources in the group
        foreach (AudioSource source in sourceGroup.Sources)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }
    }

    public void FadeInSound(AudioEntry sound)
    {
        AudioSourceGroup sourceGroup = GetAudioSourceGroup(sound);
        if (sourceGroup == null)
            return;

        float entryVolume = sound.BaseVolume;
        StartCoroutine(FadeIn(sourceGroup.GetRandom(), entryVolume));
    }

    public void FadeOutSound(AudioEntry sound)
    {
        AudioSourceGroup sourceGroup = GetAudioSourceGroup(sound);
        if (sourceGroup == null)
            return;

        foreach (AudioSource source in sourceGroup.Sources)
        {
            StartCoroutine(FadeOut(source));
        }
    }

    private IEnumerator FadeIn(AudioSource source, float entryVolume)
    {
        if (source.isPlaying)
            yield break;

        source.volume = 0f;

        source.Play();
        yield return FadeVolume(source, 0f, entryVolume);
    }

    private IEnumerator FadeOut(AudioSource source)
    {
        if (!source.isPlaying)
            yield break;

        float sourceVolume = source.volume;
        yield return FadeVolume(source, sourceVolume, 0f);
        source.Stop();
    }

    private IEnumerator FadeVolume(AudioSource sound, float current, float target)
    {
        float timer = 0f;
        while (timer <= SoundCrossfadeTime)
        {
            timer += Time.deltaTime;
            float t = timer / SoundCrossfadeTime;

            sound.volume = Mathf.Lerp(current, target, t);
            yield return new WaitForEndOfFrame();
        }
    }

    private AudioSourceGroup GetAudioSourceGroup(AudioEntry key)
    {
        //Get the AudioSource from the Dictionary
        AudioSourceGroup sourceGroup;
        AudioSources.TryGetValue(key, out sourceGroup);

        if (sourceGroup == null)
        {
            Debug.LogError("Unable to stop audio source group with key \"" + key + "\". Key not found.");
            return null;
        }

        return sourceGroup;
    }
}
