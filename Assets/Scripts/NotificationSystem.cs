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
    }

    private void OnDestroy()
    {
        DeregisterDayChangeEvents();
    }

    private void Update()
    {
        cameraPosition = cameraController.transform.position;

        UpdateNotificationPositions();
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

        if (tile.notificationIcon != null)
            notificationObj.GetComponent<Notification>().icon.sprite = tile.notificationIcon;
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
        RectTransform tileTransform = tile.transform as RectTransform;
        Vector2 tileDirection = (Vector2)tileTransform.position - cameraPosition;

        if (tileDirection.magnitude < 75f)
        {
            notification.gameObject.SetActive(false);
            return;
        }

        notification.gameObject.SetActive(true);

        if (tileDirection.y > 15f)
        {
            notification.transform.SetParent(topNotificationPanel, false);
            notification.transform.rotation = Quaternion.identity;
            return;
        }

        if (tileDirection.y < -15f)
        {
            notification.transform.SetParent(bottomNotificationPanel, false);
            notification.transform.rotation = Quaternion.identity;
            return;
        }

        if (tileDirection.x > 15f)
        {
            notification.transform.SetParent(rightNotificationPanel, false);
            notification.transform.rotation = Quaternion.identity;
            return;
        }

        if (tileDirection.x < -15f)
        {
            notification.transform.SetParent(leftNotificationPanel, false);
            notification.transform.rotation = Quaternion.identity;
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
}
