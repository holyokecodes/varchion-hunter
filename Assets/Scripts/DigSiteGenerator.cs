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

    public digSite[] digSites = new digSite[0]; //The digsites

    private AbstractLocationProvider _locationProvider = null;

    bool hasGeneratedPoints = false;

    public DistanceChecker distanceChecker;

    public TreasureList treasures;

    public bool doOverrideValue;
    public int overrideValue;

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
                GenerateTheDigSites();
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
                spawnedObject.GetComponent<BillboardPin>().icon.sprite = digSites[i].icon;
            }
        }
    }

    public void GenerateTheDigSites()
    {
        digSites = new digSite[20];

        for (int i = 0; i < 20; i++)
        {
            GenerateNewDigSite(i);
        }

        _spawnedObjects = new List<GameObject>();
        for (int i = 0; i < digSites.Length; i++)
        {
            var instance = Instantiate(_markerPrefab);
            instance.transform.localPosition = _map.GeoToWorldPosition(digSites[i].latLong, true);
            instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            _spawnedObjects.Add(instance);
        }

        //print(digSites.Length);
    }

    public void GenerateNewDigSite(int digSiteNumber)
    {
        Location currLoc = _locationProvider.CurrentLocation;

        float currentLat = (float) currLoc.LatitudeLongitude.y;
        float currentLong = (float) currLoc.LatitudeLongitude.x;

        Vector2d latLongChange = new Vector2d(Random.Range(0, 600f)-300, Random.Range(0, 600f)-300);
        float latChange = (float) latLongChange.x;
        float longChange = (float) latLongChange.y;

        float latitude =  (float) Conversions.LatLonToMeters(new Vector2d(currentLat, currentLong)).x + latChange;
        float longitude = (float) Conversions.LatLonToMeters(new Vector2d(currentLat, currentLong)).y + longChange;

        latitude = (float)Conversions.MetersToLatLon(new Vector2d(latitude, longitude)).y;
        longitude = (float)Conversions.MetersToLatLon(new Vector2d(latitude, longitude)).x;




        TreasureScriptableObject treasure;
        int randoNumba = Random.Range(0, treasures.treasures.Length);
        treasure = treasures.treasures[randoNumba];

        if (doOverrideValue) treasure = treasures.treasures[overrideValue];

        digSite currectDigSite = new digSite();

        currectDigSite.latLong = new Vector2d(latitude, longitude);
        currectDigSite.treasure = treasure;
        currectDigSite.icon = treasure.icon;

        //_locations[digSiteNumber] = new Vector2d(latitude, longitude);

        digSites[digSiteNumber] = currectDigSite;
    }
}
