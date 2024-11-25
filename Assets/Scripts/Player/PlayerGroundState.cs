using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
        stateMachine,
        animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (player.counterAttackAction.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.counterAttackState);
        }

        if (player.attackAction.IsPressed())
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }

        if (!player.IsGrounded())
        {
            stateMachine.ChangeState(player.airState);
        }

        if (player.jumpAction.WasPressedThisFrame() && player.IsGrounded())
        {
            stateMachine.ChangeState(player.jumpState);
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