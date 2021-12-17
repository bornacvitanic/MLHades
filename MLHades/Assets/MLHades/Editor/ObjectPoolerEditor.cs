using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectPooler), editorForChildClasses: true)]
public class ObjectPoolerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        ObjectPooler e = target as ObjectPooler;
        try
        {
            if(GUILayout.Button("Destroy All Objects"))
            {
                e.DestroyAllObjects();
            }
            GUILayout.Label("Objects in pool:");
            foreach(string key in e.pooledObjects.Keys)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(key, GUILayout.Width(200.0f));
                GUILayout.Label(e.pooledObjects[key].Count.ToString());
                EditorGUILayout.EndHorizontal();
            }
        }
        catch
        {
            // Error is thrown when Play mode is exited
        }
    }
}
