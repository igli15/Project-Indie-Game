using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACompanion : MonoBehaviour 

{
	protected int m_index;
	protected bool m_isThrown = false;
	
	public Action<ACompanion> OnSpawn;
	public Action<ACompanion> OnDisable;
	public Action<ACompanion> OnActivate;
	public Action<ACompanion> OnThrow;
	public Action<ACompanion> OnSelected;
	public Action<ACompanion> OnDeSelected;
	public Action<ACompanion> OnRangeReached;
	
	public abstract void Throw(Vector3 dir);
	public abstract void Activate(GameObject other = null);  //some time the activation requires other objects that companion collides with.
	public abstract void CheckIfOutOfRange();
	public abstract void Reset();
	public abstract void Spawn();
	
	
	public void SetIndex(int index)
	{
		m_index = index;
	}

	public int GetIndex()
	{
		return m_index;
	}

	public bool IsThrown
	{
		get { return m_isThrown; }
	}
}
