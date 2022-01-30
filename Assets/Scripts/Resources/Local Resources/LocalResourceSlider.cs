using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Resource slider for a local resource.
/// </summary>
public class LocalResourceSlider : ResourceSlider
{
    [SerializeField]
    private LocalResource resource;
    protected override IResource value => resource;
}
