using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionAnimation : MonoBehaviour
{
	private Animator m_animator;

	private CompanionSteering m_steering;

	private ACompanion m_companion;
	
	
	// Use this for initialization
	void Start ()
	{
		m_steering = GetComponent<CompanionSteering>();
		m_animator = GetComponentInChildren<Animator>();
		m_companion = GetComponent<ACompanion>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_animator.SetFloat("velocity",m_steering.NavMeshAgent.velocity.magnitude);
		m_animator.SetBool("isThrown",m_companion.IsThrown);
	}
}
