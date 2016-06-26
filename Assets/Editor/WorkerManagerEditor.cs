using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(WorkerManager))]
public class WorkerManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUI.changed)
        {
            WorkerManager myTarget = (WorkerManager)target;
            myTarget.UpdateJob();
            EditorUtility.SetDirty(target);
        }
    }

}