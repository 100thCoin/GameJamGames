using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeMarker : MonoBehaviour {

	public Transform Home;
	float dir;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		transform.parent.position = Home.position;
		if(transform.parent.localPosition.x > 22)
		{
			transform.parent.localPosition = new Vector3(22,transform.parent.localPosition.y,transform.parent.localPosition.z);
		}
		else if(transform.parent.localPosition.x < -22)
		{
			transform.parent.localPosition = new Vector3(-22,transform.parent.localPosition.y,transform.parent.localPosition.z);
		}
		if(transform.parent.localPosition.y > 12)
		{
			transform.parent.localPosition = new Vector3(transform.parent.localPosition.x,12,transform.parent.localPosition.z);
		}
		else if(transform.parent.localPosition.y < -14)
		{
			transform.parent.localPosition = new Vector3(transform.parent.localPosition.x,-14,transform.parent.localPosition.z);
		}


		dir = Mathf.Atan2(Home.position.y-transform.position.y,Home.position.x-transform.position.x);
		transform.eulerAngles = new Vector3(0,0,dir * Mathf.Rad2Deg - 90);



	}
}
