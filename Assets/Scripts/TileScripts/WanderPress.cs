using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Random = UnityEngine.Random;

public class WanderPress : MonoBehaviour
{
    public RectTransform range;
    public bool alwaysWandering = false;

    public bool onlyOnInteractable;
    private bool hasButton;
    private Button button;
    private Transform myTransform;

    public float minWanderTime = 2f;
    public float maxWanderTime = 4f; //yeee-maybe

    private bool Wandering = false;
    private Vector3 initialPos;
    private void Awake()
    {
        if (maxWanderTime < minWanderTime) maxWanderTime = minWanderTime;

        button = gameObject.GetComponent<Button>();
        if(button != null)
        {
            hasButton = true;
        }

        
        myTransform = gameObject.transform;

    }

    private void Start()
    {
        initialPos = transform.position;
    }

    public Vector2 randomReductionSize;
    public void Wander()
    {
        var randWander = Random.Range(minWanderTime, maxWanderTime);
        Wandering = true;

        var X_box = range.TransformPoint(range.rect.x, 0, 0).x - randomReductionSize.x;
        var width_box = range.TransformDirection(range.rect.width,0,0).x - randomReductionSize.x;
        var y_box = range.TransformPoint(0, range.rect.y, 0).y - randomReductionSize.y;
        var height_box = range.TransformDirection(0, range.rect.height, 0).y + randomReductionSize.y;

        Debug.DrawLine(new Vector2(X_box, y_box), new Vector2(X_box+width_box, y_box), Color.red, randWander);
        Debug.DrawLine(new Vector2(X_box, y_box), new Vector2(X_box, y_box+height_box), Color.green, randWander);

        var x = Random.Range(X_box, X_box + width_box);
        var y = Random.Range(y_box, y_box + height_box);



        myTransform.DOMove(new Vector3(x,y,0), randWander).OnComplete(() => { Wandering = false;});
    }

    private void Update()
    {
        if(Wandering == true && onlyOnInteractable && hasButton && button.interactable == false && !alwaysWandering)
        {
            Debug.Log("Destroy");
            Wandering = false;
            DOTween.Kill(this);
        }

        else if(Wandering == false && (hasButton && button.interactable == true || onlyOnInteractable == false))
        {
            Wander();
        }

        else if (Wandering == false && alwaysWandering)
        {
            Wander();
        }
        
    }

    public void ResetPositions()
    {
        transform.position = initialPos;
    }
}
