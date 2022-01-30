using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static extension methods for transforms.
/// </summary>
public static class TransformUtility
{
    public static int GetActiveChildren(this Transform transform)
    {
        int count = 0;

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
                count++;
        }

        return count;
    }

    public static Transform GetActiveChild(this Transform transform, int index)
    {
        int enumerator = 0;

        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf)
                continue;

            if (enumerator == index)
                return child;

            enumerator++;
        }

        return null;
    }
}
