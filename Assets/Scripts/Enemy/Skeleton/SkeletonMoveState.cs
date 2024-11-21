using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundState
{
    public SkeletonMoveState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy,
        _stateMachine, _animBoolName)
    {
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
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }

        enemy.MoveToward(enemy.facingDir);
    }

    public override void Exit()
    {
        base.Exit();
    }
}