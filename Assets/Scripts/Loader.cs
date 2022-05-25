using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;

	private void Awake()
    {
        LoadGameManager();
    }

    private void LoadGameManager()
	{
        if( GameManager.instance == null )
        {
            Instantiate( gameManager );
        }
    }
}
