using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class LoadTreasure : MonoBehaviour
{
    public GameObject swordPrefab;
    public GameObject knifePrefab;
    public GameObject bustPrefab;
    public GameObject potteryShardPrefab;
    public GameObject electroplatedSilverPrefab;
    private GameObject treasurePrefab;

    public GameObject treasureObj;
    public Text XPText;

    public CanvasGroup canvasGroup;
    public float time;
    public bool hasBeenPickedUp = false;

    public int xpGain = 10;

    public ARPlaneManager arPlaneManager;

    int itemNumber = 0;

    public TreasureList treasures;

    // Start is called before the first frame update
    void Start()
    {
        //string treasure = PlayerPrefs.GetString("treasure");
        int treasure = PlayerPrefs.GetInt("treasure");
        print("Treasure ID: " + treasure);

        arPlaneManager.planesChanged += PlanesChanged;

        //string treasure = PlayerPrefs.GetString("treasure");
        
        // REALLY IMPORTANT NOTE! if you want different values of XP for different treasures, set them here with the code "xpGain = [new value]"

        for (int i = 0; i < treasures.treasures.Length; i++)
        {
            if (treasures.treasures[i].ID == treasure)
            {
                treasureObj = treasures.treasures[i].prefab;
                print(treasures.treasures[i].displayName);
            }
        }
        //Instantiate(treasureObj, Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        if (hasBeenPickedUp) { time += Time.deltaTime; }
        if (time >= 2) { StartCoroutine(FadeLoadingScreen(0, 1)); }
    }

    private void PlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added != null && treasureObj == null)
        {
            // Get the first plane detected
            if (args.added.Count > 0)
            {
                ARPlane arPlane = args.added[0];
                treasureObj = Instantiate(treasurePrefab, arPlane.transform);//.position, Quaternion.identity);
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
        StartCoroutine(FadeLoadingScreen(1, 1));
        hasBeenPickedUp = true;
    }

    IEnumerator FadeLoadingScreen(float targetValue, float duration)
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
