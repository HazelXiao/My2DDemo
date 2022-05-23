using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour, IStateMachineOwner
{
	public Animator animator;

	public EnemyController controller;

	private StateMachine _stateMachine;

	private void Start()
	{
		_stateMachine = new StateMachine();
		_stateMachine.Initialize( this );
		_stateMachine.ChangeState<EnemyState_Idle>( State.Idle );

	}

	private void Update()
	{
		_stateMachine.Update();
	}
}
