using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordVertical : MonoBehaviour
{
    [SerializeField] private GameObjectReference agent;

    private void OnEnable()
    {
        agent.GetReference().GetComponent<RoguelikeAgent>().Combo2();
    }

    private void OnDisable()
    {
        agent.GetReference().GetComponent<RoguelikeAgent>().ComboReset();
    }
}
