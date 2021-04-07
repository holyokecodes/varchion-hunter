using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
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

    public int seed = 50; //The seed for the reandom number generator
    int digSiteNumber; //The index of the generated digsite.

    public DigSite[] digSites = new DigSite[0]; //The digsites

    private AbstractLocationProvider _locationProvider = null;

    bool hasGeneratedPoints = false;

    [SerializeField]
    bool loadDigSites = true;

    public DistanceChecker distanceChecker;

    public TreasureList treasures;

    public bool doOverrideValue;
    public int overrideValue;

    [SerializeField]
    private float maxDistance = 600;
    [SerializeField]
    private float minDistance = 100;

    void Start()
    {

        _locations = new Vector2d[20];

        Random.InitState(seed);

        if (null == _locationProvider)
        {
            _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider as AbstractLocationProvider;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Location currLoc = _locationProvider.CurrentLocation;

        if (!currLoc.LatitudeLongitude.Equals(Vector2d.zero) && currLoc.IsLocationServiceEnabled)
        {
            if (!hasGeneratedPoints)
            {
                if (PlayerPrefs.GetInt("shouldLoadDigSites") == 0) GenerateNewDigSites();
                else digSites = SaveAndLoad.LoadDigSites();
                //if (!loadDigSites) GenerateNewDigSites();
                _spawnedObjects = new List<GameObject>();
                for (int i = 0; i < digSites.Length; i++)
                {
                    var instance = Instantiate(_markerPrefab);
                    instance.transform.localPosition = _map.GeoToWorldPosition(digSites[i].latLong, true);
                    instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                    _spawnedObjects.Add(instance);
                }
                hasGeneratedPoints = true;
                //print("generated da disites!");
            }
            if (PlayerPrefs.GetInt("collected") == 1)
            {
                GenerateNewDigSite(PlayerPrefs.GetInt("minIndex"));
                //print("Generated a new dig site!");
                PlayerPrefs.SetInt("collected", 0);
            }
        }
        if (hasGeneratedPoints)
        {
            int count = _spawnedObjects.Count;
            for (int i = 0; i < count; i++)
            {
                var spawnedObject = _spawnedObjects[i];
                var location = digSites[i].latLong;
                spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                spawnedObject.GetComponent<BillboardPin>().icon.sprite = treasures.treasures[digSites[i].treasureID].icon;
            }
        }
    }

    public void GenerateNewDigSites()
    {
        digSites = new DigSite[20];

        for (int i = 0; i < 20; i++)
        {
            GenerateNewDigSite(i);
        }

        SaveAndLoad.SaveDigSites(digSites);
        PlayerPrefs.SetInt("shouldLoadDigSites", 1);
    }

    public void GenerateNewDigSite(int digSiteNumber)
    {
        Location currLoc = _locationProvider.CurrentLocation;

        float currentLat = (float) currLoc.LatitudeLongitude.y;
        float currentLong = (float) currLoc.LatitudeLongitude.x;

        print("Im at:" + currentLat + ", " + currentLong + "Lat/Long");

        currentLat = (float) Conversions.LatLonToMeters(new Vector2d(currentLat, currentLong)).x;
        currentLong = (float) Conversions.LatLonToMeters(new Vector2d(currentLat, currentLong)).y;

        print("Im at:" + currentLat + ", " + currentLong);

        Vector2d latLongChange = new Vector2d(Random.Range(minDistance, maxDistance), Random.Range(minDistance, maxDistance));
        Vector2 isNegative = new Vector2(Random.Range(0, 1), Random.Range(0, 1));

        print(latLongChange);

        if (isNegative.x == 0) latLongChange = new Vector2d(latLongChange.x * -1, latLongChange.y);
        if (isNegative.y == 0) latLongChange = new Vector2d(latLongChange.x, latLongChange.y * -1);

        float latChange = (float) latLongChange.x;
        float longChange = (float) latLongChange.y;

        float latitude = (float)Conversions.MetersToLatLon(new Vector2d(currentLat + latChange, currentLong)).x;
        float longitude = (float)Conversions.MetersToLatLon(new Vector2d(currentLat, currentLong + longChange)).y;

        print("place: " + (currentLat + latChange) + ", " + (currentLong + longChange));

        print("Place:" + latitude + ", " + longitude + "Lat/Long");

        int treasure;
        int randoNumba = Random.Range(0, treasures.treasures.Length);
        treasure = randoNumba;

        if (doOverrideValue) treasure = overrideValue;

        DigSite currectDigSite = new DigSite();

        currectDigSite.latLong = new Vector2d(latitude, longitude);
        currectDigSite.treasureID = treasure;
        //currectDigSite.icon = treasure.icon;

        //_locations[digSiteNumber] = new Vector2d(latitude, longitude);

        digSites[digSiteNumber] = currectDigSite;

        SaveAndLoad.SaveDigSites(digSites);
    }
}
