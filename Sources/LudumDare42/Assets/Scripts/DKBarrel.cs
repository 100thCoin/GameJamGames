using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DKBarrel : MonoBehaviour {

	public bool Right;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(Right)
		{
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(4,-3,0);

		}
		else
		{
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-4,-3,0);

		}

	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "LeftWall")
		{
			Right = true;
		}
		else if(other.gameObject.tag == "RightWall")
		{
			Right = false;
		}

	}

}
