using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
	public static BGMManager instance = null;

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
	}

	private void Start()
	{
		AkSoundEngine.PostEvent( Labels.BGM, this.gameObject );
	}
}
