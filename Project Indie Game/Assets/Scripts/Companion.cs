using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CompanionSteering))]
public class Companion : MonoBehaviour,ICompanion
{
	[SerializeField] 
	private CompanionManager m_manager; 

	[SerializeField]
	private float m_throwRange = 10;

	[SerializeField]
	private float m_throwSpeed = 20;

	private CompanionSteering m_steering;

	private Rigidbody m_rb;

	private bool m_isThrown = false;

	private Vector3 m_throwPos = Vector3.zero;

	
	// Use this for initialization
	void Awake ()
	{
		m_rb = GetComponent<Rigidbody>();
		m_steering = GetComponent<CompanionSteering>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!m_isThrown)
		{
			m_steering.ResetDestination();
		}

		CheckIfOutOfRange();
		
		
		//For testing Only
		if (Input.GetKeyDown(KeyCode.F))
		{
			Throw();
		}

	}

	public void Throw()
	{
		if (!m_isThrown)
		{
			m_throwPos = transform.position;
			m_rb.velocity = m_manager.gameObject.transform.forward * m_throwSpeed;   //CHANGE TRANSFORM FORWARD TO MOUSE POINT
			m_isThrown = true;
		}

	}

	public void Activate()
	{
		Debug.Log("Activate");
	}

	public void CheckIfOutOfRange()
	{
		if (m_isThrown)
		{
			if (Vector3.Distance(m_throwPos, transform.position) >= m_throwRange)
			{
				m_manager.DisableCompanion(this);
				
				//maybe put on activate events here.....
			}
		}
	}

	public void Reset()
	{
		m_isThrown = false;
		
		if(m_rb!= null)
		m_rb.velocity = Vector3.zero;
	}



	public void Respawn()
	{
		Reset();
		m_steering.FindRandomPositionAroundParent();
	}

}
