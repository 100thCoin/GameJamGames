using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstadeathRay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		gameObject.transform.position = new Vector3 (GameObject.Find ("Proto_Idle_1").gameObject.transform.position.x , gameObject.transform.position.y + Time.deltaTime * 1.5f, gameObject.transform.position.z);

		if (gameObject.transform.position.y < GameObject.Find ("Proto_Idle_1").gameObject.transform.position.y - 30) {
			gameObject.transform.position = new Vector3 (gameObject.transform.position.y, GameObject.Find ("Proto_Idle_1").gameObject.transform.position.y - 30, gameObject.transform.position.z);
		}


	}
}
