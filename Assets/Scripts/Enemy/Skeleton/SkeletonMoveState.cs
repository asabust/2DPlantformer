using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : EnemyState
{
    private EnemySkeleton enemy;

    public SkeletonMoveState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy,
        _stateMachine, _animBoolName)
    {
        enemy = _enemy as EnemySkeleton;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.IsWall() || !enemy.IsGrounded())
        {
            // Debug.Log($"{skeleton.IsWall()}, {skeleton.IsGrounded()}");
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        StairsMove();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void StairsMove()
    {
        RaycastHit2D hit = enemy.CheckStairs();
        if (hit.collider is not null && !enemy.IsWall())
        {
            // Debug.Log($"{hit.normal}, {skeleton.facingDir}");
            enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir * hit.normal.y,
                enemy.facingDir * -hit.normal.x);
        }
        else
        {
            enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, enemy.rb.velocity.y);
        }
    }
}