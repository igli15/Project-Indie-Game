﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionManager : MonoBehaviour
{
	[SerializeField]
	private List<Companion> m_companions = new List<Companion>();

	private Companion m_selectedCompanion;

	private float m_companionCount;

	// Use this for initialization
	void Start ()
	{
		m_companionCount = m_companions.Count;
		for (int i = 0; i < m_companionCount; i++)
		{
			SpawnCompanion(m_companions[i]);
			m_companions[i].SetIndex(i + 1);
		}

		SelectCompanion(1);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void SpawnCompanion(Companion companion)
	{
		companion.gameObject.SetActive(true);
		companion.Spawn();
	}

	public void DisableCompanion(Companion companion)
	{
		if (companion.OnDisable != null) companion.OnDisable(companion);
		
		companion.gameObject.SetActive(false);
		StartCoroutine(ReSpawnCooldown(companion));

	}
	
	IEnumerator ReSpawnCooldown(Companion companion)
	{
		yield return new WaitForSeconds(3);
		SpawnCompanion(companion);
	}

	public Companion GetSelectedCompanion()
	{
		return m_selectedCompanion;
	}

	public void SelectCompanion(int index)
	{
		if (index > m_companionCount) index = 1;   //mirror the array if you go past its count
		
		Companion compToSelect = m_companions[index - 1];	// get the companion we need to change to

		if (compToSelect != null && !compToSelect.IsThrown) // check if its null and if its thrown
		{

			if (m_selectedCompanion != null && m_selectedCompanion.OnDeSelected != null)  //Call on deselect for the current 
				m_selectedCompanion.OnDeSelected(m_selectedCompanion);

			m_selectedCompanion = m_companions[index - 1];      //Set the companion to selected

			if (m_selectedCompanion.OnSelected != null) m_selectedCompanion.OnSelected(m_selectedCompanion); //call select action
		}

	}

}
