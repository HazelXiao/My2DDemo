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
    public int playerFootPoints = 100;

    private int _level = 0;

    private void Awake()
    {
        if( instance == null )
        {
            instance = this;
        }
        else if( instance != this )
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
        
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
