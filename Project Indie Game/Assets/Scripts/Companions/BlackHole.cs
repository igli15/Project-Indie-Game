using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
	private float m_pullForce ;

	private Dictionary<Transform, Vector3> m_enemiesInRangeDictionary ;
	// Use this for initialization
	void Start () 
	{
		m_enemiesInRangeDictionary = new Dictionary<Transform, Vector3>();
	}
	

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Enemy"))
		{
			m_enemiesInRangeDictionary.Add(other.transform,(transform.position - other.transform.position).normalized);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.transform.CompareTag("Enemy"))
		{
			if (Vector3.Distance(transform.position, other.transform.position) <= 0.9f)
			{
				other.GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
			else
			{
				other.GetComponent<Rigidbody>().velocity = (m_enemiesInRangeDictionary[other.transform] * m_pullForce);
			}
		}
	}

	public float PullForce
	{
		get { return m_pullForce; }
		set { m_pullForce = value; }
	}
}
