using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ExitDoor), editorForChildClasses: true)]
public class ExitDoorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        ExitDoor e = target as ExitDoor;
        if(GUILayout.Button("Unlock Door"))
        {
            e.UnlockDoor();
        }
    }
}
