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
        GameObject newProjectile=Instantiate(m_projectile, transform.position, transform.rotation, null);
        newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward);
    }
}
