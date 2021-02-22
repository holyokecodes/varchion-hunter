using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTreasure : MonoBehaviour
{
    public GameObject sword;
    public GameObject knife;
    public GameObject bust;
    public GameObject potteryShard;
    public GameObject electroplatedSilver;

    public GameObject treasureObj;
    public Text XPText;

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

    int add;
    int XP;
    /**public string XPOp(int opType, int Change){
        if (opType == 0){
            AmountOfXP += Change;
            return "Added" + AmountOfXP;
        }else{
            return AmountOfXP.ToString();
        }
    }**/
    public void CollectTreasure()
    {
        if (PlayerPrefs.GetString("treasure") == "Sword")
        {
            add = 1500;
        }
        else if (PlayerPrefs.GetString("treasure") == "Bust")
        {
            add = 800;
        }
        else if (PlayerPrefs.GetString("treasure") == "Weird peice of electroplated silver")
        {
            add = 1700;
        }
        else
        {
            add = 42;
            Debug.Log("Weird thing happened(LoadTreasure, CollectTreasure): Unknown Treasure");
        }
        XP += add;
        Destroy(treasureObj);
        StartCoroutine(FadeLoadingScreen(1, 1));
        hasBeenPickedUp = true;
    }

    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        XPText.text = "You have gained" + add.ToString() + "XP";
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
