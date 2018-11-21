using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeState : AbstractState<EnemyFSM> {

    //DELETE AFTER PROTYPE
    public PrototypeColorManager colorManager;
    //PLEASE

    private EnemyFSM m_enemyFSM;
    private EnemyMeleeAttack m_enemyMeleeAttack;
    private Rigidbody m_rigidbody;

	void Start () {
        m_enemyFSM = GetComponent<EnemyFSM>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_enemyMeleeAttack = GetComponent<EnemyMeleeAttack>();
        m_enemyMeleeAttack.OnAttackEnds += OnAttackEnds;
        m_enemyMeleeAttack.enabled = false;
    }

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);

        m_enemyMeleeAttack.enabled = true;
        colorManager.ChangeColorTo(Color.yellow);
        
        //FREEZING position of enemyObject
        m_rigidbody.constraints = RigidbodyConstraints.FreezePositionX |
            RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }

    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
        colorManager.ChangeColorTo(Color.green);
        StopAllCoroutines();
        m_enemyMeleeAttack.enabled = false;
    }

    void OnAttackEnds()
    {
        StartCoroutine(DealDamage());
    }

    IEnumerator DealDamage()
    {
        colorManager.ChangeColorTo(Color.red);

        yield return new WaitForSeconds(0.5f);

        m_enemyFSM.fsm.ChangeState<EnemySeekState>();
        yield return null;
    }

    void Update () {
    }
}
