using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class LoadTreasure : MonoBehaviour
{
    public GameObject pickUpButton;
    private GameObject treasurePrefab;

    public GameObject treasureObj;
    public Text XPText;

    public CanvasGroup XPPopUp;
    public CanvasGroup movePhone;
    public float time;
    public bool hasBeenPickedUp = false;

    public int xpGain = 10;

    public ARPlaneManager arPlaneManager;

    int itemNumber = 0;

    public TreasureList treasures;
    public Text treasureName;
    public string treasureNameStr;

    // Start is called before the first frame update
    void Start()
    {
        //string treasure = PlayerPrefs.GetString("treasure");
        int treasure = PlayerPrefs.GetInt("treasure");
        Debug.Log("Treasure ID: " + treasure);

        arPlaneManager.planesChanged += PlanesChanged;

        //string treasure = PlayerPrefs.GetString("treasure");
        
        for (int i = 0; i < treasures.treasures.Length; i++)
        {
            if (treasures.treasures[i].ID == treasure)
            {
                treasurePrefab = treasures.treasures[i].prefab;
                Debug.Log(treasures.treasures[i].displayName);
                treasureNameStr = treasures.treasures[i].displayName;
                treasureName.text = treasureNameStr;
            }
        }
        //Instantiate(treasureObj, Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        if (hasBeenPickedUp) { time += Time.deltaTime; }
        if (time >= 2) { StartCoroutine(FadeLoadingScreen(0, 1, XPPopUp)); }
    }

    private void PlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added != null && treasureObj == null)
        {
            // Get the first plane detected
            if (args.added.Count > 0 && !hasBeenPickedUp)
            {
                ARPlane arPlane = args.added[0];
                treasureObj = Instantiate(treasurePrefab, arPlane.transform.position, Quaternion.identity);
                StartCoroutine(FadeLoadingScreen(0, 0.5f, movePhone));
                treasureName.text = treasureNameStr;
            }
        }
    }

    public void CollectTreasure()
    {
        PlayerPrefs.SetInt("xp", PlayerPrefs.GetInt("xp")+xpGain);
        XPText.text = xpGain + " xp gained!";

        PlayerPrefs.SetInt("collected", 1);
        PlayerPrefs.SetInt("item" + itemNumber, PlayerPrefs.GetInt("item" + itemNumber) + 1);

        Destroy(treasureObj);
        Destroy(pickUpButton);
        StartCoroutine(FadeLoadingScreen(1, 1, XPPopUp));
        hasBeenPickedUp = true;
    }

    IEnumerator FadeLoadingScreen(float targetValue, float duration, CanvasGroup canvasGroup)
    {
        float startValue = canvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetValue;
    }
}
