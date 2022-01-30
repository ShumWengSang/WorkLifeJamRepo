using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndofDayManager : MonoBehaviour
{
    public event EventHandler DayEnded;

    public GlobalResource time;

    public BeginningofDayManager beginningofDay;
    public FadeController fadeScreen;
    public StatsManager statsScreen;

    public DG.Tweening.Ease easing;

    public float dayFadeOutTimer = 1f;
    public float waitTimer = 1f;
    public float statsFadeInTimer = 1f;
    public float statsFadeOutTimer = 1f;

    public void EndDay()
    {
        ResourceEvents.CanTrigger = false;

        fadeScreen.FadeIn(dayFadeOutTimer);
        Invoke(nameof(SetupStats), dayFadeOutTimer);
        Invoke(nameof(ShowStats), dayFadeOutTimer+waitTimer);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F12))
        {
            time.Subtract(10);
        }
    }

    public void SetupStats()
    {
        statsScreen.gameObject.SetActive(true);
        statsScreen.DisplayStats();
    }

    public void ShowStats()
    {
        fadeScreen.FadeOut(statsFadeInTimer);
    }

    public void FinishDay()
    {
        fadeScreen.FadeIn(statsFadeOutTimer);
        //
        //next day call goes here
        Invoke(nameof(PromptStart), statsFadeOutTimer);
    }

    public void PromptStart()
    {
        statsScreen.gameObject.SetActive(false);
        beginningofDay.PromptStart();
    }

    public void InvokeDayEnd()
    {
        DayEnded?.Invoke(this, EventArgs.Empty);
    }
}
