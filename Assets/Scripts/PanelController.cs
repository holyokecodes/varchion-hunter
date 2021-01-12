using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public CanvasGroup panel;
    bool on = false;

    public void Toggle()
    {
        on = !on;

        if (on) panel.alpha = 1;
        else panel.alpha = 0;
    }
}
