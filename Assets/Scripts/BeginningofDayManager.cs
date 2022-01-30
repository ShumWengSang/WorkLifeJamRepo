using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeginningofDayManager : MonoBehaviour
{
    public UnityEvent OnDayStarted = new UnityEvent();

    public DayManager dayManager;
    public FadeController fadeScreen;
    public GameObject button;

    public float buttonDisplayWait = 2f;
    public float FadeInLength = 1f;

    private void Start()
    {
        PromptStart();
    }

    public void TurnOnButton()
    {
        button.SetActive(true);
    }

    public void StartDay()
    {
        fadeScreen.FadeOut(FadeInLength);

        //just call start next day here
        dayManager.StartNextDay();

        OnDayStarted.Invoke();
    }

    public void PromptStart()
    {
        Invoke(nameof(TurnOnButton), buttonDisplayWait);
    }
}
