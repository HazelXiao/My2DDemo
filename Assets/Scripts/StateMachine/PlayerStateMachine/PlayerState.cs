using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Idle : PlayerStateBase
{
	public override void Enter()
	{
		base.Enter();
	}

	public override void Update()
	{
		base.Update();

		if( GameManager.instance.gameStart && GameManager.instance.playersTurn )
		{
			if( Input.GetKeyDown( KeyCode.W ) || Input.GetKeyDown( KeyCode.S ) || Input.GetKeyDown( KeyCode.A ) || Input.GetKeyDown( KeyCode.D ) )
			{
				stateMachine.ChangeState<PlayerState_Move>( State.Move, true );
			}
		}
	}
}

public class PlayerState_Move : PlayerStateBase
{

	public override void Enter()
	{
		base.Enter();

		AkSoundEngine.PostEvent( Labels.PlayerFootSteps, playerController.gameObject );
		playerController.Move();
	}

	public override void Update()
	{
		base.Update();
		stateMachine.ChangeState<PlayerState_Idle>( State.Idle );
	}

	public override void Exit()
	{
		base.Exit();
		GameManager.instance.playersTurn = false;
	}
}