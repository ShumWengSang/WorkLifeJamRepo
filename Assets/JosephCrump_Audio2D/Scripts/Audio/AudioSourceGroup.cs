/*******************************************************************************
File:      AudioSourceGroup.cs
Author:    Joseph Crump
DP Email:  joseph.crump@digipen.edu
Date:      5/28/2021
Course:    DES302
Section:   A

Description:
    A collection of audio sources that can be used interchangeably. Allows for
    sounds with the same tag to have variations.
        
    © 2021 DigiPen Institute of Technology, all rights reserved

*******************************************************************************/
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioSourceGroup
{
    public List<AudioSource> Sources = new List<AudioSource>();
    public int Count { get { return Sources.Count; } }

    /// <summary>
    /// Indexer to get AudioSources.
    /// </summary>
    public AudioSource this[int i]
    {
        get
        {
            if (i < 0 || i >= Sources.Count)
                return null;

            return Sources[i];
        }
        set
        {
            if (i < 0 || i >= Sources.Count)
                return;

            Sources[i] = value;
        }
    }

    public AudioSource GetRandom()
    {
        int random = Random.Range(0, Sources.Count);
        return this[random];
    }

    public void Add(AudioSource item)
    {
        Sources.Add(item);
    }

    public void Remove(AudioSource item)
    {
        Sources.Remove(item);
    }

    public void Clear()
    {
        Sources.Clear();
    }

    public bool Contains(AudioSource item)
    {
        return Sources.Contains(item);
    }
}
