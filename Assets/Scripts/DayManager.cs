using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public List<DayInfo> days = new List<DayInfo>();
    public DayTimerDisplay dayTimer;

    private int dayIndex = 0;

    private void Start()
    {
        dayTimer.SetCurrentDay(days[dayIndex]);
    }
}
