using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
	/// <summary>
	/// ײǽ�󹥻�ǽ��ͼƬ
	/// </summary>
	public Sprite damageSprite;

	public int hp = 4;

	private SpriteRenderer _spriteRenderer;

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void DamageWall( int loseHP )
	{
		_spriteRenderer.sprite = damageSprite;
		hp -= loseHP;

		if( hp <= 0 )
		{
			gameObject.SetActive( false );
		}
	}
}
