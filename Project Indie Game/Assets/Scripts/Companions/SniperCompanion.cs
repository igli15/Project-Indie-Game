using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SniperCompanion : Companion
{

	[SerializeField] 
	private float m_playerSlowAmount = 2;
	
	private GameObject m_targetEnemy;

	private Player m_player;

	private float m_playerInitSpeed;
	
	
	private void Awake()
	{
		base.Awake();
	}
	
	// Use this for initialization
	void Start ()
	{
		m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		m_playerInitSpeed = m_player.MoveSpeed;

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
		m_targetEnemy = null;
		base.Reset();
	}

	public override void Throw(Vector3 dir)
	{
		m_player.MoveSpeed = m_playerInitSpeed;
		
		base.Throw(dir);
		RaycastHit[] hits;
		
		hits = Physics.RaycastAll(transform.position, dir,m_throwRange).OrderBy(d=>d.distance).ToArray();

		for (int i = 0; i < hits.Length; i++)
		{
			Debug.Log(hits[i].transform.gameObject);
			
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
			if (m_targetEnemy != null && other.transform == m_targetEnemy.transform)
			{
				other.GetComponent<Health>().InflictDamage(100);
				m_manager.DisableCompanion(this);
			}
			else
			{
				other.GetComponent<Health>().InflictDamage(50);
			}
		}
		else if (other.transform.CompareTag("Obstacle"))
		{
			m_manager.DisableCompanion(this);
		}
		
	}
}
