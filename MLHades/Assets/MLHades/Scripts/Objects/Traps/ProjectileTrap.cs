using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrap : Trap
{
    [SerializeField] private GameObject projectileSpawner;
    [SerializeField] private Animator animator;

    public override void Activate(GameObject triggerGameObject)
    {
        ShootProjectiles(triggerGameObject.transform.position);
    }

    public void ShootProjectiles(Vector3 targetPosition)
    {
        RotateSpawnerTowards(targetPosition);
        animator.SetTrigger("Activate");
    }

    private void RotateSpawnerTowards(Vector3 position)
    {
        Vector3 targetDirection = (position - transform.position).normalized;
        projectileSpawner.transform.localEulerAngles = new Vector3(0, Mathf.Atan2(targetDirection.x, targetDirection.z) / Mathf.PI * 180, 0);
    }
}
