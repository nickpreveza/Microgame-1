using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextMeshProEdit))]
public class TextMeshProEditEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TextMeshProEdit myTarget = (TextMeshProEdit)target;

        myTarget.textSize = EditorGUILayout.IntField("Text Size", myTarget.textSize);
       
        if (GUILayout.Button("Change Color"))
        {
            myTarget.ChangeTextColor();
        }


        if (GUILayout.Button("Change Color of Menu Buttons"))
        {
            myTarget.ChangeTextColor("UI_MenuButton");
            myTarget.ChangeButtonColors("UI_MenuButton");
        }

        //ChangeButtonColors
    }
}

