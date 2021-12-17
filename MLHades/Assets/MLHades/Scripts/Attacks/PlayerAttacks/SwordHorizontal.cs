using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHorizontal : MonoBehaviour
{
    [SerializeField] public GameObjectReference agent;

    private void OnEnable()
    {
        agent.GetReference().GetComponent<RoguelikeAgent>().Combo1();
    }

    private void OnDisable()
    {
        agent.GetReference().GetComponent<RoguelikeAgent>().ComboReset();
    }
}
