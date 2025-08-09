using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prallax : MonoBehaviour {

	public float Distace;
	public CameraMove Reference;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3((Reference.transform.position.x * (1f/Distace-1)) % 24, transform.position.y,transform.position.z);


	}
}
