using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum State
{
	Default,
	Idle,
	Move,
}
/// <summary>
/// ״̬����
/// </summary>
public abstract class StateBase
{
	protected StateMachine stateMachine;
	/// <summary>
	/// �ƶ��ٶ�
	/// </summary>
	public float moveSpeed = 1f;

	/// <summary>
	/// ����ǽ��ȿ���ײ�������
	/// </summary>
	public LayerMask blockingLayer;

	public BoxCollider2D boxCollider2D;

	private Rigidbody2D _rigidbody2D;

	public virtual void Initialize( IStateMachineOwner owner, StateMachine stateMachine )
	{
		this.stateMachine = stateMachine;
	}

	public virtual void Enter() { }

	public virtual void Update() { }

	public virtual void Exit() { }
}
