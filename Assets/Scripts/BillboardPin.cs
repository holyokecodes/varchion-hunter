using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillboardPin : MonoBehaviour
{
    public Transform pivot;
    public Image icon;

    // Update is called once per frame
    void Update()
    {
        Camera camera = Camera.main;
        transform.forward = camera.transform.forward;
        pivot.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
    }
}
