using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour {

    [SerializeField]
    private GameObject m_projectile;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShootTo(Vector3 targetPosition)
    {
        Debug.Log("PewPew");
        Vector3 direction=targetPosition- transform.position;
        direction.Normalize();
        Debug.Log(direction + "D");
        GameObject newProjectile=Instantiate(m_projectile, transform.position+transform.up, transform.rotation, null);
        Rigidbody rb =newProjectile.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.velocity = direction * 20;


    }
}
