using UnityEngine;

public class Player : MonoBehaviour
{
#region Info

    public float moveSpeed = 10.0f;

#endregion

#region Components

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

#endregion

#region States

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerState idleState { get; private set; }
    public PlayerState moveState { get; private set; }

#endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "idle");
        moveState = new PlayerMoveState(this, stateMachine, "move");
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