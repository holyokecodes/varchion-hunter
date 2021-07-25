using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [SerializeField] public string questName;

    public enum questTypes
    {
        CollectType,
        CollectNumber
    }

    [SerializeField] public questTypes questType;

    [SerializeField] public int treasureType;
    [SerializeField] public int number;

    [SerializeField] public bool completed;

    public Quest(questTypes questTypeParam, int treasureTypeParam, int numberParam)
    {
        questType = questTypeParam;
        treasureType = treasureTypeParam;
        number = numberParam;
    }
}
