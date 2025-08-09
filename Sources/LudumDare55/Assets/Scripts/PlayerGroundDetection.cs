using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attatched to a small collider on the player's feet. This determines if you cna jump or not.

public class PlayerGroundDetection : MonoBehaviour {

	public PlayerController PC;

	public int coyoteTime;
	public int MaxcoyoteTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		coyoteTime--;
		if (coyoteTime < 0) {
			PC.grounded = false;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag ("Ground")) {
			if (PC.RB.velocity.y <= 0) {
				PC.grounded = true;

				coyoteTime = MaxcoyoteTime;
			}
		}


	}
}
