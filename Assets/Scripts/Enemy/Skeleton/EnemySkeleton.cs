using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Enemy
{
#region State

    public SkeletonIdleState idleState;
    public SkeletonMoveState moveState;
    public SkeletonBattleState battleState;
    public SkeletonAttackState attackState;
    public SkeletonStunState stunState;

#endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, stateMachine, "idle");
        moveState = new SkeletonMoveState(this, stateMachine, "move");
        battleState = new SkeletonBattleState(this, stateMachine, "move");
        attackState = new SkeletonAttackState(this, stateMachine, "attack");
        stunState = new SkeletonStunState(this, stateMachine, "stun");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunState);
            return true;
        }

        return false;
    }
}