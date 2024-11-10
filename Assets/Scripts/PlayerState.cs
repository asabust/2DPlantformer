using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;


    protected float xInput;
    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        rb = player.rb;
        player.anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        xInput = Input.GetAxis("Horizontal");
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
}