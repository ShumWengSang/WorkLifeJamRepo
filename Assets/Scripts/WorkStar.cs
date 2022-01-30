using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WorkStar : MonoBehaviour
{
    public Transform mask;
    public Transform fill;
    public ParticleSystem particles;

    public bool fillOnAwake;

    private void Awake()
    {
        if(fillOnAwake)
        {
            FilledIn();
        }
        else
        {
            Missing();
        }
    }

    public void FillLevel(float percent)
    {
        mask.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 75f*(1f-percent));
        fill.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    public void FilledIn()
    {
        mask.position = gameObject.transform.position; 
        fill.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    public void Missing()
    {
        mask.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 75f);
        fill.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    public float celebrateTimer = 1f;

    public void FillUpCelebrate()
    {
        //Put particle effects here
        Invoke(nameof(PlayParticles), celebrateTimer-.1f);
        mask.DOMoveY(gameObject.transform.position.y, celebrateTimer);
        fill.DOMoveY(gameObject.transform.position.y, celebrateTimer);
    }

    public void PlayParticles()
    {
        particles.Play();
    }
}
