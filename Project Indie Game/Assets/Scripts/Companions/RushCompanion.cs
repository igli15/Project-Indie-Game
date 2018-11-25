using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushCompanion : Companion {

	// Use this for initialization

	[Header("Rush Companion Values")]

	[SerializeField] 
	private GameObject m_rushHitbox;
	
	private Collider m_collider;

	private void Awake()
	{
		base.Awake();
	}

	public override void Throw(Vector3 dir)
	{
		base.Throw(dir);
		m_collider.enabled = true;
	}

	private void Reset()
	{
		base.Reset();
		m_collider.enabled = false;
	}

	void Start ()
	{
		m_collider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.Update();
	}
}
