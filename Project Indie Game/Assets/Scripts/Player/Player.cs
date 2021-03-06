﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{

	[SerializeField]
	private float m_moveSpeed = 5;

	[SerializeField]
	private float m_slowedDownMoveSpeed = 3;

	[SerializeField] 
	private float m_rotationSpeed;

	private PlayerController m_playerController;

	private float m_inputDelay = 0.12f;

	private float m_inputDelayCounter;

	private Vector3 m_vel;

	private CompanionController m_companionController;

	private float m_initSpeed;

	
	// Use this for initialization
	void Start ()
	{
		m_companionController = GetComponent<CompanionController>();
		m_playerController = GetComponent<PlayerController>();
		m_playerController.SetRotationSpeed(m_rotationSpeed);

		m_inputDelayCounter = m_inputDelay;

		m_initSpeed = m_moveSpeed;
		
		CompanionController.OnMouseCharging += delegate(CompanionController controller, ACompanion companion){m_moveSpeed = m_slowedDownMoveSpeed; };
		CompanionController.OnMouseRelease += delegate(CompanionController controller, ACompanion companion){m_moveSpeed = m_initSpeed;};
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

	private void FixedUpdate()
	{
		if (!m_companionController.IsCharging)
		{
			if(m_vel != Vector3.zero)
			m_playerController.Rotate(m_vel);
		}
		else
		{
			m_playerController.Rotate(m_companionController.MouseDir.normalized);
		}
	}

	public Vector3 GetVelocity()
	{
		return m_vel;
	}

	public float MoveSpeed
	{
		get { return m_moveSpeed; }
		set { m_moveSpeed = value; }
	}

}
