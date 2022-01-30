/*******************************************************************************
File:      AudioParameterAdjuster.cs
Author:    Joseph Crump
DP Email:  joseph.crump@digipen.edu
Date:      6/29/2021
Course:    DES302
Section:   A

Description:
    Behavior that modifies an exposed parameter from an audio mixer.
    
    © 2021 DigiPen Institute of Technology, all rights reserved

*******************************************************************************/
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class AudioParameterAdjuster : MonoBehaviour
{
    public Image TargetGrapic;
    public Sprite UnmutedSprite;
    public Sprite MutedSprite;

    public AudioMixer Mixer;
    public string ExposedParameterName = "MusicVolume";

    [SerializeField]
    private float MinValue = -80f;
    [SerializeField]
    private float MaxValue = 20f;
    [SerializeField]
    private float DefaultValue = 0f;
    public AnimationCurve InterpolantCurve;

    public float ParameterValue
    {
        get 
        { 
            if (Mixer.GetFloat(ExposedParameterName, out float value))
                return value;

            // Default
            return 0f;
        }
        set
        {
            value = Mathf.Clamp(value, MinValue, MaxValue);
            Mixer.SetFloat(ExposedParameterName, value);
        }
    }
    public bool IsMuted { get { return ParameterValue == MinValue ? true : false; } }

    [Space]

    [Tooltip("Event that is invoked whenever the parameter value is muted.")]
    public UnityEvent Muted = new UnityEvent();
    [Tooltip("Event that is invoked whenever the parameter value is unmuted.")]
    public UnityEvent Unmuted = new UnityEvent();

    /// <summary>
    /// Reset the slider position based on the current parameter value.
    /// </summary>
    public void RefreshSlider(Slider slider)
    {
        slider.value = GetInterpolantFromValue(ParameterValue);
    }

    /// <summary>
    /// Set the exposed parameter using the InterpolantCurve from 0-100%.
    /// </summary>
    public void SetValue(float interpolant)
    {
        interpolant = Mathf.Clamp01(interpolant);
        float value = InterpolantCurve.Evaluate(interpolant);

        // Do nothing if value did not change
        if (value == ParameterValue)
            return;

        ParameterValue = value;

        if (ParameterValue == MinValue)
        {
            Muted?.Invoke();
            SetSprite(MutedSprite);
        }
        else if (ParameterValue > MinValue)
        {
            SetSprite(UnmutedSprite);
        }
    }

    /// <summary>
    /// Set the exposed parameter using a slider value.
    /// </summary>
    public void SetValue(Slider slider)
    {
        SetValue(slider.value);
    }

    /// <summary>
    /// Inverts the current mute status.
    /// </summary>
    public void ToggleMute()
    {
        Mute(!IsMuted);
    }

    /// <summary>
    /// Mute/unmute by setting the exposed value to either MinVolume or DefaultVolume.
    /// </summary>
    public void Mute(bool value)
    {
        if (value == IsMuted)
            return;

        if (value)
            SetValue(0f);
        else if (!value)
        {
            SetValue(GetInterpolantFromValue(DefaultValue));
            Unmuted?.Invoke();
        }
    }

    /// <summary>
    /// Get the interpolant value from the interpolant curve from the given float value.
    /// </summary>
    private float GetInterpolantFromValue(float value)
    {
        float interpolant = GetInverseCurve().Evaluate(value);
        return interpolant;
    }

    private AnimationCurve GetInverseCurve()
    {
        AnimationCurve inverseCurve = new AnimationCurve();
        foreach (var key in InterpolantCurve.keys)
            inverseCurve.AddKey(key.value, key.time);

        return inverseCurve;
    }

    /// <summary>
    /// Change the sprite of the TargetGraphic.
    /// </summary>
    private void SetSprite(Sprite sprite)
    {
        TargetGrapic.sprite = sprite;
    }
}
