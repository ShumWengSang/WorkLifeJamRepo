using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A custom class that provides a value which can be displayed by UI using
/// the observer pattern.
/// </summary>
[Serializable]
public class Resource : IResource
{
    public event EventHandler<ValueChangedEventArgs> ValueChanged;

    [SerializeField]
    private float currentValue = 100f;
    [SerializeField]
    private float maxValue = 100f;

    [SerializeField]
    private float growthRate = 0f;

    /// <summary>
    /// Default constructor
    /// </summary>
    public Resource() { }

    /// <summary>
    /// Copy constructor.
    /// </summary>
    public Resource(Resource other)
    {
        currentValue = other.currentValue;
        maxValue = other.maxValue;
        growthRate = other.growthRate;
    }

    public void SetValue(float value)
    {
        float previousValue = currentValue;
        currentValue = Mathf.Clamp(value, 0f, maxValue);

        if (previousValue == currentValue)
            return;

        ValueChanged?.Invoke(this, new ValueChangedEventArgs(currentValue, previousValue));
    }

    public float GetCurrent()
    {
        return currentValue;
    }

    public float GetMaximum()
    {
        return maxValue;
    }

    /// <summary>
    /// Apply the value's growth rate. Should be called on Update.
    /// </summary>
    public void Grow()
    {
        AddValue(growthRate * Time.deltaTime);
    }

    public void SetGrowthRate(float value)
    {
        growthRate = value;
    }

    /// <summary>
    /// Add a flat amount to the current value.
    /// </summary>
    public void AddValue(float amount)
    {
        SetValue(currentValue + amount);
    }

    /// <summary>
    /// Get a clone of the resource.
    /// </summary>
    public Resource Clone()
    {
        return new Resource(this);
    }

    public void SetMaxValue(float value)
    {
        maxValue = value;
    }
}
