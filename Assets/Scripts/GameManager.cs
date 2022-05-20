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

	private int _level = 0;

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

		InitGame();
	}

	private void InitGame()
	{
		boardManager.SetupScene( _level );
	}

	public void GameOver()
	{
		enabled = false;
	}
}
