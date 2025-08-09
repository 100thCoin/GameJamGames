using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualHandRotation : MonoBehaviour {

	public int Obstructed;
	public bool TouchingPlayer;
	public bool Grace;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		TouchingPlayer = false;
		if(!Grace)
		{
		Obstructed = 0;
		}
		Grace = false;
	}

	public Transform TestPoint;

	void OnTriggerStay(Collider other)
	{

		if(other.tag == "Ground")
		{
			TestPoint.transform.position = other.transform.position;
			Obstructed = TestPoint.localPosition.z > 0 ? 1 : -1;
			Grace= true;
		}


		if(other.tag == "Player" && G.Main.CharMov.Movamajig.magnitude != 0)
		{
			float dist = (transform.parent.transform.position - other.transform.position).magnitude;
			TestPoint.transform.position = other.transform.position;
			int dir = TestPoint.localPosition.z > 0 ? -1 : 1;
			if(dir == Obstructed)
			{
				dir = 0;
			}
			else
			{
				dir = 1;
			}
			transform.parent.parent.eulerAngles += new Vector3(0,dist * dir*-TestPoint.localPosition.z,0)*0.035f;
		}

		if(other.tag == "Player")
		{
			TouchingPlayer = true;
		}

	

	}

}
