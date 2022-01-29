using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class FadeController : MonoBehaviour
{
    private Image myImage;
    public Color onColor;
    public Color offColor;

    private void Awake()
    {
        myImage = gameObject.GetComponent<Image>();
    }

    public void FadeIn(float fadeInSpeed)
    {
        myImage.DOColor(onColor, fadeInSpeed);
        Invoke(nameof(TurnOnRayCastBlocking), fadeInSpeed);
    }

    public void FadeIn(float fadeInSpeed, DG.Tweening.Ease easing)
    {
        myImage.DOColor(onColor, fadeInSpeed).SetEase(easing);
        Invoke(nameof(TurnOnRayCastBlocking), fadeInSpeed);
    }

    public void FadeOut(float fadeOutSpeed)
    {
        TurnOffRayCastBlocking();
        myImage.DOColor(offColor, fadeOutSpeed);
    }

    public void TurnOnRayCastBlocking( )
    {
        myImage.raycastTarget = true;
    }

    public void TurnOffRayCastBlocking()
    {
        myImage.raycastTarget = false;
    }
}
