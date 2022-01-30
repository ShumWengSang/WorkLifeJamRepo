using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public CameraVertical verticalCamMovement;
    public CameraHorizontal topHorizontal;
    public CameraHorizontal bottomHorizontal;

    public Button arrowUp;
    public Button arrowDown;
    public Button arrowLeft;
    public Button arrowRight;

    private DayManager dayManager;

    private void Start()
    {
        var dayManager = GameObject.FindObjectOfType<DayManager>();
        // dayManager.DayStarted += DayManagerOnDayStarted;
        // Evaluate whether to turn on/off arrows
        ToggleHorizontalArrows();
        ToggleVerticalArrows();
    }

    private void DayManagerOnDayStarted(object sender, IntEventArgs e)
    {
        int amountKilled = DOTween.Kill(this.transform, true);
        Debug.Log("Killed " + amountKilled.ToString());
        topHorizontal.SetCameraPosX();
        verticalCamMovement.SetCameraPosY();
    }

    public void Update()
    {
        if (!Player.CanInput)
            return;

        if(Input.GetKeyDown(KeyCode.W))
        {
            verticalCamMovement.MoveToTop();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            verticalCamMovement.MoveToBottom();
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
    }

    private void ToggleHorizontalArrows()
    {
        CameraHorizontalUIArrowsDetermineInteractable(topHorizontal);
        CameraHorizontalUIArrowsDetermineInteractable(bottomHorizontal);
    }

    private void ToggleVerticalArrows()
    {
        if (verticalCamMovement.CanGoBottom())
        {
            arrowUp.interactable = false;
            arrowDown.interactable = true;
        }
        else if (verticalCamMovement.CanGoTop())
        {
            arrowUp.interactable = true;
            arrowDown.interactable = false;
        }
    }

    private void CameraHorizontalUIArrowsDetermineInteractable(CameraHorizontal cameraHorizontal)
    {
        if (cameraHorizontal.isActiveAndEnabled)
        {
            arrowLeft.interactable = true;
            arrowRight.interactable = true;
            if (!cameraHorizontal.CanMoveLeft())
            {
                // Turn arrow left off
                arrowLeft.interactable = false;
            }
            else if (!cameraHorizontal.CanMoveRight())
            {
                // Turn right arrow off
                arrowRight.interactable = false;
            }
        }
    }

    public void MoveLeft()
    {
        if (!Player.CanInput)
            return;
        if (topHorizontal.isActiveAndEnabled)
        {
            topHorizontal.MoveLeft();
        }
        else
        {
            bottomHorizontal.MoveLeft();
        }
        // Evaluate whether to turn on/off arrows
        ToggleHorizontalArrows();
    }

    public void MoveRight()
    {
        if (!Player.CanInput)
            return;
        if (topHorizontal.isActiveAndEnabled)
        {
            topHorizontal.MoveRight();
        }
        else
        {
            bottomHorizontal.MoveRight();
        }
        ToggleHorizontalArrows();
    }

    public void ToggleUpDownMovement()
    {
        if (!Player.CanInput)
            return;
        verticalCamMovement.ToggleMoveTilesVertical();
        ToggleVerticalArrows();
    }

    public void KillTweensHere()
    {
        Debug.Log("Tweens Killed " + DOTween.Kill(this.verticalCamMovement, true).ToString());
    }
}
