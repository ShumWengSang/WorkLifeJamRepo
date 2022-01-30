/*******************************************************************************
File:      AudioEntryVariation.cs
Author:    Joseph Crump
DP Email:  joseph.crump@digipen.edu
Date:      5/28/2021
Course:    DES302
Section:   A

Description:
    Contains serializable details for a single audio source that can be 
    created at runtime. An AudioEntry can have any number of these.
    
    © 2021 DigiPen Institute of Technology, all rights reserved

*******************************************************************************/
using UnityEngine;

[System.Serializable]
public class AudioEntryVariation
{
    public AudioClip Clip;

    private AudioSource Source;

    //Adds an AudioSource component to parent and returns the AudioSource
    public AudioSource CreateAudioSource(GameObject parent, AudioEntry entry)
    {
        AudioSource source = parent.AddComponent<AudioSource>();
        source.clip = Clip;
        source.outputAudioMixerGroup = entry.Mixer;
        source.volume = entry.BaseVolume;
        source.pitch = entry.BasePitch;
        source.panStereo = entry.StereoPan;
        source.loop = entry.Loop;
        source.playOnAwake = entry.PlayOnAwake;

        Source = source;

        return source;
    }

    public AudioSource GetSource()
    {
        return Source;
    }
}
