using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Enemy : MonoBehaviour
{
    protected enum State
    {
        Moving,
        Attacking,
        Staggered
    }

    [SerializeField] protected EnemyData data;
    [SerializeField] protected GameObjectReference agent;
    [SerializeField] protected DetectionRadius intruderDetector;

    protected Health healthComponent;
    protected Transform target;
    protected Vector3 movementDestination;
    protected State state;
    protected bool alerted = false;
    protected float timeOfNextStateChange;
    protected float attackRadius = 10f;
    protected bool ignoreMovementRotation;
    private float timeOfSpawn;
    private bool firstStagger = true;
    private float spawnGracePeriod = 1f;

    protected virtual void Awake()
    {
        healthComponent = GetComponent<Health>();
    }

    protected virtual void OnEnable()
    {
        intruderDetector.OnIntruderDetectedEvent += AlertEnemy;
        healthComponent.OnHealthChangedEvent += Stagger;

        state = State.Moving;
        alerted = false;
        firstStagger = true;
        target = transform;
        timeOfSpawn = Time.time;
        movementDestination = pickMovementDestination(target);
        timeOfNextStateChange = Time.time + Random.Range(0, data.timeBetweenModeChanges);
        if(!ignoreMovementRotation)
        {
            LookAt(movementDestination);
        }
    }

    private void FixedUpdate()
    {
        if(Time.time > timeOfNextStateChange)
        {
            if(state == State.Attacking)
            {
                OnAttackEnd();
                state = State.Moving;
            }
            else if(state == State.Staggered)
            {
                OnAttackStart();
                state = State.Attacking;
            }
            else if(state == State.Moving)
            {
                if(alerted && Time.time + spawnGracePeriod > timeOfSpawn)
                {
                    var dist = Vector3.Distance(new Vector3(agent.GetReference().transform.position.x, 0f, agent.GetReference().transform.position.z), new Vector3(transform.position.x, 0f, transform.position.z));
                    if(dist < attackRadius)
                    {
                        OnAttackStart();
                        state = State.Attacking;
                    }
                }
            }
            movementDestination = pickMovementDestination(target);
            if(!ignoreMovementRotation)
            {
                LookAt(movementDestination);
            }
            timeOfNextStateChange = Time.time + data.timeBetweenModeChanges;
        }

        Act();
    }

    public abstract void OnAttackStart();

    public abstract void Attack();

    public abstract void OnAttackInterrupt();

    public abstract void OnAttackEnd();

    public void Staggered()
    {
        // Do nothing
    }

    public virtual void Stagger()
    {
        if(!firstStagger)
        {
            //LookAt(transform.forward); do stagger animation
            state = State.Staggered;
            timeOfNextStateChange = Time.time + 0.5f;
            OnAttackInterrupt();
        }
        firstStagger = false;
    }

    public void AlertEnemy()
    {
        target = agent.GetReference().transform;
        alerted = true;
        timeOfNextStateChange = Time.time + 0.1f;
    }

    public Vector3 pickMovementDestination(Transform target)
    {
        var radius = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)).normalized * attackRadius;
        if(target == null)
        {
            target = transform;
        }
        return new Vector3(target.position.x + radius.x, transform.localPosition.y, target.position.z + radius.y);
    }

    public void LookAt(Vector3 target)
    {
        gameObject.transform.LookAt(target, Vector3.up);
    }

    public bool DestinationReached(Vector3 destination)
    {
        if(transform.position == destination)
        {
            return true;
        }
        return false;
    }

    public void MoveTowardsDestination(Vector3 destination, float speed)
    {
        float singleStep = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(destination.x, transform.position.y, destination.z), singleStep);
        if(DestinationReached(movementDestination))
        {
            movementDestination = pickMovementDestination(target);
            if(!ignoreMovementRotation)
            {
                LookAt(movementDestination);
            }
        }
    }

    public abstract void Move(float speed);

    public void Act()
    {
        switch(state)
        {
            default:
            case State.Moving:
                Move(data.movementSpeed);
                break;

            case State.Attacking:
                Attack();
                break;

            case State.Staggered:
                Staggered();
                break;
        }
    }

    virtual protected void OnDisable()
    {
        healthComponent.OnHealthChangedEvent -= Stagger;
        intruderDetector.OnIntruderDetectedEvent -= AlertEnemy;
    }
}
