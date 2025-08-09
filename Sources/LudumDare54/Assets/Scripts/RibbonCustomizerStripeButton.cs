using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonCustomizerStripeButton : MonoBehaviour {

	public bool MouseOver;
	public int Grace;

	void OnMouseOver () {
		MouseOver = true;
		Grace = 3;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Grace< 0)
		{
		MouseOver = false;
		}
		Grace--;
	}
}
