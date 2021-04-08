using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadXP : MonoBehaviour
{
    public Text xpText;

    // Start is called before the first frame update
    void Start()
    {
        Check();
    }

    public void Check()
    {
        xpText.text = PlayerPrefs.GetInt("xp") + " XP";
    }

}