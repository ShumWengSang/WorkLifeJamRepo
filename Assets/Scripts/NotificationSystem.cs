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
    private CameraController cameraController;

    [SerializeField]
    private GameObject notificationPrefab;

    [SerializeField]
    private Transform leftNotificationPanel;
    [SerializeField]
    private Transform rightNotificationPanel;
    [SerializeField]
    private Transform bottomNotificationPanel;
    [SerializeField]
    private Transform topNotificationPanel;

    private List<GameObject> tiles;

    private Dictionary<Tile, GameObject> notifications = new Dictionary<Tile, GameObject>();

    private Vector2 cameraPosition { get; set; }

    private void Start()
    {
        cameraPosition = cameraController.transform.position;

        RegisterDayChangeEvents();
        RegisterCameraMoveEvent();
    }

    private void OnDestroy()
    {
        DeregisterDayChangeEvents();
        DeregisterCameraMoveEvent();
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

    private void RegisterCameraMoveEvent()
    {
        cameraController.Moved += OnCameraMoved;
    }

    private void DeregisterCameraMoveEvent()
    {
        cameraController.Moved -= OnCameraMoved;
    }

    private void RegisterTileDemandedAttentionEvent(Tile tile)
    {
        tile.DemandedAttention += OnTileDemandedAttention;
        tile.StoppedDemangingAttention += OnTileStoppedDemandingAttention;
    }

    private void DeregisterTileDemandedAttentionEvent(Tile tile)
    {
        tile.DemandedAttention -= OnTileDemandedAttention;
    }

    private void CreateNotification(Tile tile)
    {
        if (notifications.ContainsKey(tile))
            return;

        GameObject notificationObj = Instantiate(notificationPrefab, rightNotificationPanel);
        UpdateNotificationPosition(tile, notificationObj.transform);
        notifications.Add(tile, notificationObj);
    }

    private void RemoveNotification(Tile tile)
    {
        if (!notifications.ContainsKey(tile))
            return;

        notifications.TryGetValue(tile, out GameObject notification);
        Destroy(notification);
        notifications.Remove(tile);
    }

    private void UpdateNotificationPositions()
    {
        foreach (Tile tile in notifications.Keys)
        {
            notifications.TryGetValue(tile, out GameObject notificationObj);
            Transform notification = notificationObj.transform;
            UpdateNotificationPosition(tile, notification);
        }
    }

    private void UpdateNotificationPosition(Tile tile, Transform notification)
    {
        Vector2 tileDirection = (Vector2)tile.transform.position - cameraPosition;

        if (tileDirection.magnitude < 5f)
        {
            notification.gameObject.SetActive(false);
            return;
        }

        notification.gameObject.SetActive(true);

        if (tileDirection.y > 5f)
        {
            notification.transform.parent = topNotificationPanel;
            return;
        }

        if (tileDirection.y < -5f)
        {
            notification.transform.parent = bottomNotificationPanel;
            return;
        }

        if (tileDirection.x > 5f)
        {
            notification.transform.parent = rightNotificationPanel;
            return;
        }

        if (tileDirection.x < -5f)
        {
            notification.transform.parent = leftNotificationPanel;
            return;
        }
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

    protected virtual void OnTileStoppedDemandingAttention(object sender, EventArgs e)
    {
        Tile tile = sender as Tile;

        RemoveNotification(tile);
    }

    protected virtual void OnCameraMoved(object sender, EventArgs e)
    {
        cameraPosition = cameraController.transform.position;

        UpdateNotificationPositions();
    }
}
