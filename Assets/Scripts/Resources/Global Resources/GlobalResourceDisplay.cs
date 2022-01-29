using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalResourceDisplay : ResourceDisplay
{
    [SerializeField]
    private GlobalResource resource;

    protected override IResource value => resource;
}
