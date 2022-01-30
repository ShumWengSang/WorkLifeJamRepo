using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorEvents : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public CostUnityEvent cursorDown;
    public CostUnityEvent cursorUp;
    public CostUnityEvent cursorEnter;
    public CostUnityEvent cursorExit;
    public CostUnityEvent cursorHold;

    private Button button;
    private bool hasButton = false;

    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        if(button != null)
        {
            hasButton = true;
        }
    }

    private Coroutine cursorDownCoroutine { get; set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (hasButton && button.interactable == false) return;

        cursorDown.Invoke();

        if (cursorDownCoroutine != null)
            return;

        cursorDownCoroutine = StartCoroutine(CursorDownLoop());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hasButton && button.interactable == false) return;

        cursorEnter.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hasButton && button.interactable == false) return;

        cursorExit.Invoke();
    }

    public void BreakHold() { breakhold = true; }
    private bool breakhold = false;

    private IEnumerator CursorDownLoop()
    {


        while (this.enabled)
        {
            if (Input.GetMouseButtonUp(0) || breakhold || (hasButton && button.interactable == false))
            {
                breakhold = false;
                cursorUp.Invoke();
                break;
            }

            cursorHold.Invoke();

            yield return null;
        }

        cursorDownCoroutine = null;
        yield break;
    }

    public void TestLogDown()
    {
        Debug.Log("Cursor Down");
    }

    public void TestLogUp()
    {
        Debug.Log("Cursor Up");
    }

    public void TestLogEnter()
    {
        Debug.Log("Cursor Enter");
    }

    public void TestLogExit()
    {
        Debug.Log("Cursor Exit");
    }

    public void TestLogStay()
    {
        Debug.Log("Cursor Stay");
    }
}
