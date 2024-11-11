using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10.0f;
    public float jumpForce = 12.0f;

    [Header("Collision info")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

#region Components

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

#endregion

#region States

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerState idleState { get; private set; }
    public PlayerState moveState { get; private set; }
    public PlayerState jumpState { get; private set; }
    public PlayerState airState { get; private set; }

#endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "idle");
        moveState = new PlayerMoveState(this, stateMachine, "move");
        jumpState = new PlayerJumpState(this, stateMachine, "jump");
        airState = new PlayerAirState(this, stateMachine, "jump");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }


    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }
}