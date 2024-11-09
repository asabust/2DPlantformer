using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
#region Components

    public Animator anim { get; private set; }

#endregion

#region States
    public PlayerStateMachine stateMachine { get; private set; }
    
    public PlayerState idleState { get; private set; }
    public PlayerState moveState { get; private set; }
#endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "idle");
        moveState = new PlayerIdleState(this, stateMachine, "move");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }
}
 