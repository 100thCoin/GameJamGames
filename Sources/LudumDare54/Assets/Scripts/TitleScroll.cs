using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScroll : MonoBehaviour {

	public float speed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.localPosition += new Vector3 (-speed*Time.deltaTime, 0, 0);

		if (transform.localPosition.x < -228.5f) {

			transform.localPosition += new Vector3 (28.5f, 0, 0);

		}

	}
}
