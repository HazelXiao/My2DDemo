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

		animator = ( owner as PlayerStateMachine ).animator;

		playerController = ( owner as PlayerStateMachine ).controller;
	}
}
