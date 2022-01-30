using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkDrinks : MonoBehaviour
{
    public int upgradeLevel = 0;

    public Button hold;
    public List<Button> upperPressList = new List<Button>();
    public List<Button> lowerPressList = new List<Button>();
    private List<Button> totalPressList = new List<Button>();

    public float minTimeBeforeInterupt = 1f;
    public float maxTimeBeforeInterupt = 2f;

    public LocalResource firstBar;
    public LocalResource secondBar;
    public LocalResource thirdBar;

    public WorkStar firstStar;
    public WorkStar secondStar;
    public WorkStar thirdStar;

    private int numberOfPress = 4;

    private int unresolvedPressCount = 0;

    private void Awake()
    {
        if (maxTimeBeforeInterupt < minTimeBeforeInterupt) maxTimeBeforeInterupt = minTimeBeforeInterupt;

        foreach(var press in upperPressList)
        {
            totalPressList.Add(press);
        }

        foreach (var press in lowerPressList)
        {
            totalPressList.Add(press);
        }

        SetUpTile();
    }

    public void SetUpTile()
    {
        switch(upgradeLevel)
        {
            default:
            case 0:
            {
                numberOfPress = 4;
                break;
            }
            case 1:
            {
                numberOfPress = 6;
                break;
            }
        }

        int i = numberOfPress / 2;
        foreach (var button in upperPressList)
        {
            if (i > 0)
            {
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }

            i--;
        }

        i = numberOfPress / 2;
        foreach (var button in lowerPressList)
        {
            if (i > 0)
            {
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
            i--;
        }

        foreach(var button in totalPressList)
        {
            button.interactable = false;
        }

        hold.interactable = true;

        TurnOnInterupt();
    }

    public void TurnOnInterupt()
    {
        int i = Random.Range(0, numberOfPress);
        int first_i = i;

        while(totalPressList[i].gameObject.activeSelf == false || totalPressList[i].interactable == true)
        {
            i++;
            if (i >= totalPressList.Count) i = 0;

            if (first_i == i) return;
        }

        totalPressList[i].gameObject.SetActive(true);
        totalPressList[i].interactable = true;

        hold.interactable = false;
        unresolvedPressCount++;
    }

    public void ButtonPress()
    {
        unresolvedPressCount--;
        Invoke(nameof(TurnOnInterupt),Random.Range(minTimeBeforeInterupt, maxTimeBeforeInterupt));
    }

    private void Update()
    {
        if (unresolvedPressCount <= 0) hold.interactable = true;
    }

    public void ResourceUp()
    {
        if(firstBar.GetCurrent() < 100f)
        {
            var val = (100f / 13f) * Time.deltaTime;
            firstBar.Add(val);
            if(firstBar.GetCurrent() >= 100)
            {
                firstStar.FillUpCelebrate();
                //update stats
            }
        }
        else if (secondBar.GetCurrent() < 100f)
        {
            secondBar.Add((100f / 13f) * Time.deltaTime);
            if (secondBar.GetCurrent() >= 100)
            {
                secondStar.FillUpCelebrate();
                //update stats
            }
        }
        else if (thirdBar.GetCurrent() < 100f)
        {
            thirdBar.Add((100f / 13f) * Time.deltaTime);
            if (thirdBar.GetCurrent() >= 100)
            {
                thirdStar.FillUpCelebrate();
                //update stats
            }
        }
    }
}
