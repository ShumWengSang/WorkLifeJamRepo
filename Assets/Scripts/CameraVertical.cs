using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

using RoboRyanTron.QuickButtons;

public class CameraVertical : MonoBehaviour
{
    public event EventHandler Moved;

    public Transform middleRoad;

    public CameraHorizontal topTileMovement;
    public CameraHorizontal bottomTileMovement;

    public Image background;

    [Min(0f)]
    public float toRoadDuration = 0.2f;

    [Min(0f)]
    public float toTileDuration = 0.2f;

    [Min(0f)]
    public float horiDuration = 0.2f;
    
    private Sequence seq;

    public QuickButton setCameraPosY = new QuickButton(input =>
    {
        CameraVertical demo = input as CameraVertical;
        var position = demo.transform.position;
        position = new Vector3(position.x + 20, demo.topTileMovement.GetCurrTile().transform.position.y, position.z);
        demo.transform.position = position;
    });
    // Copy and pasted from above
    public void SetCameraPosY()
    {
        var position = transform.position;
        position = new Vector3(position.x + 20, topTileMovement.GetCurrTile().transform.position.y, position.z);
        transform.position = position;
    }

    public void ToggleMoveTilesVertical()
    {
        if (CanGoTop())
        {
            MoveToTop();
        }
        else if (CanGoBottom())
        {
            MoveToBottom();
        }
    }

    public void MoveToBottom()
    {
        if (bottomTileMovement.GetActiveTiles() <= 0)
            return;
        if (!CanGoBottom())
            return;
        Player.CanInput = false;
        SwapTiles();
        Sequence seq = DOTween.Sequence();
        seq.Append(this.transform.DOMoveY(middleRoad.position.y, toRoadDuration));
        seq.Append(this.transform.DOMoveX(bottomTileMovement.GetCurrTile().position.x + 20, horiDuration));
        seq.Append(this.transform.DOMoveY(bottomTileMovement.GetCurrTile().position.y, toTileDuration));
        seq.OnComplete(() => 
        { 
            Player.CanInput = true;
            Moved?.Invoke(this, EventArgs.Empty);
        });

        background.DOColor(Color.clear, toRoadDuration+ horiDuration);

    }
    

    public void MoveToTop()
    {
        if (topTileMovement.GetActiveTiles() <= 0)
            return;
        if (!CanGoTop())
            return;
        Player.CanInput = false;
        SwapTiles();
        
        seq = DOTween.Sequence();
        seq.Append(this.transform.DOMoveY(middleRoad.position.y, toRoadDuration));
        seq.Append(this.transform.DOMoveX(topTileMovement.GetCurrTile().position.x + 20, horiDuration));
        seq.Append(this.transform.DOMoveY(topTileMovement.GetCurrTile().position.y, toTileDuration));
        seq.OnComplete(() => { Player.CanInput = true; });
        seq.SetId("VerticalMovement");
        background.DOColor(Color.white, toRoadDuration + horiDuration );
    }

    void SwapTiles()
    {
        var enabled1 = topTileMovement.enabled;
        enabled1 = !enabled1;
        topTileMovement.enabled = enabled1;
        bottomTileMovement.enabled = !enabled1;
    }

    public bool CanGoTop()
    {
        return !topTileMovement.isActiveAndEnabled && topTileMovement.GetActiveTiles() > 0;
    }

    public bool CanGoBottom()
    {
        return !bottomTileMovement.isActiveAndEnabled && bottomTileMovement.GetActiveTiles() > 0;
    }

    public void ResetInternal()
    {
        background.color = Color.white;
    }

    public void KillSequence()
    {
        Debug.Log("NUKE");
        seq?.Kill(true);
        DOTween.Kill("VerticalMovement");
        DOTween.KillAll(true);
    }
}
