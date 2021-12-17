using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Health), editorForChildClasses: true)]
public class HealthEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        Health e = target as Health;

        EditorGUILayout.BeginHorizontal();

            GUILayout.Label("Health");
            GUILayout.Label(e.GetHealth().ToString());

        EditorGUILayout.EndHorizontal();

        if(GUILayout.Button("Reset Health"))
        {
            e.ResetHealth();
        }
    }

    private void OnSceneGUI()
    {
        Health e = target as Health;
        Handles.Label(e.transform.position+Vector3.up*2f, "Health: " + e.GetHealth().ToString());
    }
}
