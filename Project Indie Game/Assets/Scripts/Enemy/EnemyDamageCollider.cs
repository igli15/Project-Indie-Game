using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollider : MonoBehaviour {

    public Action<Collider> OnEnemyTriggerStay;


    private void OnTriggerStay(Collider collider)
    {
        if (OnEnemyTriggerStay != null) OnEnemyTriggerStay(collider);
    }
}
