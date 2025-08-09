using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownBone : MonoBehaviour {

	// Use this for initialization
	void Start () {

		gameObject.GetComponent<Rigidbody>().velocity = new Vector2(-4,20);
		gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(0,0,400));

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Ground")
		{
			Destroy(gameObject);
		}




	}

}
