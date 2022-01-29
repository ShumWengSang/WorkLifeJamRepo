using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CursorEvents : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent cursorDown;
    public UnityEvent cursorUp;
    public UnityEvent cursorEnter;
    public UnityEvent cursorExit;
    public UnityEvent cursorStay;

    private Coroutine cursorDownCoroutine { get; set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        cursorDown.Invoke();

        if (cursorDownCoroutine != null)
            return;

        cursorDownCoroutine = StartCoroutine(CursorDownLoop());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cursorEnter.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cursorExit.Invoke();
    }

    private IEnumerator CursorDownLoop()
    {
        while (this.enabled)
        {
            if (Input.GetMouseButtonUp(0))
            {
                cursorUp.Invoke();
                break;
            }

            cursorStay.Invoke();

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
