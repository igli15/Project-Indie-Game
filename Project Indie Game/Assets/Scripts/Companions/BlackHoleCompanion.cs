using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleCompanion : Companion
{
	[SerializeField] 
	private GameObject m_blackHole;

	[SerializeField] 
	[Range(0.1f,2)]
	private float m_pullForce = 1;

	[SerializeField] 
	private float m_destroyBlackholeTimer = 2;
	
	private bool m_instantiatedBlackHole;
	
	private void Awake()
	{
		base.Awake();
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.Update();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Obstacle") || other.transform.CompareTag("Enemy"))
		{
			if(m_isThrown)
			Activate();
		}
	}

	public override void RangeReached()
	{
		base.RangeReached();
		
		if(m_isThrown)
		Activate();
	}

	public override void Reset()
	{
		base.Reset();
		m_instantiatedBlackHole = false;
	}

	public override void Activate(GameObject other = null)
	{
		base.Activate(other);

		if (m_instantiatedBlackHole == false)
		{
			GameObject blackHole = Instantiate(m_blackHole);
			Destroy(blackHole,m_destroyBlackholeTimer);
			blackHole.transform.position = transform.position;
			blackHole.transform.rotation = Quaternion.identity;
			blackHole.GetComponent<BlackHole>().PullForce = m_pullForce;
			m_instantiatedBlackHole = true;
		}

	}
}
