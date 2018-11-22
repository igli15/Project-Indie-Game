using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour, IAgent {

    static int countID=0;
    public int ID;

    private Fsm<EnemyFSM> m_fsm;

    void Start() {
        ID = countID;
        countID++;
        m_fsm = new Fsm<EnemyFSM>(this);
        m_fsm.ChangeState<EnemySeekState>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.S)) m_fsm.ChangeState<EnemyMeleeState>();
    }

    public Fsm<EnemyFSM> fsm{ get{ return m_fsm; } }

    private void OnDrawGizmos()
    {
        if (fsm == null) return;
        if (fsm.GetCurrentState() is EnemySeekState) Gizmos.color = Color.cyan;
        if (fsm.GetCurrentState() is EnemyMeleeState) Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position + transform.up * 2, 0.4f);
    }
}
