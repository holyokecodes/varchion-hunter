using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Examples;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class DigSiteGenerator : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map; //The map

    [SerializeField]
    Vector2d[] _locations;

    [SerializeField]
    float _spawnScale = 4f; //The scale at which to spawn the prefabs

    [SerializeField]
    GameObject _markerPrefab; // The actuall prefab

    List<GameObject> _spawnedObjects; //The objects that have been spawned

    public int seed = 50; //The seed for the reandom jnumber generator
    int digSiteNumber; //The index of the generated digsite.
    bool itemPickedUp = false; //True when an item has been picked up.

    public digSite[] digSites = new digSite[20]; //The digsites

    private AbstractLocationProvider _locationProvider = null;

    bool hasGeneratedPoints = false;

    void Start()
    {

        _locations = new Vector2d[20];

        Random.InitState(seed);

        if (null == _locationProvider)
        {
            _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider as AbstractLocationProvider;
        }

        _spawnedObjects = new List<GameObject>();
        for (int i = 0; i < _locations.Length; i++)
        {
            var instance = Instantiate(_markerPrefab);
            instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
            instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            _spawnedObjects.Add(instance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Location currLoc = _locationProvider.CurrentLocation;

        if (!hasGeneratedPoints && !currLoc.LatitudeLongitude.Equals(Vector2d.zero) && currLoc.IsLocationServiceEnabled)
        {
            GenerateTheDigSites();
            hasGeneratedPoints = true;
        }

        if (itemPickedUp)
        {
            GenerateNewDigSite(digSiteNumber);
            itemPickedUp = false;
        }

        int count = _spawnedObjects.Count;
        for (int i = 0; i < count; i++)
        {
            var spawnedObject = _spawnedObjects[i];
            var location = _locations[i];
            spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
            spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        }
    }

    public void GenerateTheDigSites()
    {
        for (int i = 0; i < 20; i++)
        {
            GenerateNewDigSite(i);
        }
    }

    public void GenerateNewDigSite(int digSiteNumber)
    {
        Location currLoc = _locationProvider.CurrentLocation;

        float currentLat = (float) currLoc.LatitudeLongitude.y;
        float currentLong = (float) currLoc.LatitudeLongitude.x;

        Vector2d latLongChange = new Vector2d(Random.Range(0, 300f), Random.Range(0, 300f));
        float latChange = (float) latLongChange.x;
        float longChange = (float) latLongChange.y;

        float latitude =  (float) Conversions.LatLonToMeters(new Vector2d(currentLat, currentLong)).x + latChange;
        float longitude = (float) Conversions.LatLonToMeters(new Vector2d(currentLat, currentLong)).y + longChange;

        latitude = (float)Conversions.MetersToLatLon(new Vector2d(latitude, longitude)).y;
        longitude = (float)Conversions.MetersToLatLon(new Vector2d(latitude, longitude)).x;




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

        currectDigSite.latLong = new Vector2d(latitude, longitude);
        currectDigSite.treasure = treasure;

        _locations[digSiteNumber] = new Vector2d(latitude, longitude);

        digSites[digSiteNumber] = new digSite();
    }
}
