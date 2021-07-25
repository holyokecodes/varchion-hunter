using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] public List<Quest> quests;
    [SerializeField] private bool done;

    [SerializeField] public GameObject[] questDisplays;
    [SerializeField] private QuestInfo[] questInfos;
    [SerializeField] private GameObject questTabPrefab;

    [SerializeField] private Transform scrollView;
    [SerializeField] public GridLayoutGroup questGrid;

    // Start is called before the first frame update
    void Awake()
    {
        questGrid.cellSize = new Vector2(scrollView.GetComponent<RectTransform>().rect.width, 50);
        questDisplays = new GameObject[5];
        questInfos = new QuestInfo[5];
        for (int i = 0; i < 5; i++)
        {
            questDisplays[i] = Instantiate(questTabPrefab);
            questDisplays[i].transform.SetParent(scrollView);
            questDisplays[i].transform.localScale = Vector3.one;

            questInfos[i] = questDisplays[i].GetComponent<QuestInfo>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        done = false;
        for (int i = 0; i < quests.Count; i++)
        {
            questInfos[i].questType = quests[i];

            switch (quests[i].questType)
            {
                case Quest.questTypes.CollectNumber:
                    done = checkNumItems(quests[i].number);
                    break;
                case Quest.questTypes.CollectType:
                    done = PlayerPrefs.GetInt("item" + quests[i].treasureType) >= quests[i].number;
                    break;
            }

        }
    }

    public bool checkNumItems(int itemID)
    {
        return true;
    }
}
