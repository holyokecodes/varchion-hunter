using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Location;
using Mapbox.Utils;

public class DistanceChecker : MonoBehaviour
{
    public GenerateObjects digSites;

    private AbstractLocationProvider _locationProvider = null;

    public double min = 0.0003f;
    public int minTreasure = 0;
    public Vector2d minPos;

    public GameObject ARButton;

    [SerializeField]
    private float distanceForAR = 0.0001f;

    [SerializeField]
    private bool overrideAR;

    void Start()
    {
        if (null == _locationProvider)
        {
            _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider as AbstractLocationProvider;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2d currLoc = _locationProvider.CurrentLocation.LatitudeLongitude;

        if (digSites.digSites.Length > 1) min = Vector2d.Distance(digSites.digSites[0].latLong, currLoc);

        if (digSites.digSites.Length == 20 && !currLoc.Equals(Vector2d.zero) && _locationProvider.CurrentLocation.IsLocationServiceEnabled)
        {
            for (int i = 0; i < 20; i++)
            {
                double distance = Vector2d.Distance(digSites.digSites[i].latLong, currLoc);
                if (distance < min)
                {
                    min = distance;
                    minTreasure = digSites.digSites[i].treasureID;
                    minPos = digSites.digSites[i].latLong;
                    PlayerPrefs.SetInt("minIndex", i);
                }
            }
        }

        if ((min < distanceForAR || overrideAR) && digSites.digSites.Length > 1)
        {
            ARButton.SetActive(true);
            PlayerPrefs.SetInt("treasure", minTreasure);
        } else
        {
            ARButton.SetActive(false);
        }

    }
}
