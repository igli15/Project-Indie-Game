using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootState : AbstractState<EnemyFSM> {

    private EnemyRangedAttack m_rangedAttack;

    private void Start()
    {
        m_rangedAttack = GetComponent<EnemyRangedAttack>();

    }
    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);

    }

    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
    }

    void Update () {
        m_rangedAttack.ShootTo(transform.position);

    }
}
