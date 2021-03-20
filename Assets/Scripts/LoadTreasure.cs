using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTreasure : MonoBehaviour
{
    public GameObject swordPrefab;
    public GameObject knifePrefab;
    public GameObject bustPrefab;
    public GameObject potteryShardPrefab;
    public GameObject electroplatedSilverPrefab;

    public GameObject treasureObj;
    public Text XPText;

    public CanvasGroup canvasGroup;
    public float time;
    public bool hasBeenPickedUp = false;

    public int xpGain = 10;

    int itemNumber = 0;

    public TreasureList treasures;

    // Start is called before the first frame update
    void Start()
    {
        //string treasure = PlayerPrefs.GetString("treasure");
        int treasure = 0;

        // REALLY IMPORTANT NOTE! if you want different values of XP for different treasures, set them here with the code "xpGain = [new value]"

        if ( treasure == "Sword")
        {
            treasureObj = Instantiate(swordPrefab, new Vector3(0, 0, 2), Quaternion.identity);
            itemNumber = 0;
        } else if (treasure == "Knife")
        {
            treasureObj = Instantiate(knifePrefab, new Vector3(0, 0, 2), Quaternion.identity);
            itemNumber = 1;
        } else if (treasure == "Bust")
        {
            treasureObj = Instantiate(bustPrefab, new Vector3(0, 0, 2), Quaternion.identity);
            itemNumber = 2;
        } else if (treasure == "Pottery Shard")
        {
            treasureObj = Instantiate(potteryShardPrefab, new Vector3(0, 0, 2), Quaternion.identity);
            itemNumber = 3;
        } else if (treasure == "Weird peice of electroplated silver")
        {
            treasureObj = Instantiate(electroplatedSilverPrefab, new Vector3(0, 0, 2), Quaternion.identity);
            itemNumber = 4;
        } else
        {
            print("none");
            print("Teasure Number: " + PlayerPrefs.GetString("treasure"));
        }
    }

    void Update()
    {
        if (hasBeenPickedUp){time += Time.deltaTime;}
        if (time >= 2){StartCoroutine(FadeLoadingScreen(0, 1));}
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
