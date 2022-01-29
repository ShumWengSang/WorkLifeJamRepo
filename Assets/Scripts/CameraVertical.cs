using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraVertical : MonoBehaviour
{
    public Transform topTile;
    public Transform bottomTile;
    public Transform middleRoad;

    public CameraHorizontal topTileMovement;
    public CameraHorizontal bottomTileMovement;

    [Range(0.0f, 1)]
    public float toRoadDuration = 0.2f;

    [Range(0.0f, 1)]
    public float toTileDuration = 0.2f;

    [Range(0.0f, 1)]
    public float horiDuration = 0.2f;
    // Update is called once per frame
    void Update()
    {
        if (!Player.CanInput)
            return;

        if(Input.GetKeyDown(KeyCode.W))
        {
            if (topTileMovement.isActiveAndEnabled)
                return;

            SwapTiles();
            Sequence seq = DOTween.Sequence();
            seq.Append(this.transform.DOMoveY(middleRoad.position.y, toRoadDuration));
            seq.Append(this.transform.DOMoveX(topTileMovement.GetCurrTile().position.x, horiDuration));
            seq.Append(this.transform.DOMoveY(topTileMovement.GetCurrTile().position.y, toTileDuration));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (bottomTileMovement.isActiveAndEnabled)
                return;

            SwapTiles();
            Sequence seq = DOTween.Sequence();
            seq.Append(this.transform.DOMoveY(middleRoad.position.y, toRoadDuration));
            seq.Append(this.transform.DOMoveX(bottomTileMovement.GetCurrTile().position.x, horiDuration));
            seq.Append(this.transform.DOMoveY(bottomTileMovement.GetCurrTile().position.y, toTileDuration));
        }
    }

    void SwapTiles()
    {
        topTileMovement.enabled = !topTileMovement.enabled;
        bottomTileMovement.enabled = !topTileMovement.enabled;
    }
}
