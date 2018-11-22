using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CompanionSteering))]
public class Companion : MonoBehaviour,ICompanion
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

	protected bool m_isThrown = false;

	protected Vector3 m_throwPos = Vector3.zero;

	protected Material m_material;

	protected int m_index;

	public Action<Companion> OnSpawn;
	public Action<Companion> OnDisable;
	public Action<Companion> OnActivate;
	public Action<Companion> OnThrow;
	public Action<Companion> OnSelected;
	public Action<Companion> OnDeSelected;
	public Action<Companion> OnRangeReached;
	
	// Use this for initialization
	protected void Awake ()
	{
		m_isThrown = false;
		m_material = GetComponent<Renderer>().material;
		
		OnSelected += delegate(Companion companion)	{companion.m_material.color = Color.red;};
		OnDeSelected +=  delegate(Companion companion) { companion.m_material.color = Color.white; };
		
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

	public virtual void Throw(Vector3 dir)
	{
		if (OnThrow != null) OnThrow(this);
			
		m_manager.SelectCompanion(m_index + 1);
		m_steering.StopAgent();
		m_isThrown = true;
	}

	public virtual void Activate(GameObject other = null)
	{
		if (OnActivate != null) OnActivate(this);
	}

	public void CheckIfOutOfRange()
	{
		if (m_isThrown)
		{
			if (Vector3.Distance(m_throwPos, transform.position) >= m_throwRange)
			{
				m_manager.DisableCompanion(this);
				if(OnRangeReached != null) OnRangeReached(this);
			}
		}
	}

	public virtual void Reset()
	{
		m_isThrown = false;
		m_steering.ResumeAgent();
		if(m_rb!= null)
		m_rb.velocity = Vector3.zero;
	}



	public virtual void Spawn()
	{
		if (OnSpawn != null) OnSpawn(this);
		Reset();
		m_steering.FindRandomPositionAroundParent();
	}

	public void SetIndex(int index)
	{
		m_index = index;
	}

	public int GetIndex()
	{
		return m_index;
	}

	public bool IsThrown
	{
		get { return m_isThrown; }
	}
}
