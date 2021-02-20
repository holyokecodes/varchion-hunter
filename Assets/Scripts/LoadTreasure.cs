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

    // Start is called before the first frame update
    void Start()
    {
        string treasure = PlayerPrefs.GetString("treasure");

        if ( treasure == "Sword")
        {
            Instantiate(sword, new Vector3(0, 0, 2), Quaternion.identity);
        } else if (treasure == "Knife")
        {
            Instantiate(knife, new Vector3(0, 0, 2), Quaternion.identity);
        } else if (treasure == "Bust")
        {
            Instantiate(bust, new Vector3(0, 0, 2), Quaternion.identity);
        } else if (treasure == "Pottery Shard")
        {
            Instantiate(potteryShard, new Vector3(0, 0, 2), Quaternion.identity);
        } else if (treasure == "Weird peice of electroplated silver")
        {
            Instantiate(electroplatedSilver, new Vector3(0, 0, 2), Quaternion.identity);
        }
    }
}
