using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : CharacterMove
{
	private Animator _animator;

	public int wallDamage = 1;

	public int foodPoints = 10;

	public int sodaPoints = 20;

	public float restartLevelDelay = 1f;

	/// <summary>
	/// 关卡中存储积分
	/// </summary>
	private int _points;

	protected override void Start()
	{
		_animator = GetComponent<Animator>();

		_points = GameManager.instance.playerFoodPoints;

		base.Start();
	}

	protected override void TryMove<T>( int x, int y )
	{
		_points--;

		base.TryMove<T>( x, y );

		CheckIfGameOver();
		GameManager.instance.playersTurn = false;
		Debug.Log("Player_currentGrid : " + currentGrid);
	}

	protected override void OnCantMove<T>( T component )
	{
		Wall hitWall = component as Wall;

		hitWall.DamageWall( wallDamage );

		_animator.SetTrigger( "playerChop" );
	}

	public void LoseFood( int losePoint )
	{
		_animator.SetTrigger( "playerHit" );
		_points -= losePoint;

		CheckIfGameOver();
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		var go = collision.gameObject;

		TriggerObjectType currentType = collision.GetComponent<ObjectType>().type;

		if( currentType == TriggerObjectType.Exit )
		{
			Invoke( "Restart", restartLevelDelay );
			enabled = false;
		}
		else if( currentType == TriggerObjectType.Food )
		{
			_points += foodPoints;
			go.SetActive( false );
		}
		else if( currentType == TriggerObjectType.Soda )
		{
			_points += sodaPoints;
			go.SetActive( false );
		}
	}

	public void Move()
	{
		int changeX = 0;
		int changeY = 0;

		if( Input.GetKeyDown( KeyCode.W ) )
		{
			changeY += 1;
		}
		else if( Input.GetKeyDown( KeyCode.S ) )
		{
			changeY -= 1;
		}
		else if( Input.GetKeyDown( KeyCode.D ) )
		{
			changeX += 1;
		}
		else if( Input.GetKeyDown( KeyCode.A ) )
		{
			changeX -= 1;
		}

		if( changeX != 0 )
		{
			changeY = 0;
		}

		if( changeX != 0 || changeY != 0 )
		{
			TryMove<Wall>( changeX, changeY );
		}
	}

	private void OnDisable()
	{
		GameManager.instance.playerFoodPoints = _points;
	}

	private void CheckIfGameOver()
	{
		if( _points <= 0 )
		{
			GameManager.instance.GameOver();
		}
	}

	public void Restart()
	{
		SceneManager.LoadScene( "Main" );
	}
}
