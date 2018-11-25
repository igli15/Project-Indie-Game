using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SniperCompanion : Companion
{

	[SerializeField] 
	private float m_playerSlowAmount = 2;

	[SerializeField] 
	[Range(0,100)]
	private float m_piercingDamage = 50;

	[SerializeField] 
	[Range(0,100)]
	private float m_fullDamage = 100;
	
	private GameObject m_targetEnemy;

	private Player m_player;

	private float m_playerInitSpeed;

	private Collider m_collider;
	
	
	private void Awake()
	{
		base.Awake();
	}
	
	// Use this for initialization
	void Start ()
	{
		m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		m_playerInitSpeed = m_player.MoveSpeed;
		m_collider = GetComponent<Collider>();
		OnStartCharging += delegate(ACompanion companion) {m_player.MoveSpeed -= m_playerSlowAmount;};
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
		m_collider.enabled = false;
		m_targetEnemy = null;
		base.Reset();
	}

	public override void Throw(Vector3 dir)
	{
		m_collider.enabled = true;
		m_player.MoveSpeed = m_playerInitSpeed;
		
		base.Throw(dir);
		RaycastHit[] hits;
		
		hits = Physics.RaycastAll(transform.position, dir,m_throwRange).OrderBy(d=>d.distance).ToArray();

		for (int i = 0; i < hits.Length; i++)
		{	
			if (hits[i].transform.CompareTag("Obstacle"))
			{
				break;
			}
			if (hits[i].transform.CompareTag("Enemy"))
			{
				m_targetEnemy = hits[i].transform.gameObject;
			}
		}
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Enemy"))
		{
			if (m_targetEnemy != null)
			{
				if (other.transform == m_targetEnemy.transform)
				{
					other.GetComponent<Health>().InflictDamage(m_fullDamage);
					m_manager.DisableCompanion(this);
				}
				else
				{
					other.GetComponent<Health>().InflictDamage(m_piercingDamage);
				}
			}
		}
		else if (other.transform.CompareTag("Obstacle"))
		{
			m_manager.DisableCompanion(this);
		}
		
	}
}
