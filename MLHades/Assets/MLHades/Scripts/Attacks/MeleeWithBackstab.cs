using UnityEngine;

public class MeleeWithBackstab : Attack
{
    [SerializeField] private GameObject hitParticleSystem;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent(out Health collisionObjectHealthComponent))
        {
            return;
        }

        if(Vector3.Dot(other.gameObject.transform.forward, gameObject.GetComponentInParent<Animator>().gameObject.transform.forward) > 0)
        {
            collisionObjectHealthComponent.TakeDamage(damage + damage / 2); // Backstab
        }
        else
        {
            collisionObjectHealthComponent.TakeDamage(damage); // Normal melee
        }

        if(hitParticleSystem!=null && other.gameObject.CompareTag("enemy")){
            var particleSystem = ObjectPooler.SharedInstance.Instantiate(hitParticleSystem);
            particleSystem.transform.position = other.gameObject.transform.position;
            particleSystem.transform.eulerAngles = transform.eulerAngles;
            particleSystem.transform.parent = GameObject.FindGameObjectWithTag("dynamic").transform;
        }
    }
}
