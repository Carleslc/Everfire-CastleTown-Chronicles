using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MovingEntityManager))]
public class EntityEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Delete Entity"))
        {
            MovingEntityManager myTarget = (MovingEntityManager)target;
            myTarget.Destroy();
        }
    }
}
