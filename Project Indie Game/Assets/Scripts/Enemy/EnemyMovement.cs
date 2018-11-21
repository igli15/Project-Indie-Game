using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{

    private NavMeshAgent m_navMeshAgent;

	void Awake () {
        m_navMeshAgent = GetComponent<NavMeshAgent>();

    }
	
	void Update () {

    }

    public void SetMoveSpeed(float moveSpeed)
    {
        m_navMeshAgent.speed = moveSpeed;
    }
    public void SetDestination(Vector3 destination)
    {
        m_navMeshAgent.SetDestination(destination);
    }
    public void ResetPath()
    {
        m_navMeshAgent.ResetPath();
    }

    public NavMeshAgent navMeshAgent { get { return m_navMeshAgent; } }
}
