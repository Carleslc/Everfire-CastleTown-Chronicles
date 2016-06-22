using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EntityManager))]
public class EntityEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Delete Entity"))
        {
            EntityManager myTarget = (EntityManager)target;
            myTarget.Destroy();
        }
    }
}
