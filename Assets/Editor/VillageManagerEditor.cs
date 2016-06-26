using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(VillageManager))]
public class VillageManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Add New Villager")) {
            VillageManager myTarget = (VillageManager)target;
            myTarget.AddNewWorker();
        }
    }

}
