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

    // Start is called before the first frame update
    void Start()
    {
        arPlaneManager.planesChanged += PlanesChanged;

        string treasure = PlayerPrefs.GetString("treasure");

        // REALLY IMPORTANT NOTE! if you want different values of XP for different treasures, set them here with the code "xpGain = [new value]"

        if (treasure == "Sword")
        {
            treasurePrefab = swordPrefab;
            itemNumber = 0;
        }
        else if (treasure == "Knife")
        {
            treasurePrefab = knifePrefab;
            itemNumber = 1;
        }
        else if (treasure == "Bust")
        {
            treasurePrefab = bustPrefab;
            itemNumber = 2;
        }
        else if (treasure == "Pottery Shard")
        {
            treasurePrefab = potteryShardPrefab;
            itemNumber = 3;
        }
        else if (treasure == "Weird peice of electroplated silver")
        {
            treasurePrefab = electroplatedSilverPrefab;
            itemNumber = 4;
        }
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
                treasureObj = Instantiate(treasurePrefab, arPlane.transform.position, Quaternion.identity);
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
