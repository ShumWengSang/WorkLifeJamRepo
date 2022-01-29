using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ResourceDisplay : MonoBehaviour
{
    /*
     * This painful inheritance chain is only to handle the interface 
     * references between the LocalResource (MonoBehaviour) and the 
     * GlobalResource (ScriptableObject). This is unfortunately the
     * best workaround for the fact that Unity can't serialize
     * UnityEngine.Object by an interface.
     */

    protected abstract IResource value { get; }

    [SerializeField]
    private Image fillImage;

    private void Start()
    {
        if (value == null)
        {
            Debug.LogWarning($"{gameObject}'s {nameof(LocalResourceEvents)} component " +
                $"does not reference a {nameof(LocalResource)}. " +
                $"Is this intended?");
            return;
        }

        RegisterValueChangeEvents();

        SetFillByValue(value);
    }

    private void OnDestroy()
    {
        if (value == null)
            return;

        DeregisterValueChangeEvents();
    }

    private void RegisterValueChangeEvents()
    {
        value.ValueChanged += OnPercentValueChanged;
    }

    private void DeregisterValueChangeEvents()
    {
        value.ValueChanged -= OnPercentValueChanged;
    }

    protected virtual void OnPercentValueChanged(object sender, ValueChangedEventArgs e)
    {
        IResource valueProvider = (IResource)sender;
        SetFillByValue(valueProvider);
    }

    private void SetFillByValue(IResource valueProvider)
    {
        float maxValue = valueProvider.GetMaximum();
        float currentValue = valueProvider.GetCurrent();

        fillImage.fillAmount = currentValue / maxValue;
    }
}
