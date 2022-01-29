using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to validate/apply costs to inspector events.
/// </summary>
[System.Serializable]
public class ResourceCost<TResource> where TResource : IResource
{
    public TResource resource;
    public float cost = 0f;

    public void SpendResource()
    {
        if (resource == null)
            return;

        resource.AddValue(-cost);
    }

    /// <summary>
    /// Validates that the cost is affordable by the resource.
    /// </summary>
    public bool IsSufficient()
    {
        if (resource == null)
            return true;

        return (resource.GetCurrent() >= cost);
    }
}

[System.Serializable]
public class LocalResourceCost : ResourceCost<LocalResource> { }

[System.Serializable]
public class GlobalResourceCost : ResourceCost<GlobalResource> { }