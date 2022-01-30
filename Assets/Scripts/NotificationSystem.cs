using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// System used to manage Tile notifications.
/// </summary>
public class NotificationSystem : MonoBehaviour
{
    [SerializeField]
    private DayManager dayManager;
    [SerializeField]
    private EndofDayManager endofDayManager;

    [SerializeField]
    private GameObject notificationPrefab;

    private List<GameObject> tiles;

    private Dictionary<Tile, GameObject> notifications = new Dictionary<Tile, GameObject>();

    private void Start()
    {
        RegisterDayChangeEvents();
    }

    private void OnDestroy()
    {
        DeregisterDayChangeEvents();
    }

    private void RegisterDayChangeEvents()
    {
        dayManager.DayStarted += OnDayStarted;
        endofDayManager.DayEnded += OnDayEnded;
    }

    private void DeregisterDayChangeEvents()
    {
        dayManager.DayStarted -= OnDayStarted;
        endofDayManager.DayEnded -= OnDayEnded;
    }

    private void RegisterTileDemandedAttentionEvent(Tile tile)
    {
        tile.DemandedAttention += OnTileDemandedAttention;
    }

    private void DeregisterTileDemandedAttentionEvent(Tile tile)
    {
        tile.DemandedAttention -= OnTileDemandedAttention;
    }

    protected virtual void OnDayStarted(object sender, EventArgs e)
    {
        tiles = dayManager.currentDay.tiles;
        foreach (GameObject tile in tiles)
        {
            RegisterTileDemandedAttentionEvent(tile.GetComponent<Tile>());
        }
    }

    protected virtual void OnDayEnded(object sender, EventArgs e)
    {
        foreach (GameObject tileObj in tiles)
        {
            Tile tile = tileObj.GetComponent<Tile>();
            DeregisterTileDemandedAttentionEvent(tile);
            RemoveNotification(tile);
        }
    }

    protected virtual void OnTileDemandedAttention(object sender, FloatEventArgs e)
    {
        Tile tile = sender as Tile;

        CreateNotification(tile);
    }

    private void CreateNotification(Tile tile)
    {
        if (notifications.ContainsKey(tile))
            return;


    }

    private void RemoveNotification(Tile tile)
    {
        if (!notifications.ContainsKey(tile))
            return;

        tile.StopDemandingAttention();

        // Clear dictionary and destroy notifications
        while (notifications.Count > 0)
        {
            notifications.TryGetValue(tile, out GameObject notification);
            Destroy(notification);
            notifications.Remove(tile);
        }
    }
}
