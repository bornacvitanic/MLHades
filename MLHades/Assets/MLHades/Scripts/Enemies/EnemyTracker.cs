using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyTracker : MonoBehaviour
{
    [SerializeField] private GameObjectList enemyInstances;
    [SerializeField] private GameEvent enemyDiedEvent;

    private Health healthComponent;

    private void Awake() => healthComponent = GetComponent<Health>();

    private void OnEnable()
    {
        healthComponent.OnDeathEvent += OnDeath;
        AddToList();
    }

    private void AddToList() => enemyInstances.objects.Add(gameObject);

    public void RemoveFromList() => enemyInstances.objects.Remove(gameObject);

    private void OnDeath()
    {
        if(enemyDiedEvent != null)
        {
            enemyDiedEvent.Raise();
        }
    }

    private void OnDisable()
    {
        healthComponent.OnDeathEvent -= OnDeath;
        RemoveFromList();
    }
}
