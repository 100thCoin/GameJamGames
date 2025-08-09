using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeObjectsInPause : MonoBehaviour {

	public Rigidbody RB;
	public Vector3 PauseVel;
	public bool UnpauseOnce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if ((Global.DataHolder.GameIsPaused || Global.DataHolder.WinGame)) {
			if (UnpauseOnce) {
				PauseVel = RB.velocity;
				RB.isKinematic = true;
			}
			UnpauseOnce = false;
		} else {
			if (!UnpauseOnce) {
				RB.isKinematic = false;
				RB.velocity = PauseVel;
			}
			UnpauseOnce = true;
		}

	}
}
