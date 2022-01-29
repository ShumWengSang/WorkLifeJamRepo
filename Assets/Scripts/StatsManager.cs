using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField]
    private WorkStats workStats;

    public void DisplayStats ()
    {
        workStats.ShowStars(2, 5);
    }
}
