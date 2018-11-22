using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HudManager : MonoBehaviour
{
	
	[Serializable]
	public class HudIcon
	{
		public ACompanion companion;
		public GameObject icon;
	}
	
	[SerializeField]
	private List<HudIcon> m_hudIcons = new List<HudIcon>();

	[SerializeField]
	[Range(1,2)]
	private float m_scaleFactor = 1.2f;

	[SerializeField] 
	private float m_scaleTime = 0.2f;
	
	private Dictionary<ACompanion, GameObject> m_hudIconDictionary = new Dictionary<ACompanion, GameObject>();
	
	// Use this for initialization
	void Start () 
	{
		foreach (HudIcon icon in m_hudIcons)
		{
			m_hudIconDictionary.Add(icon.companion,icon.icon);

			icon.companion.OnSelected += SelectIcon;
			icon.companion.OnDeSelected += DeselectIcon;
		}

		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void SelectIcon(ACompanion companion)
	{
		m_hudIconDictionary[companion].transform.DOScale(m_scaleFactor,m_scaleTime);
	}
	public void DeselectIcon(ACompanion companion)
	{
		m_hudIconDictionary[companion].transform.DOScale(1, m_scaleTime);
	}
}
