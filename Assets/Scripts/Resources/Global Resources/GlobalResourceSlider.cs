using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Custom resource slider for a global resource.
/// </summary>
public class GlobalResourceSlider : ResourceSlider
{
    [SerializeField]
    private GlobalResource resource;

    protected override IResource value => resource;
}
