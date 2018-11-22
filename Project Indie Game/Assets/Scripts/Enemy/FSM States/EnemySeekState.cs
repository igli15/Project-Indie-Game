using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemySeekState : AbstractState<EnemyFSM>
{
    private Enemy m_enemy;
    private EnemyFSM m_enemyFSM;
    private EnemyMovement m_enemyMovement;
    private EnemyMeleeAttack m_enemyMeleeAttack;

    private GameObject m_seekTarget;



    void Start()
    {
        m_enemy = GetComponent<Enemy>();
        m_enemyFSM = GetComponent<EnemyFSM>();
        m_enemyMovement = GetComponent<EnemyMovement>();
        m_enemyMeleeAttack = GetComponent<EnemyMeleeAttack>();

        m_enemy.damageCollider.OnEnemyTriggerEnter += OnPlayerEntersAttackZone;
    }


    private void OnPlayerEntersAttackZone(Collider collider)
    {
        if (collider.CompareTag("Player")) m_enemyFSM.fsm.ChangeState<EnemyMeleeState>();
    }

    public override void Enter(IAgent pAgent)
    {
        Debug.Log("ENTER SEEK STATE");
        base.Enter(pAgent);

        m_seekTarget = m_enemy.target;
        StartCoroutine( FollowTarget(m_seekTarget.transform) );
    }

    public override void Exit(IAgent pAgent)
    {
        Debug.Log("EXIT SEEK STATE");
        base.Exit(pAgent);

        m_enemyMovement.navMeshAgent.velocity /=2;
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        StopAllCoroutines();

        m_enemyMovement.ResetPath();

    }

    //EACH 0.1 seconds updating its destination if it reach previous
    private IEnumerator FollowTarget(Transform target)
    {
        Vector3 previousTargetPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity);

        while (true)
        {
            if (Vector3.SqrMagnitude(previousTargetPosition - target.position) > 0.1f)
            {
                m_enemyMovement.SetDestination(target.position);
                previousTargetPosition = target.position;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

}
