using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLayer2Loop : MonoBehaviour {

	public Camera Cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Cam.transform.position.y < 20) {
			transform.position = Cam.ScreenToWorldPoint (Input.mousePosition) + new Vector3(0,0,10);
		}

	}
}
