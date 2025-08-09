using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour {


	public float Distance;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		gameObject.transform.position = new Vector3 (GameObject.Find ("Main Camera").gameObject.transform.position.x - GameObject.Find ("Main Camera").gameObject.transform.position.x / Distance, gameObject.transform.position.y, gameObject.transform.position.z);



	}
}
