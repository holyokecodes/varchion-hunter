using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Examples;

public class DigSiteGenerator : MonoBehaviour
{
    public int seed = 50;
    int digSiteNumber;
    bool itemPickedUp = false;

    public SpawnOnMap spawnOnMap;

    public digSite[] digSites = new digSite[20];

    private AbstractLocationProvider _locationProvider = null;
    void Start()
    {
        Random.InitState(seed);

        if (null == _locationProvider)
        {
            _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider as AbstractLocationProvider;
        }

        for (int i = 0; i < 20; i++)
        {
            GenerateNewDigSite(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (itemPickedUp)
        {
            GenerateNewDigSite(digSiteNumber);
            itemPickedUp = false;
        }
    }

    void GenerateNewDigSite(int digSiteNumber)
    {
        Location currLoc = _locationProvider.CurrentLocation;
         //x is lattitude, y is logitude

        float currentLat = (float) currLoc.LatitudeLongitude.x;
        float currentLong = (float) currLoc.LatitudeLongitude.y;

        float distance = Random.Range(0, 0.01f);
        float heading = Random.Range(0, 360);

        float latChange = distance * Mathf.Cos(heading);
        float longChange = distance * Mathf.Sin(heading);

        float latitude = currentLat + latChange;
        float longitude = currentLong + longChange;

        

        string treasure = "";

        switch (Random.Range(0, 5))
        {
            case 0:
                treasure = "Sword";
                break;
            case 1:
                treasure = "Knife";
                break;
            case 2:
                treasure = "Bust";
                break;
            case 3:
                treasure = "Pottery Shard";
                break;
            case 4:
                treasure = "Weird peice of electroplated silver";
                break;
        }
        digSite currectDigSite = new digSite();

        currectDigSite.latLong = new Vector2(latitude, longitude);
        currectDigSite.treasure = treasure;

        digSites[digSiteNumber] = new digSite(); //replace old data with new data (would include lat and long)
    }
}
