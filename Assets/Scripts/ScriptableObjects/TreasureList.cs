using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Treasure List")]
public class TreasureList : ScriptableObject
{
    public TreasureScriptableObject[] treasures;
}
