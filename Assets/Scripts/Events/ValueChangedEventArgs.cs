using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event arguments for a value change. Contains both the previous value and
/// the new value.
/// </summary>
public class ValueChangedEventArgs : EventArgs
{
    /// <summary>
    /// Value after the value change.
    /// </summary>
    public readonly float CurrentValue;

    /// <summary>
    /// Value prior to the value change.
    /// </summary>
    public readonly float PreviousValue;

    /// <summary>
    /// The change in values.
    /// </summary>
    public float ValueDelta { get => CurrentValue - PreviousValue; }

    public ValueChangedEventArgs(float currentValue, float previousValue)
    {
        CurrentValue = currentValue;
        PreviousValue = previousValue;
    }
}
