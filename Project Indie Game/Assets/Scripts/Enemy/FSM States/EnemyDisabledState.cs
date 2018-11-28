using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDisabledState : AbstractState<EnemyFSM> {

    private NavMeshAgent m_navMeshAgent;
    private EnemyFSM m_enemyFSM;

    void Start () {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_enemyFSM = GetComponent<EnemyFSM>();
	}

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);
        m_navMeshAgent.ResetPath();
        m_navMeshAgent.velocity = Vector3.zero;
        m_navMeshAgent.enabled = false;

    }
    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
        m_navMeshAgent.enabled = true;
    }


    void Update () {
		
	}
}
