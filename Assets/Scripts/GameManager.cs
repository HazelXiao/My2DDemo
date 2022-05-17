using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public BoardManager boardManager;

    private int _level = 0;

    private void Awake()
    {
        if ( instance == null )
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
        boardManager.SetUpScene( _level );
    }
}
