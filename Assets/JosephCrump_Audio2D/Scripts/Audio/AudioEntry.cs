/*******************************************************************************
File:      AudioEntry.cs
Author:    Joseph Crump
DP Email:  joseph.crump@digipen.edu
Date:      5/28/2021
Course:    DES302
Section:   A

Description:
    Contains serializable details that allows audio assets to be quickly created
    and put to use in the editor.

    © 2021 DigiPen Institute of Technology, all rights reserved

*******************************************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioEntry", menuName = "Audio/AudioEntry")]
public class AudioEntry : ScriptableObject
{
    [Range(0f, 1f)]
    public float BaseVolume = 1f;
    [Range(-3f, 3f)]
    public float BasePitch = 1f;
    [Range(-1f, 1f)]
    public float StereoPan = 0f;

    [Range(-3f, 0f)]
    public float RandomPitchDown = 0f;
    [Range(0f, 3f)]
    public float RandomPitchUp = 0f;

    public bool PlayOnAwake = false;
    public bool Loop = false;
    public AudioMixerGroup Mixer;

    public List<AudioEntryVariation> Variations = new List<AudioEntryVariation>();
    public int Count { get { return Variations.Count; } }

    public AudioEntryVariation this[int i]
    {
        get
        {
            if (i < 0 || i >= Variations.Count)
                return null;

            return Variations[i];
        }
        set
        {
            if (i < 0 || i >= Variations.Count)
                return;

            Variations[i] = value;
        }
    }

    public AudioEntryVariation Random()
    {
        int random = UnityEngine.Random.Range(0, Count);
        return this[random];
    }
}
