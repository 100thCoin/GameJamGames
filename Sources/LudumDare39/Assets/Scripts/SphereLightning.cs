using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereLightning : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		gameObject.GetComponent<Rigidbody> ().AddRelativeForce (new Vector3 (0, 20, 0));

	}
}
