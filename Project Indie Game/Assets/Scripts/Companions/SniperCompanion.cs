using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SniperCompanion : Companion
{
	private GameObject m_targetEnemy;

	private List<GameObject> m_enemiesInLine;
	
	
	
	private void Awake()
	{
		base.Awake();
	}
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	void Update () 
	{
		base.Update();
	}
	
	public override void Activate(GameObject other = null)
	{
		if (OnActivate != null) OnActivate(this);
	}

	public override void CheckIfOutOfRange()
	{
		base.CheckIfOutOfRange();
	}

	public override void Reset()
	{
		base.Reset();
	}

	public override void Throw(Vector3 dir)
	{
		base.Throw(dir);
		RaycastHit[] hits;
		
		hits = Physics.RaycastAll(transform.position, dir,m_throwRange);

		for (int i = 0; i < hits.Length; i++)
		{
			if (hits[i].transform.CompareTag("Obstacle"))
			{
				break;
			}
			if (hits[i].transform.CompareTag("Enemy"))
			{
				//m_enemiesInLine.Add(hits[i].transform.gameObject);
				m_targetEnemy = hits[i].transform.gameObject;
			}
		}
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Enemy"))
		{
			if (m_targetEnemy != null && other.transform == m_targetEnemy.transform)
			{
				other.GetComponent<Health>().InflictDamage(100);
			}
			else
			{
				other.GetComponent<Health>().InflictDamage(50);
			}
		}
		
	}
}
