using UnityEngine;

[RequireComponent(typeof(Health))]
public class RaiseEventOnHealthChange : MonoBehaviour
{
    [SerializeField] private GameEvent healthChangedEvent;
    private Health healthComponent;

    private void Awake()
    {
        healthComponent = GetComponent<Health>();
        healthComponent.OnHealthChangedEvent += RaiseEvent;
    }

    private void RaiseEvent() => healthChangedEvent.Raise();

    private void OnDestroy()
    {
        healthComponent.OnHealthChangedEvent -= RaiseEvent;
    }
}
