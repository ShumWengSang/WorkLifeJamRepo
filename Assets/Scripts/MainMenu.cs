using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public GameObject fadeElement;
    public Color opaqueEnd;

    public float fadeDuration = .5f;

    public void Fade_PlayGame()
    {
        var image = fadeElement.GetComponent<Image>();

        if(image != null)
        {
            image.DOColor(opaqueEnd, fadeDuration);
        }

        Invoke(nameof(PlayGame), fadeDuration);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
