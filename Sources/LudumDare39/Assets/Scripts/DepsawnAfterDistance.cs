using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepsawnAfterDistance : MonoBehaviour {

	public bool seventy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (seventy) {
			if (GameObject.Find ("Proto_Idle_1").gameObject.transform.position.y > gameObject.transform.position.y + 70) {
				Destroy (gameObject);
			}
		}

		if (GameObject.Find ("Proto_Idle_1").gameObject.transform.position.y > gameObject.transform.position.y + 100) {
			Destroy (gameObject);
		}
	}
}
