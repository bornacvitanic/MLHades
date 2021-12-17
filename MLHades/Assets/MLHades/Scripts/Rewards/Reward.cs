using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Roguelike;

public abstract class Reward : MonoBehaviour, IInteract
{
    [SerializeField] protected GameEvent rewardCollectedEvent;

    public abstract void AdditionalEffect(GameObject origin);

    public void Interact(GameObject origin)
    {
        AdditionalEffect(origin);
        rewardCollectedEvent.Raise();
        gameObject.SetActive(false);
    }
}
