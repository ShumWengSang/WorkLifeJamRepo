using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField]
    private WorkStats workStats;

    [SerializeField]
    private GlobalResource stars;

    public void DisplayStats()
    {
        int currentStars = Mathf.RoundToInt(stars.GetCurrent());
        int maxStars = Mathf.RoundToInt(stars.GetMaximum());

        MetaStatManager.totalStars += (int)stars.GetMaximum();
        MetaStatManager.achievedStars += (int)stars.GetCurrent();

        stars.SetValue(0);
        stars.SetMaxValue(0);

        workStats.ShowStars(currentStars, maxStars);
    }
}
