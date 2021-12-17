using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Attack
{
    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent(out Health collisionObjectHealthComponent))
        {
            return;
        }

        if(whitelistedTargets.Count != 0)
        {
            if(whitelistedTargets.Exists(target => other.gameObject.CompareTag(target.tag)))
            {
                collisionObjectHealthComponent.TakeDamage(damage);
            }
        }
        else
        {
            collisionObjectHealthComponent.TakeDamage(damage);
        }
    }
}
