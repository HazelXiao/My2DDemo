using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

	public float leveStartDelay = 2f;

	private int _level = 1;

	public List<EnemyController> enemies = new List<EnemyController>();

	private Text _levelText;

	private GameObject _levelImage;

	private Button _restartButton;

	public bool isSetup;

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

	public void OnSceneLoad( Scene scene,LoadSceneMode mode )
	{
		_level++;

		InitializeGame();

	}

	public void AddEnemy( EnemyController enemy )
	{
		enemies.Add( enemy );
	}

	private void InitializeGame()
	{
		isSetup = true;

		_levelImage = GameObject.Find( Labels.LevelImage );
		_levelText = GameObject.Find( Labels.LevelText ).GetComponent<Text>();
		_restartButton = GameObject.Find( Labels.RestartButton ).GetComponent<Button>();

		_levelText.text = Labels.Day + _level;
		_levelImage.SetActive( true );
		_restartButton.onClick.AddListener( RestartGame );
		//_restartButton.gameObject.SetActive( false );

		Invoke( Labels.HideLevelImage, leveStartDelay );

		enemies.Clear();
		boardManager.SetupScene( _level );
	}

	private void HideLevelImage()
	{
		_levelImage.SetActive( false );
		isSetup = false;
	}

	public void GameOver()
	{
		if( _level > 1 )
		{
			_levelText.text = Labels.GameOverText_1 + _level + Labels.GameOverText_3;
		}
		else
		{
			_levelText.text = Labels.GameOverText_1 + _level + Labels.GameOverText_2;
		}
		_levelImage.SetActive ( true );
		_restartButton.gameObject.SetActive ( true );

		enabled = false;


	}

	public void RestartGame()
	{

	}
	private void OnDestroy()
	{
	}
}
