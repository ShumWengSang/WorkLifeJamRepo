using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using RoboRyanTron.QuickButtons;

public class CameraVertical : MonoBehaviour
{
    public Transform middleRoad;

    public CameraHorizontal topTileMovement;
    public CameraHorizontal bottomTileMovement;

    [Min(0f)]
    public float toRoadDuration = 0.2f;

    [Min(0f)]
    public float toTileDuration = 0.2f;

    [Min(0f)]
    public float horiDuration = 0.2f;

    public QuickButton setCameraPosY = new QuickButton(input =>
    {
        CameraVertical demo = input as CameraVertical;
        demo.transform.position = new Vector3(demo.transform.position.x + 20, demo.topTileMovement.GetCurrTile().transform.position.y, demo.transform.position.z);
    });

    // Update is called once per frame
    void Update()
    {
        if (!Player.CanInput)
            return;

        if(Input.GetKeyDown(KeyCode.W))
        {
            if (topTileMovement.GetActiveTiles() <= 0)
                return;

            MoveToTop();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (bottomTileMovement.GetActiveTiles() <= 0)
                return;

            MoveToBottom();
        }
    }

    public void MoveToBottom()
    {
        if (bottomTileMovement.isActiveAndEnabled)
            return;

        SwapTiles();
        Sequence seq = DOTween.Sequence();
        seq.Append(this.transform.DOMoveY(middleRoad.position.y, toRoadDuration));
        seq.Append(this.transform.DOMoveX(bottomTileMovement.GetCurrTile().position.x + 20, horiDuration));
        seq.Append(this.transform.DOMoveY(bottomTileMovement.GetCurrTile().position.y, toTileDuration));
    }

    public void MoveToTop()
    {
        if (topTileMovement.isActiveAndEnabled)
            return;

        SwapTiles();
        Sequence seq = DOTween.Sequence();
        seq.Append(this.transform.DOMoveY(middleRoad.position.y, toRoadDuration));
        seq.Append(this.transform.DOMoveX(topTileMovement.GetCurrTile().position.x + 20, horiDuration));
        seq.Append(this.transform.DOMoveY(topTileMovement.GetCurrTile().position.y, toTileDuration));
    }

    void SwapTiles()
    {
        topTileMovement.enabled = !topTileMovement.enabled;
        bottomTileMovement.enabled = !topTileMovement.enabled;
    }
}
