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

    public bool isPlayerInZone = false;
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
        //TOD: INCLUDE TWO STAGE ATTACK
        //TODO: WTF ATTACK AND STATE ARE THE SAME SCRIPT BUT IN 2 files FIX IT

        if (!collider.CompareTag("Player")&& !isPlayerInZone)
        {
            isPlayerInZone = true;
            if (OnAttackStart != null) OnAttackStart();
            m_lastTimeAttacked = Time.time;
        }

        if (m_lastTimeAttacked + m_reloadTime > Time.time) return;
        if (OnAttackEnds != null) OnAttackEnds();

        
        Health health = collider.GetComponent<Health>();
        m_lastTimeAttacked = Time.time;
        if (health != null) health.InflictDamage(m_damage);
    }

    public float reloadTime { get { return m_reloadTime; } set { m_reloadTime = value; } }

}

