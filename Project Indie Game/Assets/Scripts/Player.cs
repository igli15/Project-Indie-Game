using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{

	[SerializeField]
	private float m_moveSpeed = 5;

	private PlayerController m_playerController;
	
	
	// Use this for initialization
	void Start ()
	{
		m_playerController = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 movementInput = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
		Vector3 velocity = movementInput * m_moveSpeed;
		m_playerController.SetVelocity(velocity);
	}

}
