using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class Companion : MonoBehaviour
{

	[SerializeField] 
	private Transform m_parentFeetPos;

	[SerializeField] 
	private Player m_playerScript;
	
	[SerializeField] 
	private float m_distRadius;

	[SerializeField]
	private float m_throwRange = 10;

	[SerializeField]
	private float m_throwSpeed = 20;

	private Vector3 m_randomPos;

	private NavMeshAgent m_navMeshAgent;

	private Rigidbody m_rb;

	private bool m_isThrown = false;

	private Vector3 m_throwPos = Vector3.zero;

	
	// Use this for initialization
	void Start ()
	{
		m_rb = GetComponent<Rigidbody>();
		m_navMeshAgent = GetComponent<NavMeshAgent>();
		FindRandomPositionAroundParent();
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
		
		if (Input.GetKeyDown(KeyCode.F))
		{
			m_navMeshAgent.ResetPath();
			Throw(m_parentFeetPos.forward,m_throwRange);
		}

	}


	public void FindRandomPositionAroundParent()
	{
		m_randomPos = Spawn();
		m_randomPos.y = m_parentFeetPos.position.y;
		m_navMeshAgent.SetDestination(m_parentFeetPos.position + m_randomPos);
	}

	public void ResetDestination()
	{
		m_navMeshAgent.ResetPath();
		m_navMeshAgent.SetDestination(m_parentFeetPos.position + m_randomPos);
	}

	public Vector3 Spawn()
	{
		Vector2 randomCircle = Random.insideUnitCircle.normalized * m_distRadius;
		m_randomPos = new Vector3(randomCircle.x,0,randomCircle.y);
		transform.position = randomCircle;
		return m_randomPos;
	}

	public void Throw(Vector3 dir,float range)
	{
		if (!m_isThrown)
		{
			m_throwPos = transform.position;
			m_rb.velocity = dir * m_throwSpeed;
			m_isThrown = true;
		}

	}

	public void CheckIfOutOfRange()
	{
		if (m_isThrown)
		{
			if (Vector3.Distance(m_throwPos, transform.position) >= m_throwRange)
			{
				Destroy(gameObject);
				m_isThrown = false;
			}
		}
	}

}
