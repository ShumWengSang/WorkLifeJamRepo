using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// System used to manage Tile notifications.
/// </summary>
public class NotificationSystem : MonoBehaviour
{
    [SerializeField]
    private DayManager dayManager;

    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    private void RegisterDayChangeEvent()
    {
        dayManager.DayStarted += OnDayStarted;
    }

    private void DeregisterDayChangeEvent()
    {
        dayManager.DayStarted -= OnDayStarted;
    }

    private void RegisterTileDemandedAttentionEvent(Tile tile)
    {
        
    }

    private void DeregisterTileDemandedAttentionEvent(Tile tile)
    {

    }

    protected virtual void OnDayStarted(object sender, IntEventArgs e)
    {

    }

    protected virtual void OnTileDemandedAttention(object sender, TileEventArgs e)
    {

    }
}
