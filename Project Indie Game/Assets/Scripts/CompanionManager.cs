using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionManager : MonoBehaviour
{
	[SerializeField]
	private List<Companion> m_companions = new List<Companion>();

	private Companion m_selectedCompanion;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < m_companions.Count; i++)
		{
			SpawnCompanion(m_companions[i]);
		}

		m_selectedCompanion = m_companions[0];
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
		if(m_selectedCompanion.OnDeSelected != null) m_selectedCompanion.OnDeSelected(m_selectedCompanion);
		
		m_selectedCompanion = m_companions[index - 1];
		
		if(m_selectedCompanion.OnSelected != null) m_selectedCompanion.OnSelected(m_selectedCompanion);
	}
}
