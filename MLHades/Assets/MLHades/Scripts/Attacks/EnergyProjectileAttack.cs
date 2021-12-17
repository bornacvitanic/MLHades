using UnityEngine;

public class EnergyProjectileAttack : Attack
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float timeToLive = 2;
    private float destroyTime;
    public string originTag;
    [Header("Optional")]
    [SerializeField] private GameObject particles;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Health collisionObjectHealthComponent))
        {
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
        if(!other.gameObject.CompareTag(originTag))
        {
            if(particles != null)
            {
                var particleSystem = ObjectPooler.SharedInstance.Instantiate(particles,transform.position,transform.rotation);
                particleSystem.transform.parent = GameObject.FindGameObjectWithTag("dynamic").transform;
            }
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * movementSpeed;
        destroyTime = Time.time + timeToLive;
    }

    private void FixedUpdate()
    {
        if(Time.time >= destroyTime)
        {
            gameObject.SetActive(false);
        }
    }
}
