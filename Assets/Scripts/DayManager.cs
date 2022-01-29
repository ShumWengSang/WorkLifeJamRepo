using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public event EventHandler DayTimeElapsed;

    public List<DayInfo> days = new List<DayInfo>();
    public DayTimerDisplay dayTimer;

    public DayInfo currentDay => days[dayIndex];
    public float dayTimeRemaining { get; private set; }

    private int dayIndex = 0;

    private void Start()
    {
        StartDay(dayIndex);
    }

    private void Update()
    {
        dayTimeRemaining -= Time.deltaTime;

        if (dayTimeRemaining <= 0f)
        {
            DayTimeElapsed?.Invoke(this, EventArgs.Empty);
        }
    }

    public void StartDay(int index)
    {
        dayIndex = index;

        dayTimeRemaining = currentDay.dayLength;
    }
}
