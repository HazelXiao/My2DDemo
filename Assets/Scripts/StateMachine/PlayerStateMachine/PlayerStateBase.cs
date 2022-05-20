using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateBase : StateBase
{
	protected Animator animator;

	protected PlayerController playerController;

	public override void Init( IStateMachineOwner owner, StateMachine stateMachine )
	{
		base.Init( owner, stateMachine );

		var playerStateMachine = owner as PlayerStateMachine;

		animator = playerStateMachine.animator;

		playerController = playerStateMachine.controller;
	}
}
