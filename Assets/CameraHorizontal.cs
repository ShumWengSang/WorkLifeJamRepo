using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraHorizontal : MonoBehaviour
{
    public Transform[] tiles;
    public int tileIndex = 0;
    public DG.Tweening.Ease easing;

    [Range(0, 1)]
    public float duration = 0.25f;

    private bool canProcessInput = true;

    // Start is called before the first frame update
    void Start()
    {
        MoveHorizontalTiles();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canProcessInput)
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
        this.transform.DOMoveX(tiles[tileIndex].transform.position.x, duration).SetEase(easing).OnComplete(() => { canProcessInput = true; }); ;
        canProcessInput = false;
    }
}
