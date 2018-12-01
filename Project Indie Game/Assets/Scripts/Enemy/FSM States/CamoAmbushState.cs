﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamoAmbushState : AbstractState<EnemyFSM>
{

    private Camouflage m_enemy;
    private EnemyFSM m_enemyFSM;

    [SerializeField]
    private float m_timeTransformToAmbsuh = 1;
    [SerializeField]
    private float m_minTimeOfAmbush = 1;
    [SerializeField]
    private float m_timeTransformOutOfAmbush = 1;

    private bool m_isCollidedWithPlayer = false;
    private bool m_isUnderGround = false;

    private float m_startTimeOfAmbush;

    void Start()
    {
        m_enemyFSM = GetComponent<EnemyFSM>();
        m_enemy = GetComponent<Camouflage>();
        m_enemy.sphereCollider.OnEnemyTriggerStay += OnSphereTriggerStay;
    }

    void Update()
    {
        if (m_isUnderGround)
        {
            Debug.Log("TIME " + Time.time+" / "+(m_startTimeOfAmbush + m_minTimeOfAmbush) );
        }
    }

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);
        StopAllCoroutines();
        Debug.Log("ENTER AMBUSH STATE");
        StartCoroutine(TransformToAmbush());
    }

    IEnumerator TransformToAmbush()
    {
        Debug.Log("START Transform to ambush");
        yield return new WaitForSeconds(m_timeTransformToAmbsuh);
        m_startTimeOfAmbush = Time.time;
        m_isUnderGround = true;
        m_isCollidedWithPlayer = false;
        Debug.Log("END Transform to ambush: "+Time.time);
    }

    IEnumerator TransformOutOfAmbush(Vector3 destinationPos)
    {
        Debug.Log("START Transform OUT OF ambush: " + Time.time);
        Vector3 dir = destinationPos - transform.position;
        dir = new Vector3(dir.x, 0, dir.z);
        dir.Normalize();

        m_isUnderGround = false;

        yield return new WaitForSeconds(m_timeTransformOutOfAmbush);
        Debug.Log("END Transform OUT OF ambush");
        m_enemy.direction = dir;

        m_enemyFSM.fsm.ChangeState<CamoChargeState>();
        yield return null;
    }


    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
        Debug.Log("EXIT AMBUSH STATE");
        StopAllCoroutines();
    }

    void OnSphereTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player") && enabled && !m_isCollidedWithPlayer)
        {
            //Debug.Log("COLLIDED WITH PLAYER");
            if (m_startTimeOfAmbush + m_minTimeOfAmbush < Time.time)
            {
                m_isCollidedWithPlayer = true;
                Debug.Log("COLLIDED WITH " + collider.name + " /?/ " + Time.time);
                StartCoroutine(TransformOutOfAmbush(collider.transform.position));
            }
        }

    }

    private void OnDrawGizmos()
    {
        if(m_isUnderGround) Gizmos.color = Color.green;
        else Gizmos.color = Color.yellow;
        if (enabled)
            Gizmos.DrawWireSphere(transform.position + transform.up * 1.5f, 1f);
    }
}
