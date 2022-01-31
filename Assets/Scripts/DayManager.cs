using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Objec that runs through the day cycle for each day in the game.
/// </summary>
public class DayManager : MonoBehaviour
{
    public event EventHandler<IntEventArgs> DayStarted;

    public GlobalResource TimeResource;
    public List<DayInfo> days = new List<DayInfo>();

    public DayInfo currentDay => days[dayIndex];

    [SerializeField]
    private List<GameObject> parentTiles = new List<GameObject>();
    private int dayIndex = -1;

    public Transform endScreen;

    public void StartDay(int index)
    {
        dayIndex = index;

        InitializeDay();

        DayStarted?.Invoke(this, dayIndex);

        InvokeTileStart();
    }

    public void StartNextDay()
    {
        dayIndex++;
        
        if(dayIndex < days.Count)
            StartDay(dayIndex);
        else
        {
            // End of day
            endScreen.gameObject.SetActive(true);
            endScreen.GetComponent<MetaStatManager>().DisplayStats();
        }
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
        DisableAllTiles();
        EnableCurrentDayTiles();
        RedrawTiles();

        SetTimeResource();

        ResourceEvents.CanTrigger = true;
    }

    private void EnableCurrentDayTiles()
    {
        foreach (GameObject tileObj in currentDay.tiles)
        {
            tileObj.SetActive(true);
        }
    }

    private void InvokeTileStart()
    {
        foreach (GameObject tileObj in currentDay.tiles)
        {
            Tile tile = tileObj.GetComponent<Tile>();
            if (tile == null)
                return;

            tile.StartDay(dayIndex);
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

    private void RedrawTiles()
    {
        foreach (GameObject tile in parentTiles)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(tile.transform as RectTransform);
        }
    }
}
