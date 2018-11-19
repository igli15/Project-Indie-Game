using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

	private Vector3 m_velocity;
	private Rigidbody m_rb;
	
	// Use this for initialization
	void Start ()
	{
		m_rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		m_rb.MovePosition(transform.position + m_velocity * Time.fixedDeltaTime);
	}

	public void SetVelocity(Vector3 velocity)
	{
		m_velocity = velocity;
	}
}
