using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour {

	public SpriteRenderer Bubble;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Bubble.enabled = false;

	}

	void OnTriggerStay(Collider other)
	{

		if(other.name == "Player")
		{
			Bubble.enabled = true;
		}

	}



}
