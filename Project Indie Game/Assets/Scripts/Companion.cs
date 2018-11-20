using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavMeshAgent))]
public class Companion : MonoBehaviour
{

	[SerializeField] 
	private Transform m_parentFeetPos;

	[SerializeField] 
	private float m_distRadius;

	[SerializeField] 
	private Player m_playerScript;

	private Vector3 m_randomPos;

	private NavMeshAgent m_navMeshAgent;
	
	// Use this for initialization
	void Start ()
	{	
		m_navMeshAgent = GetComponent<NavMeshAgent>();
		FindRandomPositionAroundParent();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Vector3.Distance(transform.position,m_parentFeetPos.position) > m_distRadius + m_navMeshAgent.stoppingDistance && m_playerScript.GetVelocity().magnitude > 0f)
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
	}


	void FindRandomPositionAroundParent()
	{
		Vector2 randomCircle = Random.insideUnitCircle.normalized * m_distRadius;
		
		m_randomPos = new Vector3(randomCircle.x,0,randomCircle.y);
		m_randomPos.y = m_parentFeetPos.position.y;
		m_navMeshAgent.SetDestination(m_parentFeetPos.position + m_randomPos);
	}

	void ResetDestination()
	{
		m_navMeshAgent.ResetPath();
		m_navMeshAgent.SetDestination(m_parentFeetPos.position + m_randomPos);
	}

}
