using System;
using System.Collections.Generic;
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

    //地图大小
    public int columns = 10;
    
    public int rows = 10;
    
    //每一关随机产生内墙的数量（范围5-9）
     public CountRange wallCountRange = new CountRange( 5, 9 );
    
    //每一关随机产生食物的数量（范围1-5）
     public CountRange foodCountRange = new CountRange( 1, 5 );
    
    //每一关随机产生饮料的数量（范围0-3）
     public CountRange sodaCountRange = new CountRange( 0, 3 );

    #region 场景物品（出口、地板、内墙、外墙、食物、敌人）
    
    public GameObject exit;
    
    public GameObject[] floorTiles;
    
    public GameObject[] innerWallTiles;
    
    public GameObject[] outerWallTiles;
    
    public GameObject[] foodTiles;
    
    public GameObject[] enemyTiles;

    #endregion
    
    //存储已经生成过物品的位置
    private List<Vector3> _gridPositions = new List<Vector3>();

}
