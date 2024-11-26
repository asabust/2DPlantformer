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
        player = PlayerManager.instance.player.transform;
    }

    public override void Update()
    {
        base.Update();

        RaycastHit2D hit = enemy.IsPlayerDetected();
        if (hit.collider is not null)
        {
            stateTimer = enemy.battleTime;
            if (hit.distance < enemy.attackDistance)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);
                }
            }
        }
        else
        {
            //TODO : 暂定仇恨范围10。设置好场景之后再调整。
            if (stateTimer < 0 || Vector3.Distance(player.position, enemy.transform.position) < 10)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }

        if (Vector3.Distance(player.position, enemy.transform.position) > 0.1)
            moveDir = player.position.x > enemy.transform.position.x ? 1 : -1;
        enemy.MoveToward(moveDir, enemy.chaseSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}