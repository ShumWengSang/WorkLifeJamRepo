using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MetaStatManager : MonoBehaviour
{
    public static int totalStars = 0;
    public static int achievedStars = 0;

    public float achievedPlant;
    public float achievedCat;
    public float achievedHobby;
    public float achievedFren;
    public float achievedMate;

    private int totalPlant = 11;
    private int totalCat = 10;
    private int totalHobby = 7;
    private int totalFren = 9;
    private int totalMate = 6;

    public TextMeshProUGUI read;

    public LieFill plant;
    public LieFill cat;
    public LieFill hobby;
    public LieFill fren;
    public LieFill mate;

    public void AchievedLifePlant(float value)
    {
        achievedPlant += value;
    }

    public void AchievedLifeCat(float value)
    {
        achievedCat += value;
    }
    public void AchievedLifeHobbu(float value)
    {
        achievedHobby += value;
    }
    public void AchievedLifeFren(float value)
    {
        achievedFren += value;
    }
    public void AchievedLifeMate(float value)
    {
        achievedMate += value;
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void DisplayStats()
    {
        read.text = achievedStars.ToString() + " / " + totalStars.ToString();

        plant.FillLevel(achievedPlant / totalPlant);
        cat.FillLevel(achievedCat / totalCat);
        hobby.FillLevel(achievedHobby / totalHobby);
        fren.FillLevel(achievedFren / totalFren);
        mate.FillLevel(achievedMate / totalMate);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DisplayStats();
        }
    }
}
