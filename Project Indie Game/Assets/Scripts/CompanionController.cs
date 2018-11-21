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

	// Use this for initialization
	void Start ()
	{
		m_mainCam = Camera.main;
		m_manager = GetComponent<CompanionManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray camRay = m_mainCam.ScreenPointToRay(Input.mousePosition);
			Plane plane = new Plane(Vector3.up,m_feetPos.position);

			float rayDistance;
			Vector3 dir = Vector3.negativeInfinity;
			if (plane.Raycast(camRay, out rayDistance))
			{
				Vector3 point = camRay.GetPoint(rayDistance);
				dir = point - m_manager.GetSelectedCompanion().transform.position;
			}

			if (dir != Vector3.negativeInfinity)
			{
				if(!m_manager.GetSelectedCompanion().IsThrown)
				m_manager.GetSelectedCompanion().Throw(dir.normalized);
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			m_manager.SelectCompanion(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			m_manager.SelectCompanion(2);
		}
	}
}
