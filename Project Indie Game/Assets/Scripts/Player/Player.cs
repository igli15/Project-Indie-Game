using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{

	[SerializeField]
	private float m_moveSpeed = 5;

	[SerializeField] 
	private float m_rotationSpeed;

	private PlayerController m_playerController;

	private float m_inputDelay = 0.12f;

	private float m_inputDelayCounter;

	private Vector3 m_vel;
	
	// Use this for initialization
	void Start ()
	{
		m_playerController = GetComponent<PlayerController>();
		m_playerController.SetRotationSpeed(m_rotationSpeed);

		m_inputDelayCounter = m_inputDelay;
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_inputDelayCounter -= Time.deltaTime;
		if (m_inputDelayCounter <= 0)
		{
			Vector3 movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
			m_vel = movementInput.normalized * m_moveSpeed;
			m_playerController.SetVelocity(m_vel);
			m_inputDelayCounter = m_inputDelay;
		}
		
	}

	public Vector3 GetVelocity()
	{
		return m_vel;
	}

}
