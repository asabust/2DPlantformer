using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunState : EnemyState
{
    private EnemySkeleton enemy;

    public SkeletonStunState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy,
        _stateMachine, _animBoolName)
    {
        enemy = _enemy as EnemySkeleton;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.fx.InvokeRepeating("RadColorBlink", 0f, 0.1f);
        stateTimer = enemy.stunDuration;
        enemy.rb.velocity = new Vector2(-enemy.facingDir * enemy.stunFource.x, enemy.stunFource.y);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("StopRadColorBlink", 0);
    }
}