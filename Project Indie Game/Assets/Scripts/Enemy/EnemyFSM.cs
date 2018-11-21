using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour,IAgent {

    private Fsm<EnemyFSM> m_fsm;

	void Start () {
        Debug.Log("EnemyFSM");
        m_fsm = new Fsm<EnemyFSM>(this);
        m_fsm.ChangeState<EnemySeekState>();
    }
	
	void Update () {
		
	}
}
