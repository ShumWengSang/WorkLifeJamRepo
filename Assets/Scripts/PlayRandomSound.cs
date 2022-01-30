using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayRandomSound : MonoBehaviour
{
    public AudioEntry audioEntry;
    public AudioSource audioSource;
    public void PlayARandomSound()
    {
        audioSource.PlayOneShot(audioEntry.Random().Clip);
    }

}
