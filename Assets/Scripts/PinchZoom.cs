using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;

public class PinchZoom : MonoBehaviour
{
    public AbstractMap map;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 2)
        {
            Vector2 touch0, touch1;
            float distance;
            touch0 = Input.GetTouch(0).position;
            touch1 = Input.GetTouch(1).position;
            distance = Vector2.Distance(touch0, touch1);

            map.SetZoom(Mathf.Clamp(Mathf.RoundToInt(distance / 18), 13, 19));
        }
    }
}
