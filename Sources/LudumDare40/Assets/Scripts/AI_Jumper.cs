using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Jumper : MonoBehaviour {

	public bool InRange;
	public bool going;

	public Sprite Idle;
	public Sprite Idle2;
	public Sprite PreJump;
	public Sprite Jumpin;
	public bool grounded;

	public bool CheckOnce;

	// Use this for initialization
	void Start () {

		going = false;
		InRange = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (InRange && !going && grounded) {
			going = true;
			StartCoroutine (Jump());

		}

		if (gameObject.transform.position.x - GameObject.Find ("Player").gameObject.transform.position.x < 16) {
			InRange = true;
		}

	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Ground") {

			grounded = false;

		}



	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Ground") {

			grounded = true;

		}



	}	

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Ground") {

			gameObject.GetComponent<SpriteRenderer> ().sprite = Idle;

		}
		if (other.gameObject.tag == "Laser" && InRange) {

			gameObject.transform.Find ("HealthBar").gameObject.transform.Find ("HealthBarBar").gameObject.GetComponent<HealthBar> ().Hp -= 1; 

		}


	}

	IEnumerator Jump ()
	{
		yield return new WaitForSeconds (0.125f);
		gameObject.GetComponent<SpriteRenderer> ().sprite = Idle2;

		yield return new WaitForSeconds (0.125f);

		gameObject.GetComponent<SpriteRenderer> ().sprite = PreJump;
		yield return new WaitForSeconds (0.25f);
		gameObject.GetComponent<SpriteRenderer> ().sprite = Jumpin;
		gameObject.GetComponent<Rigidbody> ().AddForce ((GameObject.Find ("Player").gameObject.transform.position.x - gameObject.transform.position.x) * 0.75f, 10, 0, ForceMode.Impulse);
		going = false;
		grounded = false;

	}


}
