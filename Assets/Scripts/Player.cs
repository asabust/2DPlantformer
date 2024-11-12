using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public Vector2 moveInputValue;
    public float moveSpeed = 10.0f;
    public float jumpForce = 12.0f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.5f;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 0.4f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance = 0.4f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

#region Components

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

#endregion

#region Input

    public Vector2 inputValue { get; private set; }
    public InputAction moveAction { get; private set; }
    public InputAction jumpAction { get; private set; }
    public InputAction DashAction { get; private set; }

#endregion

#region States

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerState idleState { get; private set; }
    public PlayerState moveState { get; private set; }
    public PlayerState jumpState { get; private set; }
    public PlayerState airState { get; private set; }
    public PlayerState dashState { get; private set; }

#endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "idle");
        moveState = new PlayerMoveState(this, stateMachine, "move");
        jumpState = new PlayerJumpState(this, stateMachine, "jump");
        airState = new PlayerAirState(this, stateMachine, "jump");
        dashState = new PlayerDashState(this, stateMachine, "dash");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        DashAction = InputSystem.actions.FindAction("Dash");
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        moveInputValue = moveAction.ReadValue<Vector2>();
        stateMachine.currentState.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }


    public void SetVelocity(float x, float y)
    {
        rb.velocity = new Vector2(x, y);
        FlipController(x);
    }

    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)
    {
        if ((_x > 0 && !facingRight) || (_x < 0 && facingRight))
            Flip();
    }

    public bool IsGrounded() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position,
            new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position,
            new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}