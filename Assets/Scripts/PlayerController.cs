using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterMove
{
	private Animator _animator;

	protected override void Start()
	{
		_animator = GetComponent<Animator>();

		base.Start();
	}

	protected override void TryMove<T>( int x, int y )
	{
		base.TryMove<T>( x, y );
		// Todo : 步数减少、游戏结束判断（步数是否清空）

	}

	protected override void OnCantMove<T>( T component )
	{
		Wall hitWall = component as Wall;
		// Todo : 打内墙可以拆掉，因为内墙没写逻辑，先放着
	}
	public void Move()
	{
		int horizontal = (int)Input.GetAxisRaw( "Horizontal" );
		int vertical = (int)Input.GetAxisRaw( "Vertical" );

		if( horizontal != 0 )
		{
			vertical = 0;
		}

		if( horizontal != 0 || vertical != 0 )
		{
			TryMove<Wall>( horizontal, vertical );
		}
	}

	private void Update()
	{
	}
}
