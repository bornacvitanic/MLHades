using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUi : MonoBehaviour
{
    [Header("Agent reference")]
    [SerializeField] private GameObjectReference agentReference;

    [Header("Actions")]
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    [SerializeField] private Button attackButton;
    [SerializeField] private Button interactButton;

    [SerializeField] private Button dashButton;

    private RoguelikeAgent roguelikeAgentComponent;

    // Start is called before the first frame update
    void Start()
    {
        roguelikeAgentComponent = agentReference.GetReference().GetComponent<RoguelikeAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        upButton.interactable = roguelikeAgentComponent.moveUpAvailable;
        downButton.interactable = roguelikeAgentComponent.moveDownAvailable;
        leftButton.interactable = roguelikeAgentComponent.moveLeftAvailable;
        rightButton.interactable = roguelikeAgentComponent.moveRightAvailable;
        attackButton.interactable = roguelikeAgentComponent.attackAvailable;
        interactButton.interactable = roguelikeAgentComponent.interactAvailable;
        dashButton.interactable = roguelikeAgentComponent.dashAvailable;
    }
}
