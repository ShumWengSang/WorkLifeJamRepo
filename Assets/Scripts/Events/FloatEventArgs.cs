using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event argument that can be implicitly converted to a float.
/// </summary>
public class FloatEventArgs : EventArgs
{
    public readonly float Value;

    public FloatEventArgs(float value)
    {
        Value = value;
    }

    public static implicit operator FloatEventArgs(float value)
    {
        return new FloatEventArgs(value);
    }

    public static implicit operator float(FloatEventArgs e)
    {
        return e.Value;
    }
}
