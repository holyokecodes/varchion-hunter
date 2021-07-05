using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour
{
    [SerializeField]
    private GenerateObjects digSiteGenerator;
    [SerializeField]
    private LoadXP loadXP;
    [SerializeField]
    private GetTotalArtifacts[] getTotalArtifacts;

    public void ClearPlayerprefs()
    {
        PlayerPrefs.DeleteAll();
        digSiteGenerator.GenerateNewDigSites();
        loadXP.Check();
        for (int i = 0; i < getTotalArtifacts.Length; i++)
        {
            getTotalArtifacts[i].Check();
        }
    }
}
