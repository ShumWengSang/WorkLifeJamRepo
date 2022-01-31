using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenManager : MonoBehaviour
{
    private bool hasScoredToday = false;
    public ParticleSystem celebration;

    public void Score()
    {
        if (hasScoredToday == false)
        {
            hasScoredToday = true;
            MetaStatManager.achievedFren++;
        }
    }

    public void NewDay()
    {
        hasScoredToday = false;
    }
}
