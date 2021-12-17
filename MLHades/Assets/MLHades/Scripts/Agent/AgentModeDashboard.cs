using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Demonstrations;
using Unity.MLAgents.Policies;
using UnityEngine;

[ExecuteAlways]
public class AgentModeDashboard : MonoBehaviour
{
    [SerializeField] private GameObject agent;

    public BehaviorType behaviorType;
    public bool recordDemonstration;

    private void Start()
    {
        if(gameObject == null)
        {
            agent = FindObjectOfType<RoguelikeAgent>().gameObject;
        } 
    }

    private void OnValidate()
    {
         if(gameObject == null)
        {
            agent = FindObjectOfType<RoguelikeAgent>().gameObject;
        }
        if(agent == null) { return; } // Exit if no agent found
        agent.GetComponent<BehaviorParameters>().BehaviorType = behaviorType;
        agent.GetComponent<DemonstrationRecorder>().Record = recordDemonstration;
    }
}
