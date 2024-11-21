using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask playerLayer;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float idleTime = 2f;

    [Header("Attack")]
    public float attackDistance = 5f;

    public EnemyStateMachine stateMachine;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public void MoveToward(int direction)
    {
        RaycastHit2D hit = IsStairsDetected();
        if (hit.collider is not null && !IsWall())
        {
            // Debug.Log($"{hit.normal}, {skeleton.facingDir}");
            SetVelocity(moveSpeed * direction * hit.normal.y,
                direction * -hit.normal.x);
        }
        else
        {
            SetVelocity(moveSpeed * direction, rb.velocity.y);
        }
    }

    public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishedTrigger();

    public virtual RaycastHit2D IsPlayerDetected() =>
        Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, playerLayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheck.position,
            new Vector3(transform.position.x + attackDistance * facingDir, wallCheck.position.y));
    }
}