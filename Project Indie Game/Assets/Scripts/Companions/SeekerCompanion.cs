using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeekerCompanion : Companion
{

	[SerializeField]
	private float m_seekRange = 3;

	[SerializeField] 
	private float m_bounceAmount = 4;
	
	private Collider m_collider;

	private float m_initbounceAmount = 0;
	
	private void Awake()
	{
		base.Awake();
		
		m_collider = GetComponent<Collider>();
	}

	// Use this for initialization
	void Start ()
	{
		m_initbounceAmount = m_bounceAmount;
	}

	public override void Throw(Vector3 dir)
	{
		base.Throw(dir);
		m_collider.enabled = true;
		m_throwPos = transform.position;
		m_rb.velocity = dir * m_throwSpeed; 
	}

	// Update is called once per frame
	void Update () 
	{
		base.Update();
	}

	private void OnTriggerEnter(Collider other)
	{
		Activate();

		if (other.gameObject.CompareTag("Enemy"))
		{
			Collider[] inRangeColliders = Physics.OverlapSphere(transform.position, m_seekRange);

			List<GameObject> enemiesInRange =  new List<GameObject>();

			for (int i = 0; i < inRangeColliders.Length; i++)
			{
				if (inRangeColliders[i].gameObject.CompareTag("Enemy") && inRangeColliders[i].transform != other.transform)
				{
					enemiesInRange.Add(inRangeColliders[i].gameObject);
				}
			}

			other.GetComponent<Health>().InflictDamage(20);
			
			m_bounceAmount -= 1;
			
			
			if (enemiesInRange.Count == 0 || m_bounceAmount <= 0)
			{
				m_manager.DisableCompanion(this);
			}
			
			else
			{
				Vector3 targetDir = Vector3.forward * m_seekRange * 2; 
				for (int i = 0; i < enemiesInRange.Count; i++)
				{
					if (enemiesInRange[i].transform != other.transform)
					{
						if (targetDir.magnitude >=
						    (enemiesInRange[i].transform.position - other.transform.position).magnitude)
						{
							targetDir = enemiesInRange[i].transform.position - other.transform.position;
						}
					}
				}
				Throw(targetDir.normalized);
			}
			
		}
	}

	public override void Reset()
	{
		base.Reset();
		m_bounceAmount = m_initbounceAmount;
	}

	public override void Activate()
	{
		base.Activate();
	}
}
