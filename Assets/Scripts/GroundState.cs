using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : PlayerState
{
    public GroundState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine,
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