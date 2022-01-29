using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Resource event subclass for local resources.
/// </summary>
public class LocalResourceEvents : ResourceEvents
{
    [SerializeField]
    protected LocalResource resource;

    protected override IResource value => resource;
}
