using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACompanion : MonoBehaviour 

{
	[SerializeField]
	protected float m_chargeTime = 0;

	protected bool m_isCharged = false;
	
	protected int m_index;
	
	protected bool m_isThrown = false;
	
	public Action<ACompanion> OnSpawn;
	public Action<ACompanion> OnDisable;
	public Action<ACompanion> OnActivate;
	public Action<ACompanion> OnThrow;
	public Action<ACompanion> OnSelected;
	public Action<ACompanion> OnDeSelected;
	public Action<ACompanion> OnRangeReached;
	public Action<ACompanion> OnStartCharging;
	public Action<ACompanion> OnCharging;
	public Action<ACompanion> OnChargeFinished;
	
	public abstract void Throw(Vector3 dir);
	public abstract void Activate(GameObject other = null);  //some time the activation requires other objects that companion collides with.
	public abstract void CheckIfOutOfRange();
	public abstract void Reset();
	public abstract void Spawn();
	
	
	public int Index
	{
		get { return m_index; }
		set { m_index = value; }
	}

	public bool IsThrown
	{
		get { return m_isThrown; }
	}

	public float ChargeTime
	{
		get { return m_chargeTime; }
	}

	public bool IsCharged
	{
		get { return m_isCharged; }
		set { m_isCharged = value; }
	}
}
