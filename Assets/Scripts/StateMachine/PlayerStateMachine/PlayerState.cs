using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Idle : PlayerStateBase
{
	public override void Enter()
	{
		base.Enter();

		Debug.Log( "Idle" );
	}

	public override void Update()
	{
		base.Update();

		if( Input.GetKeyDown( KeyCode.W ) || Input.GetKeyDown( KeyCode.S ) || Input.GetKeyDown( KeyCode.A ) || Input.GetKeyDown( KeyCode.D ) )
		{
			stateMachine.ChangeState<PlayerState_Move>( State.Move, true );
		}

	}
}

public class PlayerState_Move : PlayerStateBase
{
	public override void Enter()
	{
		base.Enter();

		Debug.Log( "move" );
		playerController.Move();
	}

	public override void Update()
	{
		base.Update();

		playerController.Move();

	}
}

public class PlayerState_Chop : PlayerStateBase
{
	public override void Enter()
	{
		base.Enter();
	}

	public override void Update()
	{
		base.Update();
	}
}

public class PlayerState_Hit : PlayerStateBase
{
	public override void Enter()
	{
		base.Enter();
	}

	public override void Update()
	{
		base.Update();
	}
}