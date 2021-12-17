using Roguelike;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class RoguelikeAgent : Agent
{
    [Header("Values")]
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float dashStrength = 85f;
    [SerializeField] private float dashCooldown = 0.5f;
    [Header("Objects")]
    [SerializeField] private GameObject interact;
    [SerializeField] private GameObject attack;
    [Header("References")]
    [SerializeField] private GameObjectReference interactableReference;
    [Header("Events")]
    [SerializeField] private GameEvent runStartedEvent;
    [SerializeField] private GameEvent playerDashedEvent;
    [Space]
    [SerializeField] private Animator attackAnimator;

    private Rigidbody agent_rigidbody;
    private Health healthComponent;
    private StatsRecorder statsRecorder;
    // Stats
    private int roomsCleared = 0;
    private float timeOfRoomEnter = 0f;

    private int previousHealthAmount;
    private int previous_primary_action = 0;
    private int previous_dashing = 0;
    private int previous_left_mouse_button = 0;
    private float timeOfLastDash = 0;
    private float timeOfLastAttack = 0;
    private float actualMovementSpeed;

    [HideInInspector] public bool moveUpAvailable = true;
    [HideInInspector] public bool moveDownAvailable = true;
    [HideInInspector] public bool moveLeftAvailable = true;
    [HideInInspector] public bool moveRightAvailable = true;
    [HideInInspector] public bool attackAvailable = true;
    [HideInInspector] public bool interactAvailable = false;
    [HideInInspector] public bool dashAvailable = true;

    private bool movementDisabled;
    private bool isAttacking;
    private bool isDashing;

    private Vector3 dashDirection;

    MLHadesActions controls;

    public override void Initialize()
    {
        statsRecorder = Academy.Instance.StatsRecorder;
        agent_rigidbody = GetComponent<Rigidbody>();
        healthComponent = GetComponent<Health>();
        healthComponent.OnHealthDecreasedEvent += DamageTaken;
        actualMovementSpeed = movementSpeed;
        attackAvailable = true;
        controls = new MLHadesActions();
        controls.Enable();
    }

    public override void OnEpisodeBegin()
    {
        // Tracking additional stats
        statsRecorder.Add("Roguelike/Rooms Cleared", roomsCleared);

        runStartedEvent.Raise();
        healthComponent.ResetHealth();
        previousHealthAmount = healthComponent.GetHealth();
        roomsCleared = 0;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(dashDirection);
    }

    /// <summary>
    /// Performs action according to the selected action.
    /// </summary>
    public void AgentAct(ActionSegment<int> discreteActions, ActionSegment<float> continuousActions)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;
        float x = 0;
        float y = 0;

        var y_movement = discreteActions[0];
        var x_movement = discreteActions[1];
        var primary_action = discreteActions[2];
        var dashing = discreteActions[3];
        var attack_x = Mathf.Clamp(continuousActions[0], -1f, 1f);
        var attack_y = Mathf.Clamp(continuousActions[1], -1f, 1f);

        if(Time.time > timeOfLastAttack + 0.15f)
        {
            attackAvailable = true;
        }
        if(Time.time > timeOfLastDash + dashCooldown)
        {
            dashAvailable = true;
        }

        switch(y_movement)
        {
            case 1:
                y = 1;
                break;

            case 2:
                y = -1;
                break;
        }
        switch(x_movement)
        {
            case 1:
                x = 1;
                break;

            case 2:
                x = -1;
                break;
        }
        switch(primary_action)
        {
            case 1:
                if(attackAvailable && !attackAnimator.GetCurrentAnimatorStateInfo(0).IsName("SwordSlash_piercing"))
                {

                    var attack_angle = AngleToVector3Rotation(AsymetricAngleFromXY(attack_x,attack_y));
                    transform.eulerAngles = attack_angle;
                    isAttacking = true;
                    Attack();
                    timeOfLastAttack = Time.time;
                }
                break;

            case 2:
                if(previous_primary_action != 2)
                {
                    Interact();
                }
                break;
        }
        previous_primary_action = primary_action;

        float dashAngle;
        if(!(x == 0 && y == 0))
        {
            dashAngle = AsymetricAngleFromXY(x, y);
        }
        else
        {
            dashAngle = transform.eulerAngles.y;
        }
        dashDirection = AngleToVector3Direction(dashAngle).normalized;
        switch(dashing)
        {
            case 1:
                if(previous_dashing != 1 && dashAvailable)
                {
                    Dash();
                    timeOfLastDash = Time.time;
                }
                break;
        }
        previous_dashing = dashing;

        // Move if not dashing or attacking
        if(!(x == 0 && y == 0))
        {
            var direction = AngleToVector3Direction(AsymetricAngleFromXY(x, y));
            if(!isAttacking)
            {
                transform.eulerAngles = AngleToVector3Rotation(AsymetricAngleFromXY(x, y));
            }
            if(!isDashing && !movementDisabled)
            {

                agent_rigidbody.AddForce(direction * actualMovementSpeed, ForceMode.VelocityChange);
            }
        }
    }

    #region Helper Functions

    private float AsymetricAngleFromXY(float x,float y)
    {
        return Mathf.Atan2(y, x) * Mathf.Rad2Deg - 45;
    }

    private Vector3 AngleToVector3Direction(float angle)
    {
        return new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
    }

    private Vector3 AngleToVector3Rotation(float angle)
    {
        return new Vector3(0f, angle, 0f);
    }

    private void PropellTowards(float strength, Vector3 direction)
    {
        agent_rigidbody.AddForce(strength * direction, ForceMode.VelocityChange);
    }

    private void DisableMovement()
    {
        moveUpAvailable = false;
        moveDownAvailable = false;
        moveLeftAvailable = false;
        moveRightAvailable = false;
    }

    private void EnableMovement()
    {
        moveUpAvailable = true;
        moveDownAvailable = true;
        moveLeftAvailable = true;
        moveRightAvailable = true;
    }

    #endregion Helper Functions

    #region Attack
    public void Attack()
    {
        attackAnimator.SetTrigger("Attack");
        attackAvailable = false;
    }

    public void Combo1()
    {
        movementDisabled = false;
        actualMovementSpeed = movementSpeed * 0.5f;
    }
    public void Combo2()
    {
        movementDisabled = true;
    }
    public void Combo3()
    {
        movementDisabled = true;
        PropellTowards(dashStrength/2, transform.forward);
    }
    public void ComboReset()
    {
        actualMovementSpeed = movementSpeed;
    }
    public void AttackEnded()
    {
        isAttacking = false;
        movementDisabled = false;
    }
    #endregion Attack

    #region Interact
    public void Interact()
    {
        interact.GetComponent<Interact>().InteractWithObject();
    }
    #endregion

    #region Dash
    public void Dash()
    {
        if(Physics.OverlapSphere(transform.position + dashDirection * dashStrength/10f, 1f, 8192).Length==0)
        {
            gameObject.layer = 11; // set agent on Dashing layer
        }
        dashAvailable = false;
        isDashing = true;
        attackAnimator.SetTrigger("Interrupt"); // Interrupt the current attack
        PropellTowards(dashStrength, dashDirection);
        playerDashedEvent.Raise();
        Invoke("DashEnd", 0.25f);
    }

    private void DashEnd()
    {
        isDashing = false;
        gameObject.layer = 9; // set agent to Player layer
    }
    #endregion Dash

    public override void WriteDiscreteActionMask(IDiscreteActionMask actionMask)
    {
        if(interactableReference.GetReference() == null)
        {
            interactAvailable = false;
        }
        else
        {
            interactAvailable = true;
        }

        if(isDashing || movementDisabled)
        {
            DisableMovement();
        }
        else
        {
            EnableMovement();
        }

        var yBranch = 0;
        var xBranch = 1;
        var primaryBranch = 2;
        var dashBranch = 3;

        actionMask.SetActionEnabled(yBranch, 1, moveUpAvailable); // Up
        actionMask.SetActionEnabled(yBranch, 2, moveDownAvailable); // Down
        actionMask.SetActionEnabled(xBranch, 1, moveLeftAvailable); // Left
        actionMask.SetActionEnabled(xBranch, 2, moveRightAvailable); // Right
        actionMask.SetActionEnabled(primaryBranch, 1, attackAvailable); // Attack
        actionMask.SetActionEnabled(primaryBranch, 2, interactAvailable); // Interaction
        actionMask.SetActionEnabled(dashBranch, 1, dashAvailable); // Dash
    }

    /// <summary>
    /// Called every step of the engine. Here the agent takes an action.
    /// </summary>
    public override void OnActionReceived(ActionBuffers actionBuffers)

    {
        // Move the agent using the action.
        AgentAct(actionBuffers.DiscreteActions,actionBuffers.ContinuousActions);

        AddReward(-0.0001f); // Existential dread
        //Debug.Log(GetCumulativeReward());

        // Tacking additional stats
        statsRecorder.Add("Roguelike/Health", healthComponent.GetHealth());
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = 0f;
        continuousActionsOut[1] = 0f;

        Vector2 movement = controls.Gameplay.Movement.ReadValue<Vector2>();

        if(movement.y>0) // Move up
        {
            discreteActionsOut[0] = 1;
        }
        else if(movement.y<0) // Move down
        {
            discreteActionsOut[0] = 2;
        }
        else
        {
            discreteActionsOut[0] = 0; // Do nothing
        }

        if(movement.x<0) // Move to the left
        {
            discreteActionsOut[1] = 1;
        }
        else if(movement.x>0) // Move to the right
        {
            discreteActionsOut[1] = 2;
        }
        else
        {
            discreteActionsOut[1] = 0; // Do nothing
        }

        if(controls.Gameplay.Attack.ReadValue<float>()>0) // Attack
        {
            if(previous_left_mouse_button != 1)
            {
                discreteActionsOut[2] = 1;
            }
            Vector3 mousePos = new Vector2(controls.Gameplay.AttackDirectionX.ReadValue<float>(), controls.Gameplay.AttackDirectionY.ReadValue<float>());
            Vector2 mouseXY = new Vector2(mousePos.x - Screen.width / 2, mousePos.y - Screen.height / 2).normalized;
            continuousActionsOut[0] = -mouseXY.x;
            continuousActionsOut[1] = mouseXY.y;
        }
        else if(controls.Gameplay.Interact.ReadValue<float>()>0) // Interact
        {
            discreteActionsOut[2] = 2;
        }
        else
        {
            discreteActionsOut[2] = 0; // Do nothing
        }
        previous_left_mouse_button = (int)controls.Gameplay.Attack.ReadValue<float>();

        if(controls.Gameplay.Dash.ReadValue<float>()>0) // Dash
        {
            discreteActionsOut[3] = 1;
        }
        else
        {
            discreteActionsOut[3] = 0; // Do nothing
        }
    }

    #region Rewards
    public void DamageTaken()
    {
        var healthLost = previousHealthAmount - healthComponent.GetHealth();
        AddReward(-healthLost / 10f);
        previousHealthAmount = healthComponent.GetHealth();
    }

    public void AdvanceLevel()
    {
        AddReward(1f);
        roomsCleared += 1;
        // Tracking additional stats
        var timePerLevel = Time.time - timeOfRoomEnter;
        statsRecorder.Add("Roguelike/Time Per Room", timePerLevel);
        timeOfRoomEnter = Time.time;
    }

    public void RewardCollected()
    {
        AddReward(1f);
    }

    public void CoinsCollected()
    {
        AddReward(1f);
    }

    public void Death()
    {
        AddReward(-1f);
        EndEpisode();
    }
    #endregion Rewards

    private void OnDestroy()
    {
        healthComponent.OnHealthDecreasedEvent -= DamageTaken;
    }
}
