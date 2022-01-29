using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event that can be triggered when a value reaches a certain threshold.
/// </summary>
[System.Serializable]
public class ValueThresholdEvent
{
    [Range(0f, 1f)]
    public float thresholdValue = 0f;

    [SerializeField]
    private UnityEvent<FloatEventArgs> unityEvent = new UnityEvent<FloatEventArgs>();

    public void Invoke(float value) => unityEvent.Invoke(value);
}
