using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterMove
{
	public int playerDamage;

	private Animator _animator;

	private Transform _target;

	private PlayerController _player;


	protected override void Start()
	{
		GameManager.instance.AddEnemy( this );

		_animator = GetComponent<Animator>();
		_target = GameObject.Find( Labels.Player ).transform;
		_player = _target.GetComponent<PlayerController>();

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
		var playerGrid = _player.currentGrid;

		// ��Һ͵���֮�� x ����ľ��룬�Ƿ���ͬһ��
		if( playerGrid.x == currentGrid.x )
		{
			// y ����������ƶ����������ƶ�
			if( playerGrid.y > currentGrid.y )
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
			if( playerGrid.x > currentGrid.x )
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

		_animator.SetTrigger( Labels.Trigger_EnemyAttack );

		AkSoundEngine.PostEvent( Labels.EnemyAttack, this.gameObject );

		player.LoseFood( playerDamage );
	}
}
