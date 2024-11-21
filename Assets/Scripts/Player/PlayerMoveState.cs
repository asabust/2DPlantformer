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

        // StairMove();
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (xInput == 0 || player.IsWall())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    private void StairMove()
    {
        // 在斜坡上用射线可能检测不到地面，以后改成别的地面检测再试
        RaycastHit2D hit = player.IsStairsDetected();
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