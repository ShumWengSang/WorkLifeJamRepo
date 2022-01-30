using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    public float randomReductionSize = 5;
    public void Wander()
    {
        var randWander = Random.Range(minWanderTime, maxWanderTime);
        Wandering = true;

        var X_box = range.TransformPoint(range.rect.x, 0, 0).x - randomReductionSize;
        var width_box = range.TransformDirection(range.rect.width,0,0).x - randomReductionSize;
        var y_box = range.TransformPoint(0, range.rect.y, 0).y - randomReductionSize;
        var height_box = range.TransformDirection(0, range.rect.height, 0).y - randomReductionSize;

        Debug.DrawLine(new Vector2(X_box, y_box), new Vector2(X_box+width_box, y_box), Color.red, randWander);
        Debug.DrawLine(new Vector2(X_box, y_box), new Vector2(X_box, y_box+height_box), Color.green, randWander);

        var x = Random.Range(X_box, X_box + width_box);
        var y = Random.Range(y_box, y_box + height_box);



        myTransform.DOMove(new Vector3(x,y,0), randWander);
        Invoke(nameof(Wander), randWander);
    }

    private void Update()
    {
        if(Wandering == true && onlyOnInteractable && hasButton && button.interactable == false)
        {
            Wandering = false;
            DOTween.Kill(this);
            CancelInvoke(nameof(Wander));
        }

        if(Wandering == false && (hasButton && button.interactable == true || onlyOnInteractable == false))
        {
            Wander();
        }

        if (Wandering == false && alwaysWandering)
        {
            Wander();
        }
    }
}
