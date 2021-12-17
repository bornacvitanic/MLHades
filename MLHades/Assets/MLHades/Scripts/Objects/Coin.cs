using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObjectReference agentReference;

    private void OnEnable() => Invoke("Collect", 1.5f);

    private void Collect() => gameObject.SetActive(false);

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out RoguelikeAgent roguelikeAgentComponent))
        {
            Collect();
        }
    }
}
