using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] public List<Quest> quests;
    [SerializeField] private bool done;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*done = false;
        for (int i = 0; i < quests.Count; i++)
        {
            switch (quests[i].questType)
            {
                case Quest.questTypes.CollectNumber:
                    done = checkNumItems(quests[i].number);
                    break;
                case Quest.questTypes.CollectType:
                    done = PlayerPrefs.GetInt("item" + quests[i].treasureType) >= quests[i].number;
                    break;
            }

        }*/
    }

    public bool checkNumItems(int itemID)
    {
        return true;
    }
}
