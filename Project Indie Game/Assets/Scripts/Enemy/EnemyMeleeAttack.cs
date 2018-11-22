using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField]
    private EnemyDamageCollider m_enemyDamageCollider;

    public Action OnAttackStart;
    public Action OnAttackEnds;

    private float m_damage = 1;
    private float m_reloadTime = 2f;
    private float m_lastTimeAttacked;

    void Start()
    {
        m_enemyDamageCollider.OnEnemyTriggerStay += OnEnemyTriggerStay;
        m_lastTimeAttacked = Time.time;
    }

    public void ResetWaitingTime()
    {
        m_lastTimeAttacked = Time.time;
    }

    void Update()
    {

    }


    public void OnEnemyTriggerStay(Collider collider)
    {
        if (m_lastTimeAttacked + m_reloadTime > Time.time) return;
        if (OnAttackEnds != null) OnAttackEnds();

        if (!collider.CompareTag("Player")) return;
        Health health = collider.GetComponent<Health>();
        m_lastTimeAttacked = Time.time;


        if (health != null) health.InflictDamage(m_damage);
    }

    public float reloadTime { get { return m_reloadTime; } set { m_reloadTime = value; } }

}

