using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatUtility
{
    /// <summary>
    /// Creates a <see cref="FloatRange"/> using two float values.
    /// </summary>
    public static FloatRange To(this float a, float b)
    {
        return new FloatRange(a, b);
    }

    public static bool IsPositive(this float value)
    {
        return (value > 0f);
    }

    public static bool IsNegative(this float value)
    {
        return (value < 0f);
    }
}

/// <summary>
/// A range between two floating point values.
/// </summary>
public class FloatRange
{
    public readonly float LowerBound;
    public readonly float UpperBound;

    public FloatRange(float a, float b)
    {
        LowerBound = Mathf.Min(a, b);
        UpperBound = Mathf.Max(a, b);
    }

    /// <summary>
    /// Returns true if the range contains the argument. (Inclusive)
    /// </summary>
    public bool Contains(float value)
    {
        return (value >= LowerBound && value <= UpperBound);
    }
}
