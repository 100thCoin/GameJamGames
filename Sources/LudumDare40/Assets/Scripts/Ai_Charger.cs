using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Charger : MonoBehaviour {
	public bool InRange;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (gameObject.transform.position.x - GameObject.Find ("Player").gameObject.transform.position.x < 16) {
			InRange = true;
		}
		if(InRange)
		{
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (-4, gameObject.GetComponent<Rigidbody> ().velocity.y, 0);
		}




	}


	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Laser" && InRange) {

			gameObject.transform.Find ("HealthBar").gameObject.transform.Find ("HealthBarBar").gameObject.GetComponent<HealthBar> ().Hp -= 1; 
		}


	}
}
