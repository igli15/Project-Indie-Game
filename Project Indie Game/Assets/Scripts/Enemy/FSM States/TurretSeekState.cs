using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSeekState : EnemySeekState {

    private Enemy m_enemy;
    private EnemyFSM m_enemyFSM;
    private EnemyMovement m_enemyMovement;

    private GameObject m_seekTarget;


    void Awake()
    {
        m_enemy = GetComponent<Enemy>();
        m_enemyFSM = GetComponent<EnemyFSM>();
        m_enemyMovement = GetComponent<EnemyMovement>();

        m_seekTarget = m_enemy.target;
        
    }


    void Update () {
		
	}
}
