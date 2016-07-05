using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(WorkerManager))]
public class WorkerManagerEditor : EntityEditor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUI.changed)
        {
            WorkerManager myTarget = (WorkerManager)target;
            Worker w = (Worker)myTarget.Entity;    

            Job job = myTarget.jobInEditor;
            //We set the Job to dirty so the manager knows that he has to update it when the event is triggered.
            myTarget.jobInEditor = Job.dirty;
            w.Job = job;
            EditorUtility.SetDirty(target);
        }
    }

}