using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Progress bar display for a local resource.
/// </summary>
public class LocalResourceDisplay : ResourceDisplay
{
    [SerializeField]
    private LocalResource resource;

    protected override IResource value => resource;
}
