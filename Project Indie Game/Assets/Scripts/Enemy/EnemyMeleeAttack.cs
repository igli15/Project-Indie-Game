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

	void Start () {
        m_enemyDamageCollider.OnEnemyTriggerStay += OnEnemyTriggerStay;
        m_lastTimeAttacked = Time.time;
    }
	
	void Update () {
		
	}


    public void OnEnemyTriggerStay(Collider collider)
    {
        if (m_lastTimeAttacked + m_reloadTime > Time.time) return;

        Health health = collider.GetComponent<Health>();

        if (health == null) { return; }
        Debug.Log("Deal Damage");
        m_lastTimeAttacked = Time.time;
        health.InflictDamage(m_damage);
        if (OnAttackEnds != null) OnAttackEnds();
    }


}
