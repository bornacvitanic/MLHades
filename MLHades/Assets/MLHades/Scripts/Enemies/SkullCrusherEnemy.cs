using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullCrusherEnemy : Enemy
{
    [Header("Objects")]
    [SerializeField] private GameObject fallAttack;
    [SerializeField] private GameObject attackIndicator;
    [SerializeField] private MeshRenderer selfMesh;

    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        ignoreMovementRotation = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Untagged"))
        {
            gameObject.layer = 15;
            fallAttack.SetActive(false);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        intruderDetector.OnIntruderDetectedEvent += GoIntoAttackMode;
        attackRadius = 10f;
    }


    private void GoIntoAttackMode()
    {
        state = State.Attacking;
        OnAttackStart();
        timeOfNextStateChange = Time.time + data.timeBetweenModeChanges;
    }

    public override void OnAttackStart()
    {
        attackIndicator.SetActive(true);
        gameObject.layer = 16;
        rb.useGravity = false;
        transform.position = transform.position + new Vector3(0f, 10f, 0f);
        fallAttack.SetActive(true);
        timeOfNextStateChange = Time.time + data.timeBetweenModeChanges/2;
    }

    public override void Attack()
    {
        MoveTowardsDestination(new Vector3(agent.GetReference().transform.position.x, 0f, agent.GetReference().transform.position.z), data.movementSpeed);
    }

    public override void OnAttackInterrupt()
    {
        //nothing
    }

    public override void OnAttackEnd()
    {
        OnAttackInterrupt();
        rb.useGravity = true;
    }


    protected override void OnDisable()
    {
        base.OnDisable();
        intruderDetector.OnIntruderDetectedEvent -= GoIntoAttackMode;
    }

    public override void Move(float speed)
    {
        // Doesen't move
    }

    public override void Stagger()
    {
        // ignore
    }
}
