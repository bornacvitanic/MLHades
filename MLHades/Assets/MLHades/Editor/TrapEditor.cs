using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Trap), editorForChildClasses: true)]
public class TrapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        Trap e = target as Trap;

        if(GUILayout.Button("Activate"))
        {
            e.Activate(new GameObject());
        }
    }

    private void OnSceneGUI()
    {
        Trap e = target as Trap;
        Handles.color = Color.cyan;
        if(e.triggers.Count > 0)
        {
            for(int i = e.triggers.Count - 1; i >= 0; i--)
            {
                if(e.triggers[i] != null)
                {
                    if(e.triggers[i].GetComponent<TrapTrigger>().isOnTrapList(e.gameObject))
                    {
                        Handles.DrawLine(e.transform.position, e.triggers[i].transform.position);
                    }
                    else
                    {
                        e.triggers.RemoveAt(i);
                    }
                }
            }
        }
    }
}
