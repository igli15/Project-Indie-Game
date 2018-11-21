using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaSeekState : EnemySeekState {

    [SerializeField]
    private BoxCollider m_damageCollider;

    public GoombaSeekState()
    {

    }

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);

    }

    public override void Exit(IAgent pAgent)
    {
        base.Enter(pAgent);

    }
}
