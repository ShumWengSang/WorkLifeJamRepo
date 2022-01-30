using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField]
    private WorkStats workStats;

    private GlobalResource stars;

    public void DisplayStats()
    {
        int currentStars = Mathf.RoundToInt(stars.GetCurrent());
        int maxStars = Mathf.RoundToInt(stars.GetMaximum());
        workStats.ShowStars(currentStars, maxStars);
    }
}
