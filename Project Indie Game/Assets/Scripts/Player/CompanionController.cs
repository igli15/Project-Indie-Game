using System;
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
		}
		if (Input.mouseScrollDelta.y * m_scorllScale < 0)
		{
			m_manager.SelectPreviousCompanion();
		}
	}

	private void HandleMouseAim()
	{
		if (Input.GetMouseButton(0))
		{
			m_startCharge = true;
			//StartCoroutine(ChargeThrow(m_manager.GetSelectedCompanion()));
			
		}
		

		if (m_startCharge)
		{
			Debug.Log("hi");
			m_startCharge = false;
			StartCoroutine(ChargeThrow(m_manager.GetSelectedCompanion()));
			Debug.Log(Input.GetMouseButtonUp(0));
			if (Input.GetMouseButtonUp(0) && m_manager.GetSelectedCompanion().IsCharged)
			{
				Debug.Log("T");
				m_manager.GetSelectedCompanion().IsCharged = false;
				ThrowAtMousePos(m_manager.GetSelectedCompanion());
			}
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

	IEnumerator ChargeThrow(ACompanion companion)
	{
		yield return new WaitForSeconds(companion.ChargeTime);
		Debug.Log("finished charging");
		companion.IsCharged = true;
	}
}
