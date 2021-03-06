using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelativeUISize : MonoBehaviour
{
    public RectTransform canvas;

    public RectTransform[] rects;
    public float padding;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < rects.Length; i++)
        {
            rects[i].sizeDelta = new Vector2((canvas.sizeDelta.x / rects.Length) - padding, rects[i].sizeDelta.y);
        }
    }
}
