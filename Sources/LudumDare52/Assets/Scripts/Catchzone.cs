using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catchzone : MonoBehaviour {

	public NPC guy;
	public PlayerMovement P;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			if (P == null) {
				P = other.GetComponent<PlayerMovement> ();
			}

			P.CaughtBy = guy;
			P.Caught = true;
			P.CaughtTimer = 0;
			guy.Alerted = true;
			guy.AlertRenderer.enabled = true;
			P.CMov.CaughtCam = true;
			P.CMov.CaughtTarg = guy.transform;
		}


	}

}
