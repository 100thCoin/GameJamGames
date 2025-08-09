using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DedTextOffset : MonoBehaviour {

	public float Mult;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 MousePos = Global.DataHolder.ForceCamera.ScreenToWorldPoint (new Vector3(Screen.width,Screen.height) - Input.mousePosition) - new Vector3(0,150,0);
		transform.localPosition = (new Vector3 (MousePos.x, MousePos.y, 0)/16f) * Mult;

	}
}
