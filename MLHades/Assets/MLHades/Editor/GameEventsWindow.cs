using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameEventsWindow : EditorWindow
{
    public List<GameEvent> GameEvents;

    [MenuItem("Window/Custom/GameEvents")]
    public static void ShowWindow()
    {
        GetWindow<GameEventsWindow>();
    }

    private void OnGUI()
    {
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
        foreach(GameEvent e in GameEvents)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(e.name, style, GUILayout.Height(40f));
            if(GUILayout.Button("Raise", GUILayout.MaxWidth(100.0f), GUILayout.MinWidth(50.0f), GUILayout.Height(40f)))
            {
                e.Raise();
            }
            EditorGUILayout.EndHorizontal();
        }

        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("GameEvents");

        EditorGUILayout.PropertyField(stringsProperty, true);
        so.ApplyModifiedProperties();
    }
}
