using System;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CompanionSteering))]
public class Companion : MonoBehaviour,ICompanion
{
	[SerializeField] 
	private CompanionManager m_manager; 

	[SerializeField]
	private float m_throwRange = 10;

	[SerializeField]
	private float m_throwSpeed = 20;

	private CompanionSteering m_steering;

	private Rigidbody m_rb;

	private bool m_isThrown = false;

	private Vector3 m_throwPos = Vector3.zero;


	public Action<Companion> OnSpawn;
	public Action<Companion> OnDisable;
	public Action<Companion> OnActivate;
	public Action<Companion> OnThrow;
	public Action<Companion> OnSelected;
	public Action<Companion> OnDeSelected;
	
	// Use this for initialization
	void Awake ()
	{
		m_rb = GetComponent<Rigidbody>();
		m_steering = GetComponent<CompanionSteering>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!m_isThrown)
		{
			m_steering.ResetDestination();
		}

		CheckIfOutOfRange();
		

	}

	public virtual void Throw(Vector3 dir)
	{
		if (!m_isThrown)
		{
			if (OnThrow != null) OnThrow(this);
			m_steering.StopAgent();
			m_throwPos = transform.position;
			m_rb.velocity = dir * m_throwSpeed;   //CHANGE TRANSFORM FORWARD TO MOUSE POINT
			m_isThrown = true;
		}

	}

	public virtual void Activate()
	{
		if (OnActivate != null) OnActivate(this);
		
		Debug.Log("Activate");
	}

	public void CheckIfOutOfRange()
	{
		if (m_isThrown)
		{
			if (Vector3.Distance(m_throwPos, transform.position) >= m_throwRange)
			{
				m_manager.DisableCompanion(this);
				
				//maybe put on activate events here.....
			}
		}
	}

	public void Reset()
	{
		m_isThrown = false;
		m_steering.ResumeAgent();
		if(m_rb!= null)
		m_rb.velocity = Vector3.zero;
	}



	public void Spawn()
	{
		if (OnSpawn != null) OnSpawn(this);
		Reset();
		m_steering.FindRandomPositionAroundParent();
	}

}
