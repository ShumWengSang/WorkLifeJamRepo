using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public event EventHandler Moved;

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

        verticalCamMovement.Moved += Moved;
        topHorizontal.Moved += Moved;
        bottomHorizontal.Moved += Moved;
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
        if (Player.IsPaused)
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
            arrowUp.gameObject.SetActive(false);
            arrowDown.gameObject.SetActive(true);
        }
        else if (verticalCamMovement.CanGoTop())
        {
            arrowUp.gameObject.SetActive(true);
            arrowDown.gameObject.SetActive(false);
        }
    }

    private void CameraHorizontalUIArrowsDetermineInteractable(CameraHorizontal cameraHorizontal)
    {
        if (cameraHorizontal.isActiveAndEnabled)
        {
            arrowLeft.gameObject.SetActive(true);
            arrowRight.gameObject.SetActive(true);
            if (!cameraHorizontal.CanMoveLeft())
            {
                // Turn arrow left off
                arrowLeft.gameObject.SetActive(false);
            }
            else if (!cameraHorizontal.CanMoveRight())
            {
                // Turn right arrow off
                arrowRight.gameObject.SetActive(false);
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
