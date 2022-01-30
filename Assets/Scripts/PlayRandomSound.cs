using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayRandomSound : MonoBehaviour
{
    public AudioEntry audioEntry;

    public void PlayARandomSound()
    {
        GetComponent<AudioSource>().PlayOneShot(audioEntry.Random().Clip);
    }

}
