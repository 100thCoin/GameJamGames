using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Dropper : MonoBehaviour {

	public bool Drop;
	public Sprite Dropping;
	public GameObject HB;

	public bool InRange;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if (gameObject.transform.position.x - GameObject.Find ("Player").gameObject.transform.position.x < 16) {
			InRange = true;
		}



		if (gameObject.transform.position.x - GameObject.Find ("Player").gameObject.transform.position.x < -2) {
			Drop = true;
		}


		if (Drop) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = Dropping;
			HB.gameObject.SetActive (true);
			gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		}




	}



	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Laser" && InRange) {

			gameObject.transform.Find ("HealthBar").gameObject.transform.Find ("HealthBarBar").gameObject.GetComponent<HealthBar> ().Hp -= 1; 
			Drop = true;
		}


	}


}
