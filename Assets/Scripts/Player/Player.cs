using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    public bool isBusy { get; private set; }

    [Header("Attack info")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = 0.2f;


    [Header("Movement")]
    public float moveSpeed = 10.0f;
    public float jumpForce = 12.0f;
    public float wallJumpSpeed = 5f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.5f;

#region Input

    public Vector2 moveInputValue { get; private set; }
    public InputAction moveAction { get; private set; }
    public InputAction attackAction { get; private set; }
    public InputAction jumpAction { get; private set; }
    public InputAction dashAction { get; private set; }
    public InputAction counterAttackAction { get; private set; }

#endregion

#region States

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }

#endregion

    public SkillManager skill { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "idle");
        moveState = new PlayerMoveState(this, stateMachine, "move");
        jumpState = new PlayerJumpState(this, stateMachine, "jump");
        airState = new PlayerAirState(this, stateMachine, "jump");
        dashState = new PlayerDashState(this, stateMachine, "dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "counterAttack");
    }

    protected override void Start()
    {
        base.Start();
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Fire");
        counterAttackAction = InputSystem.actions.FindAction("CounterAttack");
        jumpAction = InputSystem.actions.FindAction("Jump");
        dashAction = InputSystem.actions.FindAction("Dash");
        dashAction.started += Dash;
        stateMachine.Initialize(idleState);

        skill = SkillManager.instance;
    }

    protected override void Update()
    {
        base.Update();
        moveInputValue = moveAction.ReadValue<Vector2>();
        stateMachine.currentState.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    public IEnumerator BusyFor(float waitTime)
    {
        isBusy = true;
        yield return new WaitForSeconds(waitTime);
        isBusy = false;
    }

    public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishedTrigger();


    private void Dash(InputAction.CallbackContext obj)
    {
        if (IsWall()) return;
        if (SkillManager.instance.dash.CanUseSkill())
        {
            stateMachine.ChangeState(dashState);
        }
    }
}