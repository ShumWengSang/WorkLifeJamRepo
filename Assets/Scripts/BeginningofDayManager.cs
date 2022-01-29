using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningofDayManager : MonoBehaviour
{
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
    }

    public void PromptStart()
    {
        Invoke(nameof(TurnOnButton), buttonDisplayWait);
    }
}
