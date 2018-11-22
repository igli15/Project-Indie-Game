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

    private bool m_canBounce = false;

	private Transform m_targetTransform;
	
	private void Awake()
	{
		base.Awake();
		m_initbounceAmount = m_bounceAmount;
		m_collider = GetComponent<Collider>();
	}

	// Use this for initialization


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

		if (m_targetTransform != null && m_isThrown)
		{
			m_rb.velocity = (m_targetTransform.position - transform.position).normalized * m_throwSpeed;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		Activate();

        if (other.gameObject.CompareTag("Enemy") && IsThrown)
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
            // Debug.Log(m_bounceAmount);

                other.GetComponent<Health>().InflictDamage(20);

                m_bounceAmount -= 1;

            //Debug.Log(enemiesInRange.Count == 0);
            if (enemiesInRange.Count == 0 || m_bounceAmount <=0)
			{
                //Debug.Log("WTF");
				m_manager.DisableCompanion(this);
			}
			
			else
			{
				Vector3 targetDir = Vector3.forward * m_seekRange * 2;
				Transform targetTransform = null;
				for (int i = 0; i < enemiesInRange.Count; i++)
				{
					if (enemiesInRange[i].transform != other.transform)
					{
						if (targetDir.magnitude >=
						    (enemiesInRange[i].transform.position - other.transform.position).magnitude)
						{
							targetDir = enemiesInRange[i].transform.position - other.transform.position;
							targetTransform = enemiesInRange[i].transform;
						}
					}
				}

				m_targetTransform = targetTransform;
				//Throw(targetDir.normalized);
			}			
		}
        else if(!other.CompareTag("Companion") && !other.CompareTag("Player") && !other.CompareTag("IgnoreCollision")  && IsThrown)
        {
            m_manager.DisableCompanion(this);
        }
	}

	public override void Reset()
	{
		base.Reset();
		m_bounceAmount = m_initbounceAmount;
		m_targetTransform = null;
	}

	public override void Activate()
	{
		base.Activate();
	}
}
