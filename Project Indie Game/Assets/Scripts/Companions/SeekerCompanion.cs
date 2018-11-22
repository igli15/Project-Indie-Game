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
        m_canBounce = true;
	}

	// Update is called once per frame
	void Update () 
	{
		base.Update();
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

            if (m_canBounce)
            {
                other.GetComponent<Health>().InflictDamage(20);

                m_bounceAmount -= 1;
                m_canBounce = false;
            }

            //Debug.Log(enemiesInRange.Count == 0);
            if (enemiesInRange.Count == 0 || m_bounceAmount <=0)
			{
                //Debug.Log("WTF");
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
        else if(!other.CompareTag("Player") && IsThrown)
        {
            m_manager.DisableCompanion(this);
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
