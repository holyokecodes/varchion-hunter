using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSiteGenerator : MonoBehaviour
{
    public int seed = 50;
    int digSiteNumber;
    bool itemPickedUp = false;

    public string[] digSites;

    void Start()
    {
        Random.InitState(seed);
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
        float currentLat = 50;
        float currentLong = 50;

        float distance = Random.Range(0, 100);
        float heading = Random.Range(0, 360);

        float latChange = distance * Mathf.Cos(heading);
        float longChange = distance * Mathf.Sin(heading);

        float latitude = currentLat + latChange;
        float longitude = currentLong + longChange;

        string treasure = "";

        switch (Random.Range(0, 5))
        {
            case 0:
                treasure = "Vase";
                break;
            case 1:
                treasure = "Mace";
                break;
            case 2:
                treasure = "Nail";
                break;
            case 3:
                treasure = "Clay Tablet";
                break;
            case 4:
                treasure = "Batterey";
                break;
        }

        digSites[digSiteNumber] = treasure; //replace old data with new data (would include lat and long)
    }
}
