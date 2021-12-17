using UnityEngine;

public class HealReward : Reward
{
    [SerializeField] private int healAmount = 5;

    public override void AdditionalEffect(GameObject origin)
    {
        if(origin.TryGetComponent(out Health healthComponent))
        {
            healthComponent.Heal(healAmount);
        }
    }
}
