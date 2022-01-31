using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HobbyManager : MonoBehaviour
{
    private bool hasScoredToday = true;
    public ParticleSystem celebration;

    public void Score()
    {
        if (hasScoredToday == false)
        {
            hasScoredToday = true;
            MetaStatManager.achievedHobby++;
        }
    }

    public void NewDay()
    {
        hasScoredToday = false;
    }
}
