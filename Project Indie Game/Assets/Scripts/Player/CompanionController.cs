using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CompanionManager))]
public class CompanionController : MonoBehaviour
{
	[SerializeField] 
	private Transform m_feetPos;
	
	private CompanionManager m_manager;
	private Camera m_mainCam;

	private bool m_startCharge = false;

	private const int m_scorllScale = 10;

	private float m_chargeCount;

	// Use this for initialization
	void Start ()
	{
		m_mainCam = Camera.main;
		m_manager = GetComponent<CompanionManager>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		HandleScrollWheel();
		
		HandleMouseAim();

		HandleNumInput();
	}

	private void HandleScrollWheel()
	{
		if (Input.mouseScrollDelta.y  * m_scorllScale > 0)
		{
			m_manager.SelectNextCompanion();
			m_chargeCount = m_manager.GetSelectedCompanion().ChargeTime;
		}
		if (Input.mouseScrollDelta.y * m_scorllScale < 0)
		{
			m_manager.SelectPreviousCompanion();
			m_chargeCount = m_manager.GetSelectedCompanion().ChargeTime;
		}
	}

	private void HandleMouseAim()
	{
		ChargeCompanion(m_manager.GetSelectedCompanion());
	}

	private void HandleNumInput()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			m_manager.SelectCompanion(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			m_manager.SelectCompanion(2);
		}
	}

	private void ThrowAtMousePos(ACompanion companion)
	{
		Ray camRay = m_mainCam.ScreenPointToRay(Input.mousePosition);
		Plane plane = new Plane(Vector3.up,m_feetPos.position);

		float rayDistance;
		Vector3 dir = Vector3.negativeInfinity;
		if (plane.Raycast(camRay, out rayDistance))
		{
			Vector3 point = camRay.GetPoint(rayDistance);
			dir = point - companion.transform.position;
		}

		if (dir != Vector3.negativeInfinity)
		{
			if (!companion.IsThrown)
			{
				companion.Throw(dir);
			}
			
		}
	}

	private void ChargeCompanion(ACompanion companion)
	{
		if (Input.GetMouseButtonDown(0))
		{
			m_chargeCount = companion.ChargeTime;
			companion.transform.position = m_feetPos.position + m_feetPos.transform.forward;
			companion.GetComponent<NavMeshAgent>().enabled = false;
			if (companion.OnStartCharging != null) companion.OnStartCharging(companion);
		}
		if (Input.GetMouseButton(0))
		{
			if(companion.OnCharging != null) companion.OnCharging(companion);	
			m_chargeCount -= Time.deltaTime;
			
			if (m_chargeCount <= 0)
			{
				if(companion.OnChargeFinished != null) companion.OnChargeFinished(companion);
				
				companion.IsCharged = true;
				m_chargeCount = companion.ChargeTime;
			}

		}
		
		if (Input.GetMouseButtonUp(0))
		{
			if (companion.IsCharged)
			{
				ThrowAtMousePos(companion);
			}
			else
			{
				companion.GetComponent<NavMeshAgent>().enabled = true;
				companion.Spawn();
			}
		}
		
	}
}
