﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelativeUISize : MonoBehaviour
{
    public RectTransform canvas;

    public RectTransform[] rects;
    public float padding;
    int numberOfThings;

    private void Awake()
    {
        numberOfThings = rects.Length;
    }

    // Start is called before the first frame update
    void Update()
    {
        for (int i = 0; i < rects.Length; i++)
        {
            if (rects[i] == null) numberOfThings -= 1;
        }
        for (int i = 0; i < rects.Length; i++)
        {
            if (rects[i] != null)
            {
                rects[i].sizeDelta = new Vector2((canvas.sizeDelta.x / numberOfThings) - padding, rects[i].sizeDelta.y);
            }
        }
    }
}
