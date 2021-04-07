using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelativeUISize : MonoBehaviour
{
    public RectTransform canvas;

    public RectTransform[] rects;
    public float padding;
    [SerializeField]
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
                rects[i].anchoredPosition = new Vector2((canvas.sizeDelta.x / (numberOfThings)) * (i+0.5f), rects[i].anchoredPosition.y);
                if (numberOfThings == 1) rects[i].anchoredPosition = new Vector2((rects[i].sizeDelta.x + padding)/2, rects[i].anchoredPosition.y);
            }
        }
        numberOfThings = rects.Length;
    }
}
