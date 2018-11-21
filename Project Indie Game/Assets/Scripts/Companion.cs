using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class Companion : MonoBehaviour,ICompanion
{

	[SerializeField] 
	private Transform m_parentFeetPos;

	[SerializeField] 
	private CompanionManager m_manager; 

	[SerializeField] 
	private Player m_playerScript;
	
	[SerializeField] 
	private float m_distRadius;

	[SerializeField]
	private float m_throwRange = 10;

	[SerializeField]
	private float m_throwSpeed = 20;

	[SerializeField] 
	private float m_respawnTime = 3;

	private Vector3 m_randomPos;

	private NavMeshAgent m_navMeshAgent;

	private Rigidbody m_rb;

	private bool m_isThrown = false;

	private Vector3 m_throwPos = Vector3.zero;

	
	// Use this for initialization
	void Awake ()
	{
		m_rb = GetComponent<Rigidbody>();
		m_navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!m_isThrown && Vector3.Distance(transform.position,m_parentFeetPos.position) > m_distRadius + m_navMeshAgent.stoppingDistance && m_playerScript.GetVelocity().magnitude > 0f)
		{
			ResetDestination();
			/*if (Vector3.Dot(m_parentFeetPos.forward, m_parentFeetPos.position - transform.position) >= 0)
			{
				m_navMeshAgent.SetDestination(m_parentFeetPos.position + m_randomPos);
			}
			else
			{
				m_navMeshAgent.SetDestination(m_parentFeetPos.position + m_randomPos * -1);
			}*/
		}

		CheckIfOutOfRange();
		
		
		//For testing Only
		if (Input.GetKeyDown(KeyCode.F))
		{
			m_navMeshAgent.ResetPath();
			Throw();
		}

	}


	public void FindRandomPositionAroundParent()
	{
		Vector2 randomCircle = Random.insideUnitCircle.normalized * m_distRadius;
		m_randomPos = new Vector3(randomCircle.x,0,randomCircle.y);
		transform.position = m_randomPos + m_parentFeetPos.position;
		
		m_randomPos.y = m_parentFeetPos.position.y;
		m_navMeshAgent.SetDestination(m_parentFeetPos.position + m_randomPos);
	}

	public void ResetDestination()
	{
		m_navMeshAgent.ResetPath();
		m_navMeshAgent.SetDestination(m_parentFeetPos.position + m_randomPos);
	}

	public void Throw()
	{
		if (!m_isThrown)
		{
			m_throwPos = transform.position;
			m_rb.velocity = transform.forward * m_throwSpeed;   //CHANGE TRANSFORM FORWARD TO MOUSE POINT
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
		FindRandomPositionAroundParent();
	}

}
