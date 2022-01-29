using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WiggleObject : MonoBehaviour
{
    public Transform targetTransform;
    [Range(0f, 1f)]
    public float wiggleRotationDuration = 0.2f;
    public Vector3 strength;
    public int vibrato = 10;
    public float randomness = 90;
    public void Wiggle()
    {
        targetTransform.DOShakeRotation(wiggleRotationDuration, strength, vibrato, randomness);
    }
}
