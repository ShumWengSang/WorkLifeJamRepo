using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LieFill : MonoBehaviour
{
    public Transform mask;
    public Transform fill;
    public ParticleSystem particles;

    public void FillLevel(float percent)
    {
        mask.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 200f * (1f - percent));
        fill.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        particles.Play();
    }
}
