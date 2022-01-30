using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using RoboRyanTron.QuickButtons;

public class CameraHorizontal : MonoBehaviour
{
    public Transform parentTile;
    public int tileIndex = 0;
    public DG.Tweening.Ease easing;

    [Min(0f)]
    public float duration = 0.25f;

    public QuickButton setCameraPosX = new QuickButton(input =>
    {
        CameraHorizontal demo = input as CameraHorizontal;
        demo.transform.position = new Vector3(demo.GetCurrTile().transform.position.x+20, demo.transform.position.y, demo.transform.position.z);
    });

    // Update is called once per frame
    void Update()
    {
        if (!Player.CanInput)
            return;

        if(Input.GetKeyDown(KeyCode.A))
        {
            if (tileIndex < 1)
                return;

            tileIndex--;
            MoveHorizontalTiles();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (tileIndex >= GetActiveTiles() - 1)
                return;

            tileIndex++;
            MoveHorizontalTiles();
        }
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
        this.transform.DOMoveX(GetCurrTile().transform.position.x+20, duration).SetEase(easing).OnComplete(() => { Player.CanInput = true; }); ;
        Player.CanInput = false;
    }
}
