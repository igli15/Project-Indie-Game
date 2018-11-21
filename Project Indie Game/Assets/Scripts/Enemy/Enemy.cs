using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float m_moveSpeed = 5;

    private GameObject m_target;
    private EnemyMovement m_enemyMovement;

    private bool m_afterStart = false;

    void Awake() {
        m_target = GameObject.FindGameObjectWithTag("Player");
    }

    void AfterStart()
    {

        m_enemyMovement = GetComponent<EnemyMovement>();
        m_enemyMovement.SetMoveSpeed(m_moveSpeed);
        //m_enemyMovement.SetDestination(m_target.transform.position);
    }

    void Update() {
        if (!m_afterStart)
        {
            AfterStart();
            m_afterStart = true;
        }
    }

    public GameObject target{ get { return m_target; } }
}
