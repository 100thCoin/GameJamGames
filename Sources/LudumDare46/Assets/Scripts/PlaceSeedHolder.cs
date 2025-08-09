using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSeedHolder : MonoBehaviour {


	public Farm F;
	public Camera Cam;

	public bool Combat;

	void Update () {


		if(F.InvisMercyFrame)
		{
			Vector3 MousePos = Cam.ScreenToWorldPoint (Input.mousePosition) + new Vector3(0,0,2);

			transform.position = MousePos;
		}
		else
		{
			transform.position = new Vector3(0,0,-500);
		}

		if(F.PlacingBulb)
		{
			Vector3 MousePos = Cam.ScreenToWorldPoint (Input.mousePosition) + new Vector3(0,0,2);

			transform.position = MousePos;
		}

		if(Combat)
		{
			Vector3 MousePos = Cam.ScreenToWorldPoint (Input.mousePosition) + new Vector3(0,0,1);

			transform.position = MousePos;


		}


	}
}
