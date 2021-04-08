using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTotalArtifacts : MonoBehaviour
{
    public int artifactNumber;
    public Text text;

    // Start is called before the first frame update
    void Awake()
    {
        Check();
    }

    public void Check()
    {
        text.text = PlayerPrefs.GetInt("item" + artifactNumber).ToString();
    }
}
