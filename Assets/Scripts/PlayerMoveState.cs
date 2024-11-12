using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
        stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
    }
}