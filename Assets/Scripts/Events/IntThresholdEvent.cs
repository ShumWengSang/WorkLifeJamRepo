using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntThresholdEvent : MonoBehaviour
{
    public int thresholdValue = 0;

    [SerializeField]
    private UnityEvent<IntEventArgs> unityEvent = new UnityEvent<IntEventArgs>();

    public void Invoke(int value) => unityEvent.Invoke(value);
}
