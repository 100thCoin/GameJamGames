using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour {

	public DataHolder Main;
	public Camera Cam;
	public float vol;


	// Use this for initialization
	void OnMouseDrag()
	{

		vol = (((Input.mousePosition).x / Screen.width)-0.8f) * 6f;
		if(vol > 1){vol = 1;}
		if(vol < 0){vol = 0;}
		transform.localPosition = new Vector3(vol*4,-1.3f,-1);
		Main.Volume = vol;
	}
}
