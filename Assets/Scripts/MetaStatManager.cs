using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MetaStatManager : MonoBehaviour
{
    public static int totalStars = 0;
    public static int achievedStars = 0;

    public static float achievedPlant;
    public static float achievedCat;
    public static float achievedHobby;
    public static float achievedFren;
    public static float achievedMate;

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

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void DisplayStats()
    {
        read.text = totalStars.ToString() + " / " + totalStars.ToString();

        plant.FillLevel(achievedPlant / totalPlant);
        cat.FillLevel(achievedCat / totalCat);
        hobby.FillLevel(achievedHobby / totalHobby);
        fren.FillLevel(achievedFren / totalFren);
        mate.FillLevel(achievedMate / totalMate);
    }
}
