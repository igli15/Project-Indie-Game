using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamoAmbushState : AbstractState<EnemyFSM> {

    private Camouflage m_enemy;
    private EnemyFSM m_enemyFSM;

    [SerializeField]
    private float m_timeTransformToAmbsuh = 1;
    [SerializeField]
    private float m_timeTransformOutOfAmbush = 1;

    private bool m_isCollidedWithPlayer = false;
    void Start () {
        m_enemyFSM = GetComponent<EnemyFSM>();
        m_enemy = GetComponent<Camouflage>();
        m_enemy.sphereCollider.OnEnemyTriggerStay += OnSphereTriggerStay;
    }

    void Update()
    {

    }

    IEnumerator TransformToAmbush()
    {
        yield return new WaitForSeconds(m_timeTransformToAmbsuh);
    }

    IEnumerator TransformOutOfAmbush(Vector3 destinationPos)
    {
        Debug.Log("BANZAI !!!!!!!!!!!!!!!!!!");
        Vector3 dir = destinationPos - transform.position;
        dir = new Vector3(dir.x, 0, dir.z);
        dir.Normalize();

        yield return new WaitForSeconds(m_timeTransformOutOfAmbush);
        Debug.Log("CHARGE ==================");
        m_enemy.direction = dir;
        m_enemyFSM.fsm.ChangeState<CamoChargeState>();
        yield return null;
    }

    public override void Enter(IAgent pAgent)
    {

        base.Enter(pAgent);
        m_isCollidedWithPlayer = false;
        Debug.Log("ENTER AMBUSH STATE");

    }
    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
        Debug.Log("EXIT AMBUSH STATE");
        StopAllCoroutines();
    }

    void OnSphereTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player")&&enabled&& ! m_isCollidedWithPlayer)
        {
            m_isCollidedWithPlayer = true;
            Debug.Log("COLLIDED WITH " + collider.name + " /?/ " + collider.CompareTag("Player"));
            StartCoroutine(TransformOutOfAmbush(collider.transform.position));
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(enabled)
        Gizmos.DrawWireSphere(transform.position + transform.up * 1.5f, 1f);
    }
}
