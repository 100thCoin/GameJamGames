using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkArrow : MonoBehaviour {

	public int timer;
	public SpriteRenderer SR;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		timer++;
		if(timer > 32)
		{
			timer=0;
		}
		SR.enabled = timer < 16;
	}
}
