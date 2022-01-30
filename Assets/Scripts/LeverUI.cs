using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Slider)), RequireComponent(typeof(LineRenderer))]
public class LeverUI : MonoBehaviour
{


    private Slider slider;
    private LineRenderer lineRenderer;

    private RectTransform slideArea;

    [Range(0, 1)]
    public float resistance;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.enabled = false;
        slideArea = (RectTransform)slider.handleRect.parent;
    }

    
    
    public void OnPointerDown()
    {
        lineRenderer.enabled = true;
        Debug.Log("Enable Line Renderer");
    }

    public void OnPointerUp()
    {
        lineRenderer.enabled = false;
        Debug.Log("Disable Line Renderer");
    }

    public void OnPointerDrag()
    {
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Line from handle to mouse position
        lineRenderer.positionCount = 2;

        // Position of the handle
        var position = slider.handleRect.transform.position;
        // Slide it forward a bit so it renders
        position += Vector3.back * 10;
        lineRenderer.SetPosition(0, position);
        
        // Project the mouse position to the plane
        Vector3 normal = Vector3.back;
        Vector3 u = mousePositionWorld - position;
        Vector3 projection = Vector3.Dot(u, normal) / Mathf.Pow(Vector3.Dot(normal, normal), 2) * normal;
        Vector3 point = mousePositionWorld - projection;
        
        // Offset the Z so it renders
        lineRenderer.SetPosition(1, point);


        var rect = slideArea.rect;
        float mouseWidth = point.x - slideArea.TransformPoint(rect.min.x, rect.min.y, 0).x;

        // Find percentange of width
        float goalPercentage = mouseWidth / slideArea.rect.width;
        prevPercentage += (goalPercentage - prevPercentage) * Time.deltaTime * (1 - resistance);
        
        // Set value
        slider.value = Mathf.Clamp(prevPercentage, 0f, 1f);
    }

    private float prevPercentage;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Break();
        }
    }
}
