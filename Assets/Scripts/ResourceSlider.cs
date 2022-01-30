using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Extension of slider that modifies a resource.
/// </summary>
[RequireComponent(typeof(Slider))]
public abstract class ResourceSlider : MonoBehaviour
{
    protected abstract IResource value { get; }
    private Slider slider { get; set; }

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    protected virtual void OnSliderValueChanged(float value)
    {
        this.value.SetValue(value);
    }
}
