using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
	public GameObject gameManager;

	public GameObject BGM;

	private void Awake()
	{
		LoadGameManager();
	}
	private void Start()
	{
		LoadBGM();
	}

	private void LoadGameManager()
	{
		if( GameManager.instance == null )
		{
			Instantiate( gameManager );
		}
	}

	private void LoadBGM()
	{
		if( BGMManager.instance == null )
		{
			Instantiate( BGM );
		}
	}
}
