using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterMove
{
	public int playerDamage;

	private Animator _animator;

	private Transform _target;


	protected override void Start()
	{
		GameManager.instance.AddEnemyToList( this );

		_animator = GetComponent<Animator>();
		_target = GameObject.FindGameObjectWithTag( "Player" ).transform;

		base.Start();
	}

	protected override void TryMove<T>( int x, int y )
	{
		base.TryMove<T>( x, y );
	}

	public void MoveEnemy()
	{
		int x = 0;
		int y = 0;

		// 玩家和敌人之间 x 坐标的距离，是否在同一行
		if( Mathf.Abs( _target.position.x - transform.position.x ) <= 0 )
		{
			// y 坐标该向上移动还是向下移动
			if( _target.position.y > transform.position.y )
			{
				y = 1;
			}
			else
			{
				y = -1;
			}
		}
		else
		{
			if( _target.position.x > transform.position.y )
			{
				x = 1;
			}
			else
			{
				x = -1;
			}
		}

		TryMove<PlayerController>( x, y );
	}

	protected override void OnCantMove<T>( T component )
	{
		PlayerController player = component as PlayerController;

		_animator.SetTrigger( "enemyAttack" );

		player.LoseFood( playerDamage );
	}
}
