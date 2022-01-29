using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event that is raised when a certain value is accrued.
/// </summary>
[System.Serializable]
public class ValueAccrueEvent
{
    [Tooltip("The event will be invoked when the target value is accrued.")]
    public float accrueTarget = 100f;

    [SerializeField]
    [Tooltip("If true, the accrue target will reset after reaching its goal and accrue again.")]
    private bool resetAfterInvoke = false;

    private float accruedAmount { get; set; } = 0f;
    private bool invoked { get; set; } = false;

    [SerializeField]
    private CostUnityEvent<FloatEventArgs> unityEvent = new CostUnityEvent<FloatEventArgs>();

    public void Invoke(float value) => unityEvent.Invoke(value);

    /// <summary>
    /// Add to the accrued value.
    /// </summary>
    public void Accrue(float amount)
    {
        accruedAmount += amount;

        if (accruedAmount > accrueTarget && invoked == false)
        {
            Invoke(accrueTarget);
            invoked = true;
        }

        if (resetAfterInvoke && invoked)
            ResetEvent();
    }

    public void ResetEvent()
    {
        accruedAmount = 0f;
        invoked = false;
    }
}
