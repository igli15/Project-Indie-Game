using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBehaviour : MonoBehaviour
{

	[SerializeField] 
	private float m_dashRange = 3;

	[SerializeField] 
	private float m_hitOffset = 0.5f;
	
	private float m_initDashRange;

	private Rigidbody m_rb;
	
	// Use this for initialization
	void Start ()
	{
		m_rb = GetComponent<Rigidbody>();
		m_initDashRange = m_dashRange;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Dash();
		}
	}

	public void Dash()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward * m_dashRange, out hit))
		{
			if(hit.transform != transform && hit.distance <= m_dashRange)
			m_dashRange = hit.distance - m_hitOffset;
		}

		transform.position = transform.position + transform.forward * m_dashRange;
		
		m_dashRange = m_initDashRange;
	}
}
