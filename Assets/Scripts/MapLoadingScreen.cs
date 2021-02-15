using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Location;
using Mapbox.Utils;

public class MapLoadingScreen : MonoBehaviour
{
    private AbstractLocationProvider locationProvider = null;

    public RectTransform arc1;
    public RectTransform arc2;

    public float arc1Speed;
    public float arc2Speed;

    public CanvasGroup panel;

    bool startClock;
    int clock = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (null == locationProvider)
        {
            locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider as AbstractLocationProvider;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startClock)
        {
            clock++;
        }
        Location currLoc = locationProvider.CurrentLocation;
        arc1.Rotate(0, 0, arc1Speed);
        arc2.Rotate(0, 0, arc2Speed);

        if (currLoc.LatitudeLongitude.Equals(Vector2d.zero) || currLoc.IsLocationServiceEnabled)
        {
            startClock = true;
        }

        if (clock == 200){StartCoroutine(FadeLoadingScreen(0, 1f));}
    }

    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = panel.alpha;
        float time = 0;

        while (time < duration)
        {
            panel.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        panel.alpha = targetValue;
    }
}
