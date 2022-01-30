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

    private Coroutine fadeCoroutine { get; set; }

    private void Awake()
    {
        myImage = gameObject.GetComponent<Image>();
    }

    public void FadeIn(float fadeInSpeed)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeCoroutine(offColor, onColor, fadeInSpeed));

        Invoke(nameof(TurnOnRayCastBlocking), fadeInSpeed);
    }

    public void FadeIn(float fadeInSpeed, DG.Tweening.Ease easing)
    {
        myImage.DOColor(onColor, fadeInSpeed).SetEase(easing);
        Invoke(nameof(TurnOnRayCastBlocking), fadeInSpeed);
    }

    public void FadeOut(float fadeOutSpeed)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeCoroutine(onColor, offColor, fadeOutSpeed));

        TurnOffRayCastBlocking();
    }

    public void TurnOnRayCastBlocking( )
    {
        myImage.raycastTarget = true;
    }

    public void TurnOffRayCastBlocking()
    {
        myImage.raycastTarget = false;
    }

    private IEnumerator FadeCoroutine(Color a, Color b, float duration)
    {
        float timer = 0f;
        while (timer <= duration)
        {
            timer += Time.deltaTime;

            myImage.color = Color.Lerp(a, b, timer / duration);

            yield return new WaitForEndOfFrame();
        }
    }
}
