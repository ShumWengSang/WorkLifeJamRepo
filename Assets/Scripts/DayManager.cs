using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Objec that runs through the day cycle for each day in the game.
/// </summary>
public class DayManager : MonoBehaviour
{
    public GlobalResource TimeResource;
    public List<DayInfo> days = new List<DayInfo>();

    public DayInfo currentDay => days[dayIndex];

    private int dayIndex = -1;

    private void Start()
    {
        
    }

    public void StartDay(int index)
    {
        dayIndex = index;

        InitializeDay();
    }

    public void StartNextDay()
    {
        dayIndex++;

        StartDay(dayIndex);
    }

    /// <summary>
    /// Disable every tile across every day.
    /// </summary>
    public void DisableAllTiles()
    {
        List<GameObject> allTiles = GetAllTiles();

        foreach (GameObject tile in allTiles)
        {
            tile.SetActive(false);
        }
    }

    private void InitializeDay()
    {
        EnableCurrentDayTiles();
        SetTimeResource();
    }

    private void EnableCurrentDayTiles()
    {
        foreach (GameObject tile in currentDay.tiles)
        {
            tile.SetActive(true);
        }
    }

    private void SetTimeResource()
    {
        TimeResource.SetValue(currentDay.dayLength);
    }

    /// <summary>
    /// Get every tile used over the course of the game.
    /// </summary>
    private List<GameObject> GetAllTiles()
    {
        List<GameObject> allTiles = new List<GameObject>();

        foreach (DayInfo day in days)
        {
            foreach (GameObject tile in day.tiles)
            {
                if (allTiles.Contains(tile))
                    continue;

                allTiles.Add(tile);
            }
        }

        return allTiles;
    }
}
