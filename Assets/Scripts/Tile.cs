using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public event EventHandler<FloatEventArgs> DemandedAttention;
    public event EventHandler StoppedDemangingAttention;
    public event EventHandler<ValueChangedEventArgs> DemandStrengthChanged;

    [Header("Properties")]
    [SerializeField]
    private float demandStrength = 1f;

    [Header("Event Callbacks")]
    public UnityEvent<IntEventArgs> OnDayStarted = new UnityEvent<IntEventArgs>();
    public UnityEvent OnUpgrade = new UnityEvent();
    public UnityEvent OnDemandAttention = new UnityEvent();
    public UnityEvent<FloatEventArgs> OnDemandStrengthChanged = new UnityEvent<FloatEventArgs>();

    public void Upgrade()
    {
        OnUpgrade.Invoke();
    }

    public void DemandAttention()
    {
        DemandedAttention?.Invoke(this, demandStrength);
        OnDemandAttention.Invoke();
    }

    public void StopDemandingAttention()
    {
        StoppedDemangingAttention?.Invoke(this, EventArgs.Empty);
    }

    public void SetDemandStrength(float value)
    {
        float previousValue = demandStrength;

        if (value == previousValue)
            return;

        demandStrength = Mathf.Min(0f, demandStrength);

        if (demandStrength == 0f)
        {
            StopDemandingAttention();
            return;
        }

        DemandStrengthChanged?.Invoke(this, new ValueChangedEventArgs(previousValue, value));
        OnDemandStrengthChanged.Invoke(demandStrength);
    }

    public float GetDemandStrength()
    {
        return demandStrength;
    }

    public void AddDemandStrength(float value)
    {
        SetDemandStrength(demandStrength + value);
    }

    public void ReduceDemandStrength(float value)
    {
        AddDemandStrength(-value);
    }

    public void StartDay(int day)
    {
        OnDayStarted.Invoke(day);
    }
}
