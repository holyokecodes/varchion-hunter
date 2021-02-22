using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Location;
using Mapbox.Utils;

public class DistanceChecker : MonoBehaviour
{
    public DigSiteGenerator digSites;

    private AbstractLocationProvider _locationProvider = null;

    public double min = 0.0003f;
    public string minTreasure = "";
    public Vector2d minPos;

    public GameObject ARButton;

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
        min = 0.0005f;

        Vector2d currLoc = _locationProvider.CurrentLocation.LatitudeLongitude;

        if (digSites.digSites.Length == 20 && !currLoc.Equals(Vector2d.zero) && _locationProvider.CurrentLocation.IsLocationServiceEnabled)
        {
            for (int i = 0; i < 20; i++)
            {
                double distance = Vector2d.Distance(digSites.digSites[i].latLong, currLoc);
                if (distance < min)
                {
                    min = distance;
                    minTreasure = digSites.digSites[i].treasure;
                    minPos = digSites.digSites[i].latLong;
                }
            }
        }

        if (min < 0.0005)
        {
            ARButton.SetActive(true);
            PlayerPrefs.SetString("treasure", minTreasure);
        } else
        {
            ARButton.SetActive(false);
        }

    }
}
