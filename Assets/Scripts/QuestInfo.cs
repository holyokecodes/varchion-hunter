using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestInfo : MonoBehaviour
{
    [SerializeField] private Text title;
    [SerializeField] private Text progressText;
    [SerializeField] private Slider progressBar;

    public int progress;
    public Quest questType;
    public TreasureList treasures;

    // Update is called once per frame
    void Update()
    {
        string name = "";
        if (questType.questType == Quest.questTypes.CollectType)
        {
            name = "Collect " + questType.number + " " + treasures.treasures[questType.treasureType].displayName;
            if (questType.number > 1) name += "s";
        } else if (questType.questType == Quest.questTypes.CollectNumber){
            name = "Collect " + questType.number + " treasure";
            if (questType.number > 1) name += "s";
        }
        title.text = name;

        progressBar.maxValue = questType.number;
        progressBar.value = progress;

        progressText.text = progress + "/" + questType.number;
    }
}
