using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    private bool hasScoredToday = false;
    public ParticleSystem celebration;

    public void Score()
    {
        if(hasScoredToday == false)
        {
            hasScoredToday = true;
            MetaStatManager.achievedPlant++;
        }
    }

    public void NewDay()
    {
        hasScoredToday = false;
    }
}
