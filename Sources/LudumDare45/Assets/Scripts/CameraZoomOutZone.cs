using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOutZone : MonoBehaviour {

	public Camera Cam;

	public float StartZoom;
	public float DestZoom;

	// Use this for initialization
	void Start () {
		Cam = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
		{
			float Bound = StartZoom;
			float Dist = (other.transform.position.x - transform.position.x)/transform.lossyScale.x + 0.5f;
			if(Dist > 1){Dist = 1;}else if(Dist < 0){Dist = 0;}
			//Bound =	(-Dist +1)*RuleBound + (Dist)*RuleBound2;
			float a = StartZoom;
			float h = DestZoom;
			float x = Dist;
			float t = 1;

			Bound = -Mathf.Cos(Mathf.PI*x*(1f/t)) * 0.5f*(h-a)+0.5f*(a+h);
			Cam.orthographicSize = Bound;
			Cam.transform.parent.GetComponent<CameraControl>().YBonus = Bound*1.5f-12;

		}
	}
}
