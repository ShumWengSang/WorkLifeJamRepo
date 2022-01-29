using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Resource event subclass for global resources.
/// </summary>
public class GlobalResourceEvents : ResourceEvents
{
    [SerializeField]
    private GlobalResource resource;

    protected override IResource value => resource;
}
