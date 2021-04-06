using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Scriptable Object/Treasure")]
public class TreasureScriptableObject : ScriptableObject
{
    public int ID;
    public string displayName;
    public GameObject prefab;
    public Sprite icon;
}
