using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayEvents : MonoBehaviour
{
    [SerializeField]
    private DayManager dayManager;

    [SerializeField]
    private List<IntThresholdEvent> thresholdEvents = new List<IntThresholdEvent>();

    private void Start()
    {
        RegisterDayManagerEvents();
    }

    private void OnDestroy()
    {
        DeregisterDayManagerEvents();
    }

    private void RegisterDayManagerEvents()
    {
        dayManager.DayStarted += OnDayStarted;
    }

    private void DeregisterDayManagerEvents()
    {
        dayManager.DayStarted -= OnDayStarted;
    }

    protected virtual void OnDayStarted(object sender, IntEventArgs e)
    {
        foreach (IntThresholdEvent thresholdEvent in thresholdEvents)
        {
            if (e == thresholdEvent.thresholdValue)
                thresholdEvent.Invoke(e);
        }
    }
}
