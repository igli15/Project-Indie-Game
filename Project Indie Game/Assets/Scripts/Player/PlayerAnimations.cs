using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
	private Player m_player;

	private Animator m_animator;

	private Health m_health;
	
	// Use this for initialization
	void Start ()
	{
		m_animator = GetComponentInChildren<Animator>();
		m_player = GetComponent<Player>();
		m_health = GetComponent<Health>();

		m_health.OnHealthDecreased += health => Debug.Log("hit");
		m_health.OnHealthDecreased += health => m_animator.SetTrigger("isDamaged");
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_animator.SetFloat("velocity",m_player.MoveSpeed);
		m_animator.SetBool("inputIsGiven",m_player.InputIsGiven);
	}
}
