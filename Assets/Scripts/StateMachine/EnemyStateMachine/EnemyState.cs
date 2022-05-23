using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Idle : EnemyStateBase
{
	public override void Enter()
	{
		base.Enter();

		Debug.Log( "Idle_Enemy" );
	}

	public override void Update()
	{
		base.Update();
		if( !GameManager.instance.playersTurn )
		{
			stateMachine.ChangeState<EnemyState_Move>( State.Move, true );

			Debug.Log( "µ–»À“∆∂Ø" );
		}

	}
}

public class EnemyState_Move : EnemyStateBase
{
	public override void Enter()
	{
		base.Enter();

		Debug.Log( "EnemyState_Move : Enter" );
		foreach( var item in GameManager.instance.enemys )
		{
			Debug.Log( "enemymove" );
			item.MoveEnemy();
		}
		GameManager.instance.playersTurn = true;
	}

	public override void Update()
	{
		base.Update();
		if( GameManager.instance.playersTurn )
		{
			stateMachine.ChangeState<EnemyState_Idle>( State.Idle );
		}
	}
}