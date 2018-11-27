﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionManager : MonoBehaviour
{
	[SerializeField]
	private List<ACompanion> m_companions = new List<ACompanion>();

	private ACompanion m_selectedCompanion;

	private int m_companionCount;

	// Use this for initialization
	void Start ()
	{
		m_companionCount = m_companions.Count;
		SpawnCompanions();

		SelectCompanion(1);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			DropCompanion(m_selectedCompanion);
		}
	}

	public void SpawnCompanion(ACompanion companion)
	{
		companion.gameObject.SetActive(true);
		companion.Spawn();
	}

	public void DisableCompanion(ACompanion companion)
	{
		if (companion.OnDisable != null) companion.OnDisable(companion);
		
		companion.gameObject.SetActive(false);
		StartCoroutine(ReSpawnCooldown(companion));

	}
	
	IEnumerator ReSpawnCooldown(ACompanion companion)
	{
		yield return new WaitForSeconds(3);
		SpawnCompanion(companion);
	}

	public ACompanion GetSelectedCompanion()
	{
		return m_selectedCompanion;
	}

	public void SelectNextCompanion()
	{
		if (m_selectedCompanion.Index >= m_companionCount)
		{
			m_selectedCompanion.IsCharged = false;
			SelectCompanion(1);
			m_selectedCompanion.IsCharged = false;
		}
		else
		{
			m_selectedCompanion.IsCharged = false;
			SelectCompanion(m_selectedCompanion.Index + 1);
			m_selectedCompanion.IsCharged = false;
		}
	}

	public void SelectPreviousCompanion()
	{
		if (m_selectedCompanion.Index <= 1)
		{
			m_selectedCompanion.IsCharged = false;
			SelectCompanion(m_companionCount);
			m_selectedCompanion.IsCharged = false;
		}
		else
		{
			m_selectedCompanion.IsCharged = false;
			SelectCompanion(m_selectedCompanion.Index - 1 );
			m_selectedCompanion.IsCharged = false;
		}
	}

	public void SelectCompanion(int index)
	{
		//if (index > m_companionCount) index = 1;   //mirror the array if you go past its count
		
		ACompanion compToSelect = m_companions[index - 1];	// get the companion we need to change to

		if (compToSelect != null && !compToSelect.IsThrown) // check if its null and if its thrown
		{

			if (m_selectedCompanion != null && m_selectedCompanion.OnDeSelected != null)  //Call on deselect for the current 
				m_selectedCompanion.OnDeSelected(m_selectedCompanion);

			m_selectedCompanion = m_companions[index - 1];      //Set the companion to selected

			if (m_selectedCompanion.OnSelected != null) m_selectedCompanion.OnSelected(m_selectedCompanion); //call select action
		}

	}


	private void DropCompanion(ACompanion companion)
	{
		companion.Reset();
		companion.IsInParty = false;
		companion.SteeringComponent.NavMeshAgent.enabled = false;
		m_companionCount -= 1;
		m_companions.Remove(companion);
		AssignCompanionsIndex();
		SelectPreviousCompanion();
	}

	private void PickCompanion(ACompanion companion)
	{
		companion.Spawn();
		companion.IsInParty = true;
		companion.SteeringComponent.NavMeshAgent.enabled = true;
		m_companionCount += 1;
		m_companions.Add(companion);
		AssignCompanionsIndex();
		SelectCompanion(companion.Index);
		
	}


	private void AssignCompanionsIndex()
	{
		for (int i = 0; i < m_companionCount; i++)
		{
			m_companions[i].Index = i+1;
		}
	}
	
	private void SpawnCompanions()
	{
		for (int i = 0; i < m_companionCount; i++)
		{
			SpawnCompanion(m_companions[i]);
			m_companions[i].Index = i+1;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.transform.CompareTag("PickupSphere") && !other.transform.parent.GetComponent<Companion>().IsInParty &&  Input.GetKeyDown(KeyCode.P))
		{
			PickCompanion(other.transform.parent.GetComponent<Companion>());
		}
	}


}
