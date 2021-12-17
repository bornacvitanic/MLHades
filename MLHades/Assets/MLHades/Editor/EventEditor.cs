using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent), editorForChildClasses: true)]
public class EventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        GameEvent e = target as GameEvent;
        if(GUILayout.Button("Raise"))
        {
            e.Raise();
        }
        var listeners = e.getEventListeners();
        if(listeners.Count > 0)
        {
            GUILayout.Label("Listeners:");

            foreach(GameObject g in listeners)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(g.name);
                if(GUILayout.Button("Select", GUILayout.MaxWidth(100.0f), GUILayout.MinWidth(50.0f)))
                {
                    Selection.activeGameObject = g;
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
