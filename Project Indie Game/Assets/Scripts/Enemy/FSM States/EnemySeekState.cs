﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemySeekState : AbstractState<EnemyFSM>
{
    private EnemyMovement m_enemyMovement;
    private EnemyMeleeAttack m_enemyMeleeAttack;
    private GameObject m_seekTarget;
    private EnemyFSM m_enemyFSM;


    void Awake()
    {
        m_enemyFSM = GetComponent<EnemyFSM>();
        m_enemyMovement = GetComponent<EnemyMovement>();
        m_enemyMeleeAttack = GetComponent<EnemyMeleeAttack>();
    }

    public override void Enter(IAgent pAgent)
    {
        Debug.Log("ENTER SEEK_STATE");
        base.Enter(pAgent);
        m_seekTarget = GetComponent<Enemy>().target;
        Debug.Log("     START FOLLOW_TARGET");
        m_enemyMovement.navMeshAgent.enabled = true;
        StartCoroutine( FollowTarget(m_seekTarget.transform) );


    }

    public override void Exit(IAgent pAgent)
    {
        Debug.Log("EXIT SEEK_STATE");
        base.Exit(pAgent);
        m_enemyMovement.ResetPath();
        m_enemyMovement.navMeshAgent.enabled = false;

        StopAllCoroutines();
    }

    private IEnumerator FollowTarget(Transform target)
    {
        Vector3 previousTargetPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity);

        while (true)
        {
            if ((transform.position - target.position).magnitude <2)
            {
                Debug.Log("END OF FOLLOWING");

                m_enemyFSM.fsm.ChangeState<EnemyMeleeState>();
                yield return null;
            }
            if (Vector3.SqrMagnitude(previousTargetPosition - target.position) > 0.1f)
            {
                m_enemyMovement.SetDestination(target.position);
                previousTargetPosition = target.position;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

}
