using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour, IStateMachineOwner
{
	public Animator animator;

	public PlayerController controller;

	private StateMachine _stateMachine;

	private void Start()
	{
		_stateMachine = new StateMachine();
		_stateMachine.Initialize( this );
		_stateMachine.ChangeState<PlayerState_Idle>( State.Idle );

	}

	private void Update()
	{
		_stateMachine.Update();
	}
}
