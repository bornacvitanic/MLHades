using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Death : MonoBehaviour
{
    protected Health healthComponent;

    protected virtual void Awake() => healthComponent = GetComponent<Health>();

    protected virtual void OnEnable() => healthComponent.OnDeathEvent += OnDeath;

    protected virtual void OnDisable() => healthComponent.OnDeathEvent -= OnDeath;

    public abstract void OnDeath();
}
