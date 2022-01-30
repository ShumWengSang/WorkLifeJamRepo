using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PetCat : MonoBehaviour
{
    public LocalResource petPoints;
    public LeverUI lever;
    public Slider slider;
    public CursorEvents cursor;
    public Animator handAnimator;
    public void PetPointsUp()
    {
        var val = (100f / 10f) * Time.deltaTime;
        petPoints.Add(val);
        if (petPoints.GetCurrent() >= 100)
        {
            //update stats
            //happy cat particles here
        }
    }

    private void Update()
    {
        if (slider.value >= .99f)
        {
            cursor.BreakHold();
            //full pet here
            //cat pet stuff here

        }

    }

    public void ResetSlider()
    {
        DOTween.To(()=>slider.value,(float something)=>slider.value=something, 0, .1f);
        lever.ResetPercentage();
        handAnimator.SetTrigger("StopPetting");
    }

    public void killTweens()
    {
        DOTween.Kill(this);
    }
}
