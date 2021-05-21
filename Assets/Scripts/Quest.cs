using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [SerializeField]
    public string questName;

    [SerializeField]
    public DigSite[] digsites;

    [SerializeField]
    public int currentDigsite;
}
