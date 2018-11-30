using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

	private float m_rotationSpeed = 0 ;
	
	private Vector3 m_velocity;
	private Rigidbody m_rb;
	private Quaternion m_targetRotation;
	
	
	// Use this for initialization
	void Start ()
	{
		m_rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		m_rb.MovePosition(transform.position + m_velocity * Time.fixedDeltaTime);
		//Rotate();
 	}

	public void SetVelocity(Vector3 velocity)
	{
		m_velocity = velocity;
	}

	public void SetRotationSpeed(float rotationSpeed)
	{
		m_rotationSpeed = rotationSpeed;
	}

	public void Rotate(Vector3 dir)
	{
		if (m_velocity != Vector3.zero)
		{
			m_targetRotation = Quaternion.LookRotation(m_velocity);
			m_rb.rotation = m_targetRotation;
			/*m_rb.rotation =Quaternion.Euler(
				Vector3.up * Mathf.MoveTowardsAngle(m_rb.rotation.eulerAngles.y, m_targetRotation.eulerAngles.y, 
													m_rotationSpeed * Time.fixedDeltaTime));*/
		}

	}
}
