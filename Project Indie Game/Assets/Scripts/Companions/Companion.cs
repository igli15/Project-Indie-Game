using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CompanionSteering))]
public class Companion : ACompanion
{
	[SerializeField] 
	protected string m_layerToIgnoreName = "CompanionIgnore";
	[SerializeField] 
	protected CompanionManager m_manager; 
	[SerializeField]
	protected float m_throwRange = 10;

	[SerializeField]
	protected float m_throwSpeed = 20;

	protected CompanionSteering m_steering;

	protected Rigidbody m_rb;

	protected Vector3 m_throwPos = Vector3.zero;


	
	// Use this for initialization
	protected void Awake ()
	{
		m_isThrown = false;
		
		OnSelected += delegate(ACompanion companion)	{companion.GetComponent<Renderer>().material.color = Color.red;};
		OnDeSelected +=  delegate(ACompanion companion) {companion.GetComponent<Renderer>().material.color = Color.white; };
		
		m_rb = GetComponent<Rigidbody>();
		m_steering = GetComponent<CompanionSteering>();
	}
	
	// Update is called once per frame
	protected void Update ()
	{
		if (!m_isThrown)
		{
			m_steering.ResetDestination();
		}

		CheckIfOutOfRange();
		

	}

	public override void Throw(Vector3 dir)
	{
		if (OnThrow != null) OnThrow(this);
			
		m_isCharged = false;
		m_manager.SelectCompanion(m_index + 1);
		m_steering.StopAgent();
		m_isThrown = true;
		
		m_throwPos = transform.position;
		m_rb.velocity = dir * m_throwSpeed;
		m_rb.rotation = Quaternion.LookRotation(m_rb.velocity.normalized);
	}

	public override void Activate(GameObject other = null)
	{
		if (OnActivate != null) OnActivate(this);
	}

	public override void CheckIfOutOfRange()
	{
		if (m_isThrown)
		{
			//Debug.Log(this);
			if (Vector3.Distance(m_throwPos, transform.position) >= m_throwRange)
			{
				m_manager.DisableCompanion(this);
				if(OnRangeReached != null) OnRangeReached(this);
			}
		}
	}

	public override void Reset()
	{
		m_isThrown = false;
		m_steering.ResumeAgent();
		if(m_rb!= null)
		m_rb.velocity = Vector3.zero;
	}



	public override void Spawn()
	{
		if (OnSpawn != null) OnSpawn(this);
		Reset();
		m_steering.FindRandomPositionAroundParent();
	}

}
