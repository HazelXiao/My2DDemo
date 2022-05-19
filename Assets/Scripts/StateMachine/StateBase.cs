using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ×´Ì¬»ùÀà
/// </summary>
public abstract class StateBase
{
	protected StateMachine stateMachine;

	public virtual void Init( IStateMachineOwner owner, StateMachine stateMachine )
	{
		this.stateMachine = stateMachine;
	}

	public virtual void Enter() { }

	public virtual void Update() { }

	public virtual void Exit() { }
}
