using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Job_Prime : MonoBehaviour
{
    public List<LeverUI> levers = new List<LeverUI>();

    public WorkStar rightStar;
    public bool rightStarDone = false;
    public WorkStar leftStar;
    public WorkStar bonusStar;
    public bool leftStarDone = false;

    public GlobalResource money;
    public float gainOne = 15f;
    public float gainTwo = 20f;

    private void Update()
    {
        if(rightStarDone == false)
        {
            bool activeLever = false;
            foreach(var lever in levers)
            {
                if(lever.interactable)
                {
                    activeLever = true;
                    break;
                }
            }

            if(activeLever == false)
            {
                rightStar.FillUpCelebrate();
                //STATS - add star here
                rightStarDone = true;
                money.Add(gainOne);
                foreach(var lever in levers)
                {
                    lever.interactable = true;
                }
            }
        } 
        else if (leftStarDone == false)
        {
            bool activeLever = false;
            foreach (var lever in levers)
            {
                if (lever.interactable)
                {
                    activeLever = true;
                    break;
                }
            }

            if (activeLever == false)
            {
                leftStar.FillUpCelebrate();
                if (bonusStar != null) bonusStar.FillUpCelebrate();
                //STATS - add star here
                money.Add(gainTwo);
                leftStarDone = true;
            }
        }
    }

    public void SecondSide(LeverUI slider)
    {
        if(rightStarDone)
        {
            slider.interactable = false;
        }
    }

    public void FirstSide(LeverUI slider)
    {
        if (rightStarDone == false)
        {
            slider.interactable = false;
        }
    }
}
