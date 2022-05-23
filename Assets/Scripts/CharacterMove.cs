using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class CharacterMove : MonoBehaviour
{
	/// <summary>
	/// 移动速度
	/// </summary>
	public float moveSpeed = 1f;

	/// <summary>
	/// 内外墙体等可碰撞的物体层
	/// </summary>
	public LayerMask blockingLayer;

	public BoxCollider2D boxCollider2D;

	private Rigidbody2D _rigidbody2D;

	protected virtual void Start()
	{
		boxCollider2D = GetComponent<BoxCollider2D>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	/// <summary>
	/// 移动
	/// </summary>
	/// <param name="endPositon"></param>
	/// <returns></returns>
	protected IEnumerator OnMove( Vector3 endPositon )
	{
		Debug.Log( "_rigidbody2D.position" + _rigidbody2D.position );
		Debug.Log( "endPositon" + endPositon );
		Vector3 newPosition = Vector3.MoveTowards( _rigidbody2D.position, endPositon, moveSpeed );
		_rigidbody2D.MovePosition( newPosition );

		yield return null;
	}

	/// <summary>
	/// 移动前进行射线检测，被挡住就不能移动了，没有障碍物就进行计算开始移动
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="hit2D"></param>
	/// <returns>true 可以移动， false 不能移动</returns>
	protected bool OnMove( int x, int y, out RaycastHit2D hit2D )
	{
		Vector2 startPosition = transform.position;
		Vector2 endPositon = startPosition + new Vector2( x, y );

		boxCollider2D.enabled = false;
		hit2D = Physics2D.Linecast( startPosition, endPositon, blockingLayer );
		boxCollider2D.enabled = true;

		if( hit2D.transform == null )
		{
			StartCoroutine( OnMove( endPositon ) );

			return true;
		}

		return false;
	}

	/// <summary>
	/// 尝试移动时，检测碰撞到的物品的类型
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <typeparam name="T">撞到物品的类型</typeparam>
	protected virtual void TryMove<T>( int x, int y ) where T : Component
	{
		RaycastHit2D hit2D;
		bool canMove = OnMove( x, y, out hit2D );

		if( hit2D.transform == null )
		{
			return;
		}

		T hitComponent = hit2D.transform.GetComponent<T>();

		if( !canMove && hitComponent != null )
		{
			OnCantMove( hitComponent );
		}
	}

	protected abstract void OnCantMove<T>( T component ) where T : Component;
}
