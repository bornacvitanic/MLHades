using UnityEngine;

public class DashEnemy : Enemy
{
    [Header("Objects")]
    [SerializeField] private GameObject dashAttack;
    [SerializeField] private GameObject attackIndicator;
    [SerializeField] private MeshRenderer selfMesh;

    private Rigidbody rb;
    private bool dashed = false;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        dashAttack.SetActive(false);
        attackIndicator.SetActive(false);
    }

    public override void OnAttackStart()
    {
        attackIndicator.SetActive(true);
        LookAt(target.position);
        selfMesh.material.EnableKeyword("_EMISSION");
    }

    public override void Attack()
    {
        if(!dashed && Time.time > timeOfNextStateChange - data.timeBetweenModeChanges / 2)
        {
            dashAttack.SetActive(true);
            gameObject.layer = 11;
            rb.AddForce(100f * transform.forward, ForceMode.VelocityChange);
            dashed = true;
            attackIndicator.SetActive(false);
        }
    }

    public override void OnAttackInterrupt()
    {
        dashAttack.SetActive(false);
        selfMesh.material.DisableKeyword("_EMISSION");
        dashed = false;
        gameObject.layer = 15;
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
