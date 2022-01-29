using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior that invokes events when a value reaches certain thresholds.
/// </summary>
public abstract class ResourceEvents : MonoBehaviour
{
    public static bool CanTrigger { get; set; } = false;
    protected abstract IResource value { get; }

    [Header("Events", order = 1)]
    [SerializeField]
    [Tooltip("Events invoked when the value reaches a certain threshold.")]
    private List<ValueThresholdEvent> thresholdEvents = new List<ValueThresholdEvent>();

    [SerializeField]
    [Tooltip("Events invoked when the value accrues a certain positive or negative total.")]
    private List<ValueAccrueEvent> accrueEvents = new List<ValueAccrueEvent>();

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
        value.ValueChanged += OnPercentValueChanged;
    }

    protected virtual void OnPercentValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (!ResourceEvents.CanTrigger)
            return;

        UpdateThresholdEvents((IResource)sender, e);
        UpdateAccrueEvents(e);
    }

    private void UpdateAccrueEvents(ValueChangedEventArgs e)
    {
        foreach (ValueAccrueEvent accrueEvent in accrueEvents)
        {
            // If both are positive or both are negative, add the change in value.
            // (Don't want to subtract from a positive accrue goal and vice-versa)
            if (accrueEvent.accrueTarget.IsPositive() == e.ValueDelta.IsPositive())
                accrueEvent.Accrue(e.ValueDelta);
        }
    }

    private void UpdateThresholdEvents(IResource valueProvider, ValueChangedEventArgs e)
    {
        // Convert values from literals to percentages.
        float maxValue = valueProvider.GetMaximum();
        float previousValue = e.PreviousValue / maxValue;
        float currentValue = e.CurrentValue / maxValue;

        foreach (ValueThresholdEvent thresholdEvent in thresholdEvents)
        {
            float thresholdValue = thresholdEvent.thresholdValue;

            if (!previousValue.To(currentValue).Contains(thresholdValue))
                continue;

            thresholdEvent.Invoke(currentValue);
        }
    }
}
