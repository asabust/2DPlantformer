using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    // Start is called before the first frame update
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
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

        float slideSpeendFacter = yInput < 0 ? 1.05f : 0.7f;
        rb.velocity = new Vector2(0f, rb.velocity.y * slideSpeendFacter);

        if (player.jumpAction.IsPressed())
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if (player.IsGrounded() || (xInput != 0 && player.facingDir != xInput))
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (!player.IsWall())
        {
            stateMachine.ChangeState(player.airState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}