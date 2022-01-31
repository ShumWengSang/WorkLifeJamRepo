using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    private bool hasScoredToday = false;
    public ParticleSystem celebration;

    public void Score()
    {
        if (hasScoredToday == false)
        {
            hasScoredToday = true;
            MetaStatManager.achievedCat++;
        }
    }

    public void NewDay()
    {
        hasScoredToday = false;
    }
}
