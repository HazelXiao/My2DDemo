using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateBase : StateBase
{
	protected Animator animator;

	protected PlayerController playerController;

	public override void Initialize( IStateMachineOwner owner, StateMachine stateMachine )
	{
		base.Initialize( owner, stateMachine );

		var playerStateMachine = owner as PlayerStateMachine;

		animator = playerStateMachine.animator;

		playerController = playerStateMachine.controller;
	}
}
