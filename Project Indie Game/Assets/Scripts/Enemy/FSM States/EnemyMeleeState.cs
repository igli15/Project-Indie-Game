using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMeleeState : AbstractState<EnemyFSM>
{

    //DELETE AFTER PROTYPE
    public PrototypeColorManager colorManager;
    //PLEASE

    private EnemyFSM m_enemyFSM;
    private EnemyMeleeAttack m_enemyMeleeAttack;
    private Rigidbody m_rigidbody;
    private NavMeshObstacle m_navMeshObstacle;

    void Awake()
    {
        Debug.Log("ENEMY MELEE START");

        m_enemyFSM = GetComponent<EnemyFSM>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_enemyMeleeAttack = GetComponent<EnemyMeleeAttack>();
        m_navMeshObstacle = GetComponent<NavMeshObstacle>();

        m_navMeshObstacle.enabled = false;
        m_enemyMeleeAttack.OnAttackEnds += OnAttackEnds;
    }

    private void OnAttackEnds(bool isPlayerDamaged)
    {
        if (isPlayerDamaged)
        {
            //Debug.Log("DO DAMAGE AGAIN");
            colorManager.ChangeColorTo(Color.yellow);
            m_enemyMeleeAttack.AttackPlayer();
            StartCoroutine(SetItRed(Color.yellow));
        }
        else
        {
            Debug.Log("Player Escaped");
            m_enemyFSM.fsm.ChangeState<EnemySeekState>();
            StartCoroutine(SetItRed(Color.green));
        }
        
    }

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);
       
        colorManager.ChangeColorTo(Color.yellow);
        m_navMeshObstacle.enabled = true;
        //FREEZING position of enemyObject
        //RigidbodyConstraints.FreezePositionX |  RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ |
        m_rigidbody.constraints = 
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY| RigidbodyConstraints.FreezeRotationZ;

        m_enemyMeleeAttack.AttackPlayer();
    }

    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
        m_navMeshObstacle.enabled = false;
        Debug.Log(" EXIT MELEE STATE");
        m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ; ;
        colorManager.ChangeColorTo(Color.green);
        StopAllCoroutines();
    }

    IEnumerator SetItRed(Color endColor)
    {
        colorManager.ChangeColorTo(Color.red);
        
        yield return new WaitForSeconds(0.5f);
        colorManager.ChangeColorTo(Color.green);
        yield return null;
    }

    void Update()
    {
    }
}
