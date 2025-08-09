using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillCloudHolder : MonoBehaviour {

	public GameObject Target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Target.transform.position;

	}
}
