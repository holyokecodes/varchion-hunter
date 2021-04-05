using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAnimation : MonoBehaviour
{
    float scale = 0f;

    public float ScaleSpeed = 0.1f;
    public float maxScale = 2;
    public float rotateSpeed = 1;

    bool goingUp = true;

    public void FixedUpdate()
    {
        transform.Rotate(0, 0, rotateSpeed);
        if (goingUp)
        {
            transform.localScale = new Vector3(transform.localScale.x + ScaleSpeed, transform.localScale.y + ScaleSpeed, 1);
            scale += ScaleSpeed;
            if (scale >= maxScale) goingUp = false;
        } else
        {
            transform.localScale = new Vector3(transform.localScale.x - ScaleSpeed, transform.localScale.y - ScaleSpeed, 1);
            scale -= ScaleSpeed;
            if (scale <= 0) goingUp = true;
        }
    }
}
