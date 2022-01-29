using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using RoboRyanTron.QuickButtons;

public class CameraHorizontal : MonoBehaviour
{
    public Transform[] tiles;
    public int tileIndex = 0;
    public DG.Tweening.Ease easing;

    [Range(0, 1)]
    public float duration = 0.25f;

    public QuickButton setCameraPosX = new QuickButton(input =>
    {
        CameraHorizontal demo = input as CameraHorizontal;
        demo.transform.position = new Vector3(demo.GetCurrTile().transform.position.x, demo.transform.position.y, demo.transform.position.z);
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
            if (tileIndex >= tiles.Length - 1)
                return;

            tileIndex++;
            MoveHorizontalTiles();
        }
    }

    public Transform GetCurrTile()
    {
        return tiles[tileIndex];
    }

    private void MoveHorizontalTiles()
    {
        Debug.Log(tiles[tileIndex].transform.position.x.ToString());
        this.transform.DOMoveX(tiles[tileIndex].transform.position.x, duration).SetEase(easing).OnComplete(() => { Player.CanInput = true; }); ;
        Player.CanInput = false;
    }
}
