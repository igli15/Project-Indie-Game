using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private Enemy m_enemy;
    private NavMeshAgent m_navMeshAgent;

    private float m_initialSpeed;
    private bool m_isSlowDown = false;
	void Awake () {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_enemy = GetComponent<Enemy>();
        m_enemy.damageCollider.OnEnemyTriggerStay += OnObjectTriggerStay;
    }
	
	void Update () {

    }

    void OnObjectTriggerStay(Collider collider)
    {
        if (!m_isSlowDown&&collider.CompareTag("Enemy"))
        {
            SlowDown();
            m_isSlowDown = true;
        }
    }

    private void SlowDown()
    {
        Debug.Log("     SLOW_DOWN");
        m_navMeshAgent.velocity = Vector3.zero;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        m_navMeshAgent.speed = moveSpeed;
        m_initialSpeed = moveSpeed;
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
