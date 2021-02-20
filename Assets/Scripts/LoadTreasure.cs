using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTreasure : MonoBehaviour
{
    public GameObject sword;
    public GameObject knife;
    public GameObject bust;
    public GameObject potteryShard;
    public GameObject electroplatedSilver;

    public GameObject treasureObj;

    public CanvasGroup canvasGroup;

    public float time;
    public bool hasBeenPickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        string treasure = PlayerPrefs.GetString("treasure");

        if ( treasure == "Sword")
        {
            treasureObj = Instantiate(sword, new Vector3(0, 0, 2), Quaternion.identity);
        } else if (treasure == "Knife")
        {
            treasureObj = Instantiate(knife, new Vector3(0, 0, 2), Quaternion.identity);
        } else if (treasure == "Bust")
        {
            treasureObj = Instantiate(bust, new Vector3(0, 0, 2), Quaternion.identity);
        } else if (treasure == "Pottery Shard")
        {
            treasureObj = Instantiate(potteryShard, new Vector3(0, 0, 2), Quaternion.identity);
        } else if (treasure == "Weird peice of electroplated silver")
        {
            treasureObj = Instantiate(electroplatedSilver, new Vector3(0, 0, 2), Quaternion.identity);
        }
    }

    void Update()
    {
        if (hasBeenPickedUp){time += Time.deltaTime;}
        if (time >= 2){StartCoroutine(FadeLoadingScreen(0, 1));}
    }

    public void CollectTreasure()
    {
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
