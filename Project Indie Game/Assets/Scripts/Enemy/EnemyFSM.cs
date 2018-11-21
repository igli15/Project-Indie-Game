using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour, IAgent {

    private Fsm<EnemyFSM> m_fsm;

    void Start() {
        m_fsm = new Fsm<EnemyFSM>(this);
        m_fsm.ChangeState<EnemySeekState>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.S)) m_fsm.ChangeState<EnemyMeleeState>();
    }

    public Fsm<EnemyFSM> fsm{ get{ return m_fsm; } }
}
