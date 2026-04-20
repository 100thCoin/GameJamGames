using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMovement : MonoBehaviour {

	public Transform Gun;
	public Camera Cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 GunPoint = Cam.ScreenToWorldPoint (Input.mousePosition);
		Gun.transform.localPosition = (new Vector3 (GunPoint.x, GunPoint.y, 0) - (new Vector3 (transform.position.x, transform.position.y + 0.5f, 0)));

		if (Gun.transform.localPosition.magnitude > 1) {
			Gun.transform.localPosition = Gun.transform.localPosition.normalized;
		}
		Gun.transform.localPosition *= 0.4f;
	}
}
