using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private Enemy m_enemy;
    private NavMeshAgent m_navMeshAgent;

    private float m_pushPower = 0;

    private float m_initialSpeed;
    private bool m_isPushed = false;
    public bool pushIsEnabled = true;
	void Awake () {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_enemy = GetComponent<Enemy>();
        m_enemy.sphereCOllider.OnEnemyTriggerEnter += OnObjectTriggerStay;
    }
	
	void Update () {

    }

    void OnObjectTriggerStay(Collider collider)
    {
        if (!pushIsEnabled) return;
        if (collider.CompareTag("Enemy"))
        {
            if (!IsBehindMe(collider.transform.position)) return;
            print("The other transform is behind me!  " + collider.name);
            Vector3 direction = collider.transform.position- transform.position;
            direction.Normalize();
            collider.GetComponent<NavMeshAgent>().velocity += (direction + transform.right )* m_pushPower;
        }
    }

    IEnumerator DisablePushingFor(float seconds)
    {
        m_isPushed = true;
        yield return new WaitForSeconds(seconds);
        m_isPushed = false;
        yield return null;
    }

    bool IsBehindMe(Vector3 target)
    {

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = target- transform.position;

        return (Vector3.Dot(forward, toOther) < 0);
    }

    private void SlowDown()
    {
        if (m_isPushed) return;
        StartCoroutine(DisablePushingFor(1));

        Debug.Log("     SLOW_DOWN");
        m_navMeshAgent.velocity = Vector3.zero;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    public void SetPushPower(float pushPower)
    {
        m_pushPower = pushPower;
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
