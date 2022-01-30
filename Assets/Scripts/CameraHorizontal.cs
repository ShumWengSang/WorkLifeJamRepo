using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using DG.Tweening;
using RoboRyanTron.QuickButtons;

public class CameraHorizontal : MonoBehaviour
{
    public event EventHandler Moved;

    public Transform parentTile;
    public int tileIndex = 0;
    public DG.Tweening.Ease easing;

    [Min(0f)]
    public float duration = 0.25f;

    public QuickButton setCameraPosX = new QuickButton(SetCameraPosX);

    public static void SetCameraPosX(object input)
    {
        CameraHorizontal demo = input as CameraHorizontal;
        demo.transform.position = new Vector3(demo.GetCurrTile().transform.position.x + 20, demo.transform.position.y, demo.transform.position.z);
    }
    // Copy and pasted
    public void SetCameraPosX()
    {
        transform.position = new Vector3(GetCurrTile().transform.position.x + 20, transform.position.y, transform.position.z);
    }

    public void MoveRight()
    {
        if (!CanMoveRight())
            return;

        tileIndex++;
        MoveHorizontalTiles();
    }

    public void MoveLeft()
    {
        if (!CanMoveLeft())
            return;

        tileIndex--;
        MoveHorizontalTiles();
        return;
    }

    public void SetIndex(int index)
    {
        tileIndex = index;
    }

    public void MoveToStartingTile()
    {
        tileIndex = 0;
        this.DoAfterDelay(MoveHorizontalTiles, 0.2f);
    }

    public int GetActiveTiles()
    {
        return parentTile.GetActiveChildren();
    }

    public Transform GetCurrTile()
    {
        return parentTile.GetActiveChild(tileIndex);
    }

    private void MoveHorizontalTiles()
    {
        this.transform.DOMoveX(GetCurrTile().transform.position.x+20, duration).SetEase(easing).OnComplete(() => 
        { 
            Player.CanInput = true;
            Moved?.Invoke(this, EventArgs.Empty);
        }); ;
        Player.CanInput = false;
    }

    public bool CanMoveLeft()
    {
        return tileIndex > 0;
    }

    public bool CanMoveRight()
    {
        return tileIndex < GetActiveTiles() - 1;
    }
}
