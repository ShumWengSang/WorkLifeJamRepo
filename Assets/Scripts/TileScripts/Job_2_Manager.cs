using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Job_2_Manager : MonoBehaviour
{
    public WorkStar rightStar;
    public bool rightStarDone = false;

    public GlobalResource money;
    public float gainOne = 15f;

    public GlobalResource energy;
    public float gainOneNRG = 15f;

    public float holdLength = 5f;

    public float window = 11f;
    public Button hold;
    public Button start;

    public void HoldOffInvokeMe()
    {
        Invoke(nameof(HoldOff), window);
    }

    public void HoldOff()
    {
        hold.interactable = false;
        start.interactable = true;
    }

    public void HoldTimerDown()
    {
        holdLength -= Time.deltaTime;
        if (rightStarDone == false &&  holdLength <= 0) Done();
    }

    public void Done()
    {
        rightStarDone = true;
        rightStar.FillUpCelebrate();
        energy.Add(gainOneNRG);
        money.Add(gainOne);
    }
}
