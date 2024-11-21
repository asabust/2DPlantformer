using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    protected EnemySkeleton enemy;

    public SkeletonGroundState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy,
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
        if (enemy.IsPlayerDetected().collider is not null)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}