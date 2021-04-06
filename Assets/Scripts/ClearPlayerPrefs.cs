using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour
{
    [SerializeField]
    private DigSiteGenerator digSiteGenerator;

    public void ClearPlayerprefs()
    {
        PlayerPrefs.DeleteAll();
        digSiteGenerator.GenerateNewDigSites();
    }
}
