using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    //随机数取值范围
    [Serializable]
    public class CountRange
    {
        public int minimum;
        
        public int maximun;

        public CountRange( int min, int max )
        {
            minimum = min;
            maximun = max;
        }
    }

    //地板大小
    public int columns = 8;
    
    public int rows = 8;
    
    //每一关随机产生内墙的数量（范围5-9）
     public CountRange wallCountRange = new CountRange( 5, 9 );
    
    //每一关随机产生食物的数量（范围1-5）
     public CountRange foodCountRange = new CountRange( 1, 5 );

    #region 场景物品（出口、地板、内墙、外墙、食物、敌人）
    
    public GameObject exitTile;
    
    public GameObject[] floorTiles;
    
    public GameObject[] innerWallTiles;
    
    public GameObject[] outerWallTiles;
    
    public GameObject[] foodTiles;
    
    public GameObject[] enemyTiles;

    #endregion
    
    //画板父物体，让地图都生成在它里面
    private Transform _board;
    
    //存储除了外墙和固定通路外的可生成其他物品的地块
    private List<Vector3> _gridPositions = new List<Vector3>();

    /// <summary>
    /// 存储可生成items的位置
    /// </summary>
    private void InitGridPositionsList()
    {
        _gridPositions.Clear();

        //上下左右都空出一行可行动位置，避免后面随机生成物品把路堵死
        for ( int i = 1; i < columns - 1; i++ )
        {
            for ( int j = 1; j < rows - 1; j++ )
            {
                //把生成过的地块存起来，可以在这些位置生成items
                _gridPositions.Add( new Vector3( i, j, 0f ) );
            }
        }
    }

    /// <summary>
    /// 生成画板（外墙和地面）
    /// </summary>
    private void SetUpBoard()
    {
        _board = new GameObject( "Board" ).transform;

        //外墙（比地板多出一块），上下左右 + 1
        for ( int i = -1; i < columns + 1; i++ )
        {
            for ( int j = -1; j < rows + 1; j++ )
            {
                //准备生成地板块
                GameObject waitInstantiate = floorTiles[ Random.Range( 0, floorTiles.Length ) ];
                
                //准备生成外墙块
                if ( i == -1 || i == columns || j == -1 || j == rows )
                {
                    waitInstantiate = outerWallTiles[ Random.Range( 0, outerWallTiles.Length ) ];
                }
                
                GameObject instantiateGO = Instantiate( waitInstantiate, new Vector3( i, j, 0f ), Quaternion.identity ) as GameObject;
                
                instantiateGO.transform.SetParent( _board );
            }
        }
    }

    /// <summary>
    /// 获取一个随机的生成物品位置
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRandomPosition()
    {
        int randomIndex = Random.Range( 0, _gridPositions.Count );

        Vector3 randomPosition = _gridPositions[ randomIndex ];
        _gridPositions.RemoveAt( randomIndex );

        return randomPosition;
    }

    /// <summary>
    /// 生成Items
    /// </summary>
    /// <param name="tilesArray">要生成的对象数组</param>
    /// <param name="minimum">生成最小数量</param>
    /// <param name="maximun">生成最大数量</param>
    private void InstantiateObjectAtRandom( GameObject[] tilesArray, int minimum, int maximun )
    {
        int objectCount = Random.Range( minimum, maximun );

        for ( int i = 0; i < objectCount; i++ )
        {
            Vector3 randomPosition = GetRandomPosition();
            
            GameObject tileChoice = tilesArray[ Random.Range( 0, tilesArray.Length ) ];

            Instantiate( tileChoice, randomPosition, Quaternion.identity );
        }
    }

    /// <summary>
    /// 布景
    /// </summary>
    /// <param name="level">关卡数</param>
    public void SetUpScene( int level )
    {
        SetUpBoard();
        InitGridPositionsList();
        
        InstantiateObjectAtRandom( innerWallTiles, wallCountRange.minimum, wallCountRange.maximun );
        InstantiateObjectAtRandom( foodTiles, foodCountRange.minimum, foodCountRange.maximun );

        int enemyCount = level + 1;
        InstantiateObjectAtRandom( enemyTiles,enemyCount,enemyCount );
        
        Instantiate( exitTile, new Vector3( columns - 1, rows - 1, 0f ),Quaternion.identity );
    }
}
