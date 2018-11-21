using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionManager : MonoBehaviour
{
	[SerializeField]
	private List<Companion> m_companions = new List<Companion>();

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < m_companions.Count; i++)
		{
			SpawnCompanion(m_companions[i]);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void SpawnCompanion(Companion companion)
	{
		companion.gameObject.SetActive(true);
		companion.Respawn();
	}

	public void DisableCompanion(Companion companion)
	{
		companion.gameObject.SetActive(false);
		StartCoroutine(ReSpawnCooldown(companion));

	}
	
	IEnumerator ReSpawnCooldown(Companion companion)
	{
		yield return new WaitForSeconds(3);
		SpawnCompanion(companion);
	}
}
