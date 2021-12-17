using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TrapTrigger), editorForChildClasses: true)]
public class TrapTriggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        TrapTrigger e = target as TrapTrigger;

        if(GUILayout.Button("Trigger"))
        {
            e.Trigger();
        }
    }

    private void OnSceneGUI()
    {
        TrapTrigger e = target as TrapTrigger;
        Handles.color = Color.cyan;
        if(e.traps.Count > 0)
        {
            e.traps.ForEach(trap =>
            {
                if(trap != null)
                {
                    Handles.DrawLine(e.transform.position, trap.transform.position);
                }
            });
        }
    }
}
