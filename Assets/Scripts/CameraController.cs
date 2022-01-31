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
        dayManager.DayStarted += DayManagerOnDayStarted;
        // Evaluate whether to turn on/off arrows
        OnStart();

        verticalCamMovement.Moved += Moved;
        topHorizontal.Moved += Moved;
        bottomHorizontal.Moved += Moved;
    }

    public void OnStart()
    {
        ToggleHorizontalArrows();
        ToggleVerticalArrows();
    }

    private void DayManagerOnDayStarted(object sender, IntEventArgs e)
    {
        int amountKilled = DOTween.Kill(this.transform, true);
        Debug.Log("Killed " + amountKilled.ToString());

        if (verticalCamMovement.CanGoTop())
        {
            ToggleUpDownMovement();
        }
        topHorizontal.MoveToStartingTile();
        ToggleHorizontalArrows();
        ToggleVerticalArrows();
    }

    public void Update()
    {
        if (!Player.CanInput)
            return;
        if (Player.IsPaused)
            return;

        if(Input.GetKeyDown(KeyCode.W))
        {
            if (!verticalCamMovement.CanGoTop())
                return;

            ToggleUpDownMovement();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (!verticalCamMovement.CanGoBottom())
                return;

            ToggleUpDownMovement();
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
        arrowUp.gameObject.SetActive(verticalCamMovement.CanGoTop());
        arrowDown.gameObject.SetActive(verticalCamMovement.CanGoBottom());
    }

    private void CameraHorizontalUIArrowsDetermineInteractable(CameraHorizontal cameraHorizontal)
    {
        if (cameraHorizontal.isActiveAndEnabled)
        {
            arrowLeft.gameObject.SetActive(cameraHorizontal.CanMoveLeft());
            arrowRight.gameObject.SetActive(cameraHorizontal.CanMoveRight());
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
        verticalCamMovement.ToggleMoveTilesVertical();
        ToggleVerticalArrows();
        ToggleHorizontalArrows();
    }

    public void KillTweensHere()
    {
        Debug.Log("Tweens Killed " + DOTween.Kill(this.verticalCamMovement, true).ToString());
    }
}
