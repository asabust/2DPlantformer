using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
        stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();
        if (xInput == player.facingDir && player.IsWall()) return;
        if (xInput != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}