using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Idle : PlayerStateBase
{
	public override void Enter()
	{
		base.Enter();

		Debug.Log( "Idle" );
		animator.Play( "Idle" );
	}

	public override void Update()
	{
		base.Update();

		int horizontal = (int)Input.GetAxisRaw( "Horizontal" );
		int vertical = (int)Input.GetAxisRaw( "Vertical" );

		if( horizontal != 0 || vertical != 0 )
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
		animator.Play( "Idle" );
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