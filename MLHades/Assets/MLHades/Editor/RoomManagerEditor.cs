using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomManager), editorForChildClasses: true)]
public class RoomManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        RoomManager e = target as RoomManager;
        if(GUILayout.Button("Spawn Room"))
        {
            e.SpawnRoom();
        }
    }
}
