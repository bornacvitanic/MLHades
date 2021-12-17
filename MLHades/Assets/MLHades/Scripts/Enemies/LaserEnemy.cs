using UnityEngine;

public class LaserEnemy : Enemy
{
    [Header("Values")]
    [SerializeField] private float rotationSpeed = 0.3f;
    [Header("Objects")]
    [SerializeField] private GameObject laserAttack;
    [SerializeField] private GameObject attackIndicator;

    protected override void OnEnable()
    {
        base.OnEnable();
        laserAttack.SetActive(false);
    }

    public override void OnAttackStart()
    {
        LookAt(target.position);
        laserAttack.SetActive(true);
        attackIndicator.SetActive(true);
    }

    public override void Attack()
    {
        Vector3 targetDirection = target.transform.position - transform.position;
        float singleStep = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public override void OnAttackInterrupt()
    {
        laserAttack.SetActive(false);
        attackIndicator.SetActive(false);
    }

    public override void OnAttackEnd()
    {
        OnAttackInterrupt();
    }

    public override void Move(float speed)
    {
        MoveTowardsDestination(movementDestination, speed);
    }
}
