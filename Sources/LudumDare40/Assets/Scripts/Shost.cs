using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shost : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		gameObject.GetComponent<Rigidbody> ().AddForce (-(gameObject.transform.position.x - GameObject.Find("Player").gameObject.transform.position.x) * 0.1f,-(gameObject.transform.position.y - GameObject.Find("Player").gameObject.transform.position.y) * 0.1f,0,ForceMode.Impulse);

		gameObject.GetComponent<Rigidbody> ().AddForce (Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),0,ForceMode.Impulse);




	}
}
