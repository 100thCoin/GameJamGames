using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleParallax : MonoBehaviour {

	public float Mult;
	public Camera Cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 MousePos = Cam.ScreenToWorldPoint (Input.mousePosition);
		transform.localPosition = (transform.localPosition*5 + MousePos * Mult)/6f;

	}
}
