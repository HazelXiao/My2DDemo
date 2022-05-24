using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	public BoardManager boardManager;

	/// <summary>
	/// 总步数
	/// </summary>
	public int playerFoodPoints = 100;

	/// <summary>
	/// 玩家回合（ false 敌人行动 ）
	/// </summary>
	[HideInInspector]
	public bool playersTurn = true;

	public float moveDelay = 0.3f;

	private int _level = 0;

	public List<EnemyController> enemies = new List<EnemyController>();

	private void Awake()
	{
		if( instance == null )
		{
			instance = this;
		}
		else if( instance != this )
		{
			Destroy( gameObject );
		}

		DontDestroyOnLoad( gameObject );

		boardManager = GetComponent<BoardManager>();

		InitializeGame();
	}

	public void AddEnemy( EnemyController enemy )
	{
		enemies.Add( enemy );
	}

	private void InitializeGame()
	{
		enemies.Clear();
		boardManager.SetupScene( _level );
	}

	public void GameOver()
	{
		enabled = false;
	}
}
