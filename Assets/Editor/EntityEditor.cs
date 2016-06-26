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

        if (GUILayout.Button("Kill Entity"))
        {
            EntityManager myTarget = (EntityManager)target;
            myTarget.Kill();
        }
    }
}
[CustomEditor(typeof(AnimalManager))]
public class AnimalEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}

