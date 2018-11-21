using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionSteering : MonoBehaviour 
{

	[SerializeField] 
	private Transform m_parentFeetPos;
	
	[SerializeField] 
	private float m_distRadius;
	
	[SerializeField] 
	private Player m_playerScript;
	
	private Vector3 m_randomPos;

	private NavMeshAgent m_navMeshAgent;

	private Rigidbody m_rb;
	
	
	// Use this for initialization
	void Awake () 
	{
		m_rb = GetComponent<Rigidbody>();
		m_navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void StopAgent()
	{
		if(m_navMeshAgent.gameObject.activeSelf)
		m_navMeshAgent.isStopped = true;
	}

	public void ResumeAgent()
	{
		m_navMeshAgent.isStopped = false;
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
		if (Vector3.Distance(transform.position, m_parentFeetPos.position) >
		    m_distRadius + m_navMeshAgent.stoppingDistance && m_playerScript.GetVelocity().magnitude > 0f)
		{
			m_navMeshAgent.ResetPath();
			m_navMeshAgent.SetDestination(m_parentFeetPos.position + m_randomPos);
		}
	}
	
	
	/*if (Vector3.Dot(m_parentFeetPos.forward, m_parentFeetPos.position - transform.position) >= 0)
			{
				m_navMeshAgent.SetDestination(m_parentFeetPos.position + m_randomPos);
			}
			else
			{
				m_navMeshAgent.SetDestination(m_parentFeetPos.position + m_randomPos * -1);
			}*/
}
