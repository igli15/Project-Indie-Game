﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    public int waveIndex = -1;
    public int zoneIndex = -1;

    [Header("Stats")]
    [SerializeField]
    private float m_moveSpeed = 5;
    [SerializeField]
    private float m_reloadTime = 0.5f;
    [SerializeField]
    private float m_pushPower = 5;

    [Header("Other")]
    [SerializeField]
    private EnemyDamageCollider m_enemyDamageCollider;
    [SerializeField]
    private EnemyDamageCollider m_sphereCollider;

    private GameObject m_target;
    private EnemyMovement m_enemyMovement;
    private EnemyMeleeAttack m_meleeAttack;
    private EnemyRangedAttack m_enemyRangedAttack;

    private bool m_afterStart = false;

    public Action onEnemyDestroyed;

    void Awake() {
        m_target = GameObject.FindGameObjectWithTag("Player");
    }

    void AfterStart()
    {
        //NO STATES PLEASE!
        m_enemyMovement = GetComponent<EnemyMovement>();
        m_enemyMovement.SetMoveSpeed(m_moveSpeed);
        m_enemyMovement.SetPushPower(m_pushPower);
        m_meleeAttack = GetComponent<EnemyMeleeAttack>();
        if(m_meleeAttack!=null) m_meleeAttack.reloadTime = m_reloadTime;
        m_enemyRangedAttack = GetComponent<EnemyRangedAttack>();
        if (m_enemyRangedAttack != null) m_enemyRangedAttack.reloadTime = m_reloadTime;

        GetComponent<Health>().OnDeath += OnEnemyDestroyed;
    }

    void Update() {
        if (!m_afterStart)
        {
            AfterStart();
            m_afterStart = true;
        }
    }

    public void OnEnemyDestroyed(Health health)
    {
        if (onEnemyDestroyed != null) onEnemyDestroyed();

        string tag="";
        switch (GetComponent<EnemyFSM>().enemyTpe)
        {
            case EnemyFSM.EnemyType.GOOMBA:
                tag = "Goomba";
                break;
            case EnemyFSM.EnemyType.TURRET:
                tag = "Turret";
                break;
        }
        ObjectPooler.instance.DestroyFromPool(tag,gameObject);
    }

    public GameObject target{ get { return m_target; } }
    public EnemyDamageCollider damageCollider { get { return m_enemyDamageCollider; } }
    public EnemyDamageCollider sphereCollider { get { return m_sphereCollider; } }
}
