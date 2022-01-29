using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTimerDisplay : MonoBehaviour
{
    public DayInfo currentDay { get; private set; }

    [SerializeField]
    private Image fillImage;
    private float timeRemaining { get; set; }

    // Update is called once per frame
    void Update()
    {
        if (currentDay == null)
            return;

        timeRemaining -= Time.deltaTime;

        fillImage.fillAmount = timeRemaining / currentDay.dayLength;
    }

    public void SetCurrentDay(DayInfo day)
    {
        currentDay = day;
        timeRemaining = day.dayLength;
    }
}
