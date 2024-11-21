using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private EnemySkeleton enemy;
    private int moveDir;

    public SkeletonBattleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy,
        _stateMachine, _animBoolName)
    {
        enemy = _enemy as EnemySkeleton;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Update()
    {
        base.Update();

        RaycastHit2D hit = enemy.IsPlayerDetected();
        if (hit.collider is not null)
        {
            moveDir = player.position.x > enemy.transform.position.x ? 1 : -1;
            enemy.MoveToward(moveDir);
            if (hit.distance < enemy.attackDistance)
            {
                stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}