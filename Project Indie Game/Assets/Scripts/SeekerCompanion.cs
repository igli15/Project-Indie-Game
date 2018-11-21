using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerCompanion : Companion
{
	private void Awake()
	{
		base.Awake();
	}

	// Use this for initialization
	void Start () 
	{
		
	}

	public override void Throw(Vector3 dir)
	{
		base.Throw(dir);
		m_throwPos = transform.position;
		m_rb.velocity = dir * m_throwSpeed; 
	}

	// Update is called once per frame
	void Update () 
	{
		base.Update();
	}
}
