using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushHitbox : MonoBehaviour
{

	private RushCompanion m_parentCompanion;
	
	// Use this for initialization
	void Start ()
	{
		m_parentCompanion = transform.parent.GetComponent<RushCompanion>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			m_parentCompanion.CatchEnemy(other.gameObject);
		}
		else if (other.CompareTag("Obstacle"))
		{
			m_parentCompanion.Activate();
		}
	}
}
