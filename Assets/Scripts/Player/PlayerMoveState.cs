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

        if (xInput == 0 || player.IsWall())
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        // StairMove();
    }

    private void StairMove()
    {
        RaycastHit2D hit = player.CheckStairs();
        if (hit.collider is not null && !player.IsWall())
        {
            player.SetVelocity(xInput * player.moveSpeed * hit.normal.y,
                xInput * -hit.normal.x);
        }
        else
        {
            player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}