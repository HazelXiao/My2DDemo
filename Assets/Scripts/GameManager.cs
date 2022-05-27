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

	public float nextLevelDelay = 1f;

	private int _level = 1;

	public List<EnemyController> enemies = new List<EnemyController>();

	private Text _levelText;

	private GameObject _levelImage;

	private GameObject _mainPanel;

	private Button _startButton;

	private Button _exitButton;

	private Button _restartButton;

	public bool isSetup;

	public bool isShowMainPanel = true;

	public bool isShowLevelImage = true;

	public bool gameStart = false;

	public bool isExit = false;

	private float _time = 0;

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

	public void GoToNextLevel()
	{
		SceneManager.sceneLoaded += OnSceneLoad;
		SceneManager.LoadScene( Labels.MainScene );
	}

	private void OnSceneLoad( Scene scene, LoadSceneMode mode )
	{
		SceneManager.sceneLoaded -= OnSceneLoad;

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
		isShowLevelImage = true;
		gameStart = false;

		_levelImage = GameObject.Find( Labels.LevelImage );
		_mainPanel = GameObject.Find( Labels.MainPanel );
		_levelText = GameObject.Find( Labels.LevelText ).GetComponent<Text>();
		_restartButton = GameObject.Find( Labels.RestartButton ).GetComponent<Button>();
		_startButton = GameObject.Find( Labels.StartButton ).GetComponent<Button>();
		_exitButton = GameObject.Find( Labels.ExitButton ).GetComponent<Button>();

		ShowMainPanle();
		ShowLevelImage();

		enemies.Clear();
		boardManager.SetupScene( _level );
	}

	private void ExitButtonOnClick()
	{
		Application.Quit();
	}

	private void StartButtonOnClick()
	{
		_mainPanel.SetActive( false );
		isShowMainPanel = false;
	}

	private void ShowMainPanle()
	{
		if( isShowMainPanel )
		{
			_mainPanel.SetActive( true );

			_startButton.onClick.AddListener( StartButtonOnClick );
			_exitButton.onClick.AddListener( ExitButtonOnClick );
		}
		else
		{
			_mainPanel.SetActive( false );
		}
	}

	private void ShowLevelImage()
	{
		_levelText.text = Labels.Day + _level;
		_levelImage.SetActive( true );
		_restartButton.gameObject.SetActive( false );
		_restartButton.onClick.AddListener( RestartGame );
	}

	private void HideLevelImage()
	{
		_levelImage.SetActive( false );
		isSetup = false;
		gameStart = true;
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
		_levelImage.SetActive( true );
		_restartButton.gameObject.SetActive( true );

		gameStart = false;
		enabled = false;


	}

	public void Update()
	{
		if( !isShowMainPanel && isShowLevelImage )
		{
			_time += Time.deltaTime;
			if( _time >= leveStartDelay )
			{
				HideLevelImage();
				_time = 0;
				isShowLevelImage = false;
			}
		}

		if( isExit )
		{
			_time += Time.deltaTime;
			if( _time >= nextLevelDelay )
			{
				GoToNextLevel();
				_time = 0;
				isExit = false;
			}
		}
	}

	public void RestartGame()
	{
		Destroy( gameObject );
		SceneManager.LoadScene( Labels.MainScene );
	}
}
