using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private int knockbackStrength = 55;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Rigidbody otherRigidbody) && other.TryGetComponent(out Health otherHealthComponent))
        {
            otherRigidbody.AddForce(knockbackStrength*transform.forward.normalized, ForceMode.VelocityChange);
        }
    }
}
