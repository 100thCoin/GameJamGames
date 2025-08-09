using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOverTime : MonoBehaviour {

	public Vector3 Vel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!Global.DataHolder.GameIsPaused) {
			transform.position += Vel * Time.fixedDeltaTime;
		}
	}
}
