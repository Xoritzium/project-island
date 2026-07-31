using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IslandGenerator))]
public class IslandGeneratorEditor : Editor
{
    IslandGenerator generator;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        generator = (IslandGenerator)target;

        if (GUILayout.Button("Spawn Island"))
        {
            generator.SpawnIsland();
        }
        //GUILayout.BeginHorizontal() / EndHorizontal()
        if (GUILayout.Button("Destroy Islands"))
        {
            generator.DestroyIslands();
        }
    }
}
