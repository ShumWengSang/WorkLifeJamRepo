using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for an object that can provide a current and maximum value 
/// (which can be interpreted as a percentage).
/// </summary>
public interface IResource
{
    event EventHandler<ValueChangedEventArgs> ValueChanged;
    float GetCurrent();
    float GetMaximum();
}
