using UnityEngine;

public class AgentTracker : MonoBehaviour
{
    [SerializeField] private GameObjectReference agentReference;

    private void OnEnable() => AddReference();

    private void AddReference() => agentReference.AddReference(gameObject);

    public void RemoveReference() => agentReference.RemoveReference();

    private void OnDisable() => RemoveReference();
}
