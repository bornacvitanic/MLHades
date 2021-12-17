using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomController), editorForChildClasses: true)]
public class RoomControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        RoomController e = target as RoomController;
        if(GUILayout.Button("Spawn Enemy"))
        {
            e.SpawnEnemy();
        }

        if(GUILayout.Button("Spawn SpawnPoint"))
        {
            e.SpawnSpawnPoint();
        }

        if(GUILayout.Button("Spawn Reward"))
        {
            e.SpawnReward();
        }
    }
}
