﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		if (Input.GetMouseButtonDown(0))
		{
			 m_chargeCount = m_manager.GetSelectedCompanion().ChargeTime;
		}
		if (Input.GetMouseButton(0))
		{
			m_chargeCount -= Time.deltaTime;
			//Debug.Log(m_chargeCount);
			if (m_chargeCount <= 0)
			{
				m_manager.GetSelectedCompanion().IsCharged = true;
				m_chargeCount = m_manager.GetSelectedCompanion().ChargeTime;
			}

		}
		
		if (Input.GetMouseButtonUp(0) && m_manager.GetSelectedCompanion().IsCharged)
		{
			ThrowAtMousePos(m_manager.GetSelectedCompanion());
		}

		
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
				companion.Throw(dir.normalized);
			}
			
		}
	}
}
