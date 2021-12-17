using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    private void OnEnable()
    {
        GameObject projectile = ObjectPooler.SharedInstance.Instantiate(projectilePrefab, gameObject.transform.position, gameObject.transform.rotation);
        projectile.transform.parent = GameObject.FindGameObjectWithTag("dynamic").transform;
        projectile.GetComponent<EnergyProjectileAttack>().originTag = gameObject.transform.parent.tag;
    }
}
