using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(UIRecyclingScrollView))]
[CanEditMultipleObjects]
public class UIRecyclingScrollViewEditor : ScrollRectEditor
{
    SerializedProperty prefab;

    protected override void OnEnable()
    {
        base.OnEnable();
        prefab = serializedObject.FindProperty("listItemPrefab");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.PropertyField(prefab); 
        serializedObject.ApplyModifiedProperties();
    }
}
