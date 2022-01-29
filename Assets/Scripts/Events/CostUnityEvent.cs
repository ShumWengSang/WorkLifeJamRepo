using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Extension of UnityEvent that can abort if a resource cost can't be met.
/// </summary>
[System.Serializable]
public class CostUnityEvent
{
    [SerializeField]
    private LocalResourceCost localResourceCost;
    
    [SerializeField]
    private GlobalResourceCost globalResourceCost;

    [SerializeField]
    private UnityEvent unityEvent = new UnityEvent();

    public void Invoke()
    {
        if (!localResourceCost.IsSufficient() || !globalResourceCost.IsSufficient())
            return;

        localResourceCost.SpendResource();
        globalResourceCost.SpendResource();

        unityEvent.Invoke();
    }
}

[System.Serializable]
public class CostUnityEvent<TArg0>
{
    [SerializeField]
    private LocalResourceCost localResourceCost;

    [SerializeField]
    private GlobalResourceCost globalResourceCost;

    [SerializeField]
    private UnityEvent<TArg0> unityEvent = new UnityEvent<TArg0>();

    public void Invoke(TArg0 arg0)
    {
        if (!localResourceCost.IsSufficient() || !globalResourceCost.IsSufficient())
            return;

        localResourceCost.SpendResource();
        globalResourceCost.SpendResource();

        unityEvent.Invoke(arg0);
    }
}
