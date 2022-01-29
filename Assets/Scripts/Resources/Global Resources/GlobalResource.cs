using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject containing a <see cref="Resource"/>.
/// </summary>
[CreateAssetMenu(fileName = "NewResource", menuName = "Resource")]
public class GlobalResource : ScriptableObject, IResource
{
    [SerializeField]
    private Resource resourceStartingValues;

    [SerializeField]
    private Resource resource { get; set; }

    public event EventHandler<ValueChangedEventArgs> ValueChanged
    {
        add
        {
            ((IResource)resource).ValueChanged += value;
        }

        remove
        {
            ((IResource)resource).ValueChanged -= value;
        }
    }

    public void Initialize()
    {
        resource = resourceStartingValues.Clone();
    }

    public float GetCurrent()
    {
        return ((IResource)resource).GetCurrent();
    }

    public float GetMaximum()
    {
        return ((IResource)resource).GetMaximum();
    }

    /// <summary>
    /// Adds a flat value.
    /// </summary>
    public void Add(float value)
    {
        resource.AddValue(value);
    }

    /// <summary>
    /// Subtracts a flat value.
    /// </summary>
    public void Subtract(float value)
    {
        resource.AddValue(-value);
    }

    /// <summary>
    /// Adds a value multiplied by delta time.
    /// </summary>
    public void AddByDeltaTime(float value)
    {
        Add(value * Time.deltaTime);
    }

    /// <summary>
    /// Subtracts a value multiplied by delta time.
    /// </summary>
    public void SubtractByDeltaTime(float value)
    {
        Subtract(value * Time.deltaTime);
    }

    public void Update()
    {
        resource.Grow();
    }
}
