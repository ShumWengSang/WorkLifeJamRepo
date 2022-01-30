using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event argument that can be implicitly converted to an integer.
/// </summary>
public class IntEventArgs : EventArgs
{
    public readonly int Value;

    public IntEventArgs(int value)
    {
        Value = value;
    }

    public static implicit operator IntEventArgs(int value)
    {
        return new IntEventArgs(value);
    }

    public static implicit operator int(IntEventArgs e)
    {
        return e.Value;
    }
}
