using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour {


	public Vector3 RotationOTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x + RotationOTime.x * Time.fixedDeltaTime,gameObject.transform.eulerAngles.y + RotationOTime.y * Time.fixedDeltaTime, gameObject.transform.eulerAngles.z + RotationOTime.z * Time.fixedDeltaTime);

	}
}
