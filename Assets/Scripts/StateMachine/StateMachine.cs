using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有使用状态机的对象继承此接口
/// </summary>
public interface IStateMachineOwner { };

public class StateMachine
{
	/// <summary>
	/// 当前状态
	/// </summary>
	public State CurrentStateType { get; private set; } = State.Default;

	/// <summary>
	/// 当前生效中的状态对象
	/// </summary>
	private StateBase _currentStateObject;

	/// <summary>
	/// 应用状态机的宿主
	/// </summary>
	private IStateMachineOwner _owner;

	/// <summary>
	/// 所有的状态字典
	/// </summary>
	private Dictionary<State, StateBase> _states = new Dictionary<State, StateBase>();

	/// <summary>
	/// 初始化
	/// </summary>
	/// <param name="owner"></param>
	public void Initialize( IStateMachineOwner owner )
	{
		this._owner = owner;
	}

	/// <summary>
	/// 注册状态
	/// </summary>
	/// <param name="State"></param>
	/// <param name="stateBase"></param>
	public void ReginState( State State, StateBase stateBase ) { }


	/// <summary>
	/// 切换状态
	/// </summary>
	/// <typeparam name="T">具体要切换的状态对象类型</typeparam>
	/// <param name="newState">新状态</param>
	/// <param name="refreshCurrentState">刷新当前状态</param>
	/// <returns></returns>
	public bool ChangeState<T>( State newStateType, bool refreshCurrentState = false ) where T : StateBase, new()
	{
		// 状态一致，不刷新，不切换
		if( newStateType == CurrentStateType && !refreshCurrentState )
		{
			return false;
		}

		// 退出当前状态
		if( _currentStateObject != null )
		{
			_currentStateObject.Exit();
		}

		// 进入新状态
		_currentStateObject = GetState<T>( newStateType );
		_currentStateObject.Enter();

		return true;
	}

	/// <summary>
	/// 获取状态
	/// </summary>
	private StateBase GetState<T>( State stateType ) where T : StateBase, new()
	{
		// 检查缓存里面有没有这个状态，有直接从缓存取
		if( _states.TryGetValue( stateType, out StateBase stateObject ) )
		{
			return stateObject;
		}

		// 如果没有 new 一个，添加到缓存中
		StateBase state = new T();
		state.Init( _owner, this );
		_states.Add( stateType, state );

		return state;
	}

	public void Update()
	{
		if( _currentStateObject != null )
		{
			_currentStateObject.Update();
		}
	}

	/// <summary>
	/// 停止工作
	/// </summary>
	public void Stop()
	{
		if( _currentStateObject != null )
		{
			_currentStateObject.Exit();
		}

		_currentStateObject = null;
		CurrentStateType = State.Default;

		_states.Clear();
	}
}
