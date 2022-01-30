using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Extension of slider that modifies a resource.
/// </summary>
public abstract class ResourceSlider : Slider
{
    protected abstract IResource value { get; }

    protected override void Start()
    {
        onValueChanged.AddListener(OnSliderValueChanged);
    }

    protected override void OnDestroy()
    {
        onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    protected virtual void OnSliderValueChanged(float value)
    {
        this.value.SetValue(value);
    }
}
