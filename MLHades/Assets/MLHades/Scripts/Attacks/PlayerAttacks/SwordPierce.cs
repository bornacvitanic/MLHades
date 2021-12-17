using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPierce : MonoBehaviour
{
    [SerializeField] private GameObjectReference agent;

    private void OnEnable()
    {
        agent.GetReference().GetComponent<RoguelikeAgent>().Combo3();
    }

    private void OnDisable()
    {
        agent.GetReference().GetComponent<RoguelikeAgent>().ComboReset();
    }
}
