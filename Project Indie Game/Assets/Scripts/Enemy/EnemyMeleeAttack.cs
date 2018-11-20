using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMeleeAttack : MonoBehaviour
{
    private Rigidbody m_rigidbody;

    private float m_damage = 1;
    private float m_reloadTime = 0.5f;
    private float m_lastTimeAttacked;

	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
        m_lastTimeAttacked = Time.time;
    }
	
	void Update () {
		
	}


    public void OnCollisionStay(Collision collision)
    {
        if (m_lastTimeAttacked + m_reloadTime > Time.time) return;

        Health health = collision.collider.GetComponent<Health>();

        if (health == null) { return; }

        m_lastTimeAttacked = Time.time;
        health.InflictDamage(m_damage);

    }
}
