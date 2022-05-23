using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateBase : StateBase
{
	protected Animator animator;

	protected EnemyController enemyController;

	public override void Initialize( IStateMachineOwner owner, StateMachine stateMachine )
	{
		base.Initialize( owner, stateMachine );

		var enemyStateMachine = owner as EnemyStateMachine;

		animator = enemyStateMachine.animator;

		enemyController = enemyStateMachine.controller;
	}
}
