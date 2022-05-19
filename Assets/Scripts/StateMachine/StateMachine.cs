using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ʹ��״̬���Ķ���̳д˽ӿ�
/// </summary>
public interface IStateMachineOwner { };

public class StateMachine
{
	/// <summary>
	/// ��ǰ״̬
	/// </summary>
	public State CurrentStateType { get; private set; } = State.Default;

	/// <summary>
	/// ��ǰ��Ч�е�״̬����
	/// </summary>
	private StateBase _currentStateObject;

	/// <summary>
	/// Ӧ��״̬��������
	/// </summary>
	private IStateMachineOwner _owner;

	/// <summary>
	/// ���е�״̬�ֵ�
	/// </summary>
	private Dictionary<State, StateBase> _stateDictionary = new Dictionary<State, StateBase>();

	/// <summary>
	/// ��ʼ��
	/// </summary>
	/// <param name="owner"></param>
	public void Init( IStateMachineOwner owner )
	{
		this._owner = owner;
	}

	/// <summary>
	/// �л�״̬
	/// </summary>
	/// <typeparam name="T">����Ҫ�л���״̬��������</typeparam>
	/// <param name="newState">��״̬</param>
	/// <param name="reCurrentState">ˢ�µ�ǰ״̬</param>
	/// <returns></returns>
	public bool ChangeState<T>( State newStateType, bool reCurrentState = false ) where T : StateBase, new()
	{
		// ״̬һ�£���ˢ�£����л�
		if( newStateType == CurrentStateType && !reCurrentState )
		{
			return false;
		}

		// �˳���ǰ״̬
		if( _currentStateObject != null )
		{
			_currentStateObject.Exit();
		}

		// ������״̬
		_currentStateObject = GetState<T>( newStateType );
		_currentStateObject.Enter();

		return true;
	}

	/// <summary>
	/// ��ȡ״̬
	/// </summary>
	private StateBase GetState<T>( State stateType ) where T : StateBase, new()
	{
		// ��黺��������û�����״̬����ֱ�Ӵӻ���ȡ
		if( _stateDictionary.TryGetValue( stateType, out StateBase stateObject ) )
		{
			return stateObject;
		}

		// ���û�� new һ������ӵ�������
		StateBase state = new T();
		state.Init( _owner, this );
		_stateDictionary.Add( stateType, state );

		return state;
	}

	public void Update()
	{
		if(_currentStateObject != null )
		{
			_currentStateObject.Update();
		}
	}

	/// <summary>
	/// ֹͣ����
	/// </summary>
	public void Stop()
	{
		if(_currentStateObject != null )
		{
			_currentStateObject.Exit();
		}

		_currentStateObject = null;
		CurrentStateType = State.Default;

		_stateDictionary.Clear();
	}
}
