using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XP : MonoBehaviour
{
    public Text xpText;

    // Start is called before the first frame update
    void Start()
    {
        xpText.text = PlayerPrefs.GetInt("xp") + " XP";
    }
    
}