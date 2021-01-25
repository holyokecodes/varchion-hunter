using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LocationParser : MonoBehaviour
{
    [System.Serializable]
    public class Quest{
        public float[] Longs;
        public float[] Lats;
        public string Name;
        public string Description;
    }

    // Start is called before the first frame update
    public string jsonString = "allQuests";
    void Start()
    {
        Quest[] allQuests = JsonUtility.FromJson<Quest[]>(jsonString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
