using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
        stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skill.clone.CreateClone(player.transform.position);
        stateTimer = player.dashDuration;
    }

    public override void Update()
    {
        base.Update();
        if (!player.IsGrounded() && player.IsWall())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }

        player.SetVelocity(player.facingDir * player.dashSpeed, 0);
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}