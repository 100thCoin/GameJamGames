using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteractable3D : MonoBehaviour {

	public MouseInteractable Conncet;

	// Use this for initialization
	void OnMouseEnter () {
		Conncet.Mouseover = true;
	}
	
	// Update is called once per frame
	void OnMouseExit () {
		Conncet.Mouseover = false;
	}
}
