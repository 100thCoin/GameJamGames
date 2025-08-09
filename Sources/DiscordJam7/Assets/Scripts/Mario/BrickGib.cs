using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGib : MonoBehaviour {

	public float XSpeed;
	public float Yspeed;

	float timer = 200;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Yspeed -=3;

		transform.position += new Vector3(XSpeed/128f,Yspeed/128f);
		timer--;
		if(timer<0)
		{
			Destroy(gameObject);
		}
	}
}
