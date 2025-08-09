using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralaxee : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public bool DoOnce;

	// Update is called once per frame
	void Update () {
	

		if (gameObject.transform.position.x - GameObject.Find ("Main Camera").gameObject.transform.position.x < 24) {
			if (!DoOnce) {
				Instantiate (gameObject, new Vector3 (gameObject.transform.position.x + 16, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);
				DoOnce = true;

			}
		}


	}
}
