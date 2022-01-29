using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior used to initialize and update global resources 
/// (since they can't do it themselves).
/// </summary>
public class GlobalResourceManager : MonoBehaviour
{
    [SerializeField]
    private List<GlobalResource> resources = new List<GlobalResource>();

    private void Awake()
    {
        InitializeResources();
    }

    private void Update()
    {
        UpdateResources();
    }

    private void InitializeResources()
    {
        foreach (GlobalResource resource in resources)
        {
            resource.Initialize();
        }
    }

    private void UpdateResources()
    {
        foreach (GlobalResource resource in resources)
        {
            resource.Update();
        }
    }
}
