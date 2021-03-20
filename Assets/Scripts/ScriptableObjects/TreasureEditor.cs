using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TreasureScriptableObject))]
public class TreasureEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TreasureScriptableObject treasure = (TreasureScriptableObject)target;
        if (treasure.icon != null) GUILayout.Box(treasure.icon.texture, GUILayout.Width(100), GUILayout.Height(100));
    }
}
