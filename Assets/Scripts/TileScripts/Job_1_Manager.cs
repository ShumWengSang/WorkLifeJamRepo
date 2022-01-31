using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job_1_Manager : MonoBehaviour
{
    public WorkStar rightStar;
    public bool rightStarDone = false;
    public WorkStar leftStar;

    public GlobalResource money;
    public float gainOne = 15f;
    public float gainTwo = 20f;

    public GlobalResource energy;
    public float gainOneNRG = 15f;
    public float gainTwoNRG = 20f;

    public void SecondSide(LeverUI slider)
    {
        if (rightStarDone)
        {
            if(leftStar != null) leftStar.FillUpCelebrate();
            money.Add(gainTwo);
        }
    }

    public void FirstSide(LeverUI slider)
    {
        if (rightStarDone == false)
        {
            rightStar.FillUpCelebrate();
            rightStarDone = true;
            money.Add(gainOne);
        }
    }
}
