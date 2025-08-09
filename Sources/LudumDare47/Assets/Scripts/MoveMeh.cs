using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMeh : MonoBehaviour {

	public GameObject Terget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3(transform.position.x,Terget.transform.position.y,transform.position.z);

	}
}
