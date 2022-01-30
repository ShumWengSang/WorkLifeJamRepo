/*******************************************************************************
File:      AudioFacilitator.cs
Author:    Joseph Crump
DP Email:  joseph.crump@digipen.edu
Date:      5/28/2021
Course:    DES302
Section:   A

Description:
    ScriptableObject that can be accessed by objects in the scene in order to
    utilize the AudioManager, even if the object doesn't know where the
    audio manager is. By using an asset to facilitate the audio, prefabs can
    be properly hooked up in each scene.
    
    © 2021 DigiPen Institute of Technology, all rights reserved

*******************************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "AudioFacilitator", menuName = "Audio/Facilitator")]
public class AudioFacilitator : ScriptableObject
{
    /// <summary>
    /// Reference to audio manager. If this is null, the getter will find it.
    /// </summary>
    private AudioManager _Manager;
    private AudioManager Manager 
    {
        get
        {
            // Default get
            if (_Manager != null)
                return _Manager;

            _Manager = FindObjectOfType<AudioManager>();
            return _Manager;
        }
    }

    public void PlaySound(AudioEntry entry)
    {
        Manager.PlaySound(entry);
    }

    public void StopSound(AudioEntry entry)
    {
        Manager.StopSound(entry);
    }

    public void FadeInSound(AudioEntry entry)
    {
        Manager.FadeInSound(entry);
    }

    public void FadeOutSound(AudioEntry entry)
    {
        Manager.FadeOutSound(entry);
    }
}
