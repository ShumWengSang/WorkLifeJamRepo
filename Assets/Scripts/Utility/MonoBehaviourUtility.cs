using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static extension methods for MonoBehaviours.
/// </summary>
public static class MonoBehaviourUtility
{
    public static void DoAfterDelay(this MonoBehaviour behaviour, Action action, float delay)
    {
        behaviour.StartCoroutine(DoAfterDelayCoroutine(action, delay));
    }

    private static IEnumerator DoAfterDelayCoroutine(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);

        action.Invoke();
    }
}
