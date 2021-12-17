using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnHealthIncreasedEvent = delegate { };

    public event Action OnHealthChangedEvent = delegate { };

    public event Action OnHealthDecreasedEvent = delegate { };

    public event Action OnDeathEvent = delegate { };

    [SerializeField] private int max_health;
    private int health;

    private void OnEnable() => ResetHealth();

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return max_health;
    }

    public void IncreaseMaxHealth(int amount)
    {
        max_health += amount;
    }

    public void TakeDamage(int amount)
    {
        if(health > 0)
        {
            health = Mathf.Max(0, health - amount);
            OnHealthChangedEvent();
            OnHealthDecreasedEvent();
            if(health == 0)
            {
                Die();
            }
        }
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(max_health, health + amount);
        OnHealthChangedEvent();
        OnHealthIncreasedEvent();
    }

    private void Die()
    {
        OnDeathEvent();
    }

    public void ResetHealth()
    {
        health = max_health;
        OnHealthChangedEvent();
        OnHealthIncreasedEvent();
    }
}
