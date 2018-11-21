using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeState : AbstractState<EnemyFSM> {

    private EnemyMeleeAttack m_enemyMeleeAttack;

	void Start () {
        m_enemyMeleeAttack = GetComponent<EnemyMeleeAttack>();
        m_enemyMeleeAttack.enabled = false;
    }

    public override void Enter(IAgent pAgent)
    {
        Debug.Log("ENTER MELEE_STATE");
        base.Enter(pAgent);
        m_enemyMeleeAttack.enabled = true;
    }

    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
    }

    void Update () {
		
	}
}
