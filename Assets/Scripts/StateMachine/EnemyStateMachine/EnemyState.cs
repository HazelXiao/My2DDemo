using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Idle : EnemyStateBase
{
	private float _time;
	public override void Enter()
	{
		base.Enter();
	}

	public override void Update()
	{
		base.Update();
		if( !GameManager.instance.playersTurn && !GameManager.instance.isSetup)
		{
			_time += Time.deltaTime;
			if( _time > GameManager.instance.moveDelay )
			{
				stateMachine.ChangeState<EnemyState_Move>( State.Move, true );
				_time = 0;
			}
		}
	}
}

public class EnemyState_Move : EnemyStateBase
{
	private float _time;

	public override void Enter()
	{
		base.Enter();
	}

	public override void Update()
	{
		base.Update();
		_time += Time.deltaTime;
		if( _time > GameManager.instance.moveDelay )
		{
			_time = 0;
			foreach( var item in GameManager.instance.enemies )
			{
				item.MoveEnemy();
			}
			stateMachine.ChangeState<EnemyState_Idle>( State.Idle );
		}
	}
	public override void Exit()
	{
		base.Exit();

		GameManager.instance.playersTurn = true;
	}
}