using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Objects")]
    [SerializeField] private GameObject energyBallAttack;
    [SerializeField] private MeshRenderer selfMesh;

    protected override void OnEnable()
    {
        base.OnEnable();
        energyBallAttack.SetActive(false);
    }

    public override void OnAttackStart()
    {
        selfMesh.material.EnableKeyword("_EMISSION");
    }

    public override void Attack()
    {
        LookAt(target.position);
        if(Time.time > timeOfNextStateChange - 2f)
        {
            timeOfNextStateChange -= 1f;
        }
    }

    public override void OnAttackInterrupt()
    {
        selfMesh.material.DisableKeyword("_EMISSION");
    }

    public override void OnAttackEnd()
    {
        OnAttackInterrupt();
        energyBallAttack.SetActive(true);
        energyBallAttack.SetActive(false);
    }

    public override void Move(float speed)
    {
        MoveTowardsDestination(movementDestination, speed);
    }
}
