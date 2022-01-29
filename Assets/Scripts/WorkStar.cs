using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class WorkStar : MonoBehaviour
{
    public void FilledIn()
    {
        gameObject.GetComponent<Image>().color = Color.white;
    }

    public void Missing()
    {
        gameObject.GetComponent<Image>().color = Color.black;
    }
}
