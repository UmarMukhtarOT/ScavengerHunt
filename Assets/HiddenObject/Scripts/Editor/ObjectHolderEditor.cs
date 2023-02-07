using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AreaHolder))]
public class ObjectHolderEditor : Editor
{

    public override void OnInspectorGUI()
    {
        
        DrawDefaultInspector();

        AreaHolder objectHolder = target as AreaHolder;

        if (GUILayout.Button("Arrange List"))
        {
           // objectHolder.ArrangeList();
        }

       // serializedObject.ApplyModifiedProperties();
    }


}
