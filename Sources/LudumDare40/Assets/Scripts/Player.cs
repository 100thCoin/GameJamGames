using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed;
	public bool walking;
	public bool grounded;

	public RuntimeAnimatorController Idle;
	public RuntimeAnimatorController Walk;


	public Sprite Jump;

	public bool Ded;
	public bool Win;

	public bool WinOnce;

	public GameObject WinSplash;

	public int PloogyCount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x + speed*0.03f, gameObject.transform.position.y, gameObject.transform.position.z);
		Rigidbody Rb = gameObject.GetComponent<Rigidbody> ();

		if (!GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().NopMoving) {

			if (Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A)) {

				walking = true;
				speed = speed + Time.fixedDeltaTime * 50;

				gameObject.GetComponent<SpriteRenderer> ().flipX = false;


			}
			if (!Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.A)) {

				walking = true;
				speed = speed - Time.fixedDeltaTime * 50;

				gameObject.GetComponent<SpriteRenderer> ().flipX = true;

			}

			if (speed > 15) {
				speed = 15;

			}
			if (speed < -15) {
				speed = -15;
			}
			


			if ((Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.A)) || (!Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A))) {

				speed = speed * 0.9f;
				if (grounded) {
					gameObject.GetComponent<Animator> ().runtimeAnimatorController = Idle;
				}

			} else {
				if (grounded) {
					gameObject.GetComponent<Animator> ().runtimeAnimatorController = Walk;

				}


			}

			if (gameObject.GetComponent<SpriteRenderer> ().sprite != Jump && grounded && gameObject.GetComponent<Rigidbody> ().velocity.y > 0) {
				gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
			}


			//gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (speed, Rb.velocity.y, Rb.velocity.z);


			if (Input.GetKeyDown (KeyCode.Space) && grounded) {
				grounded = false;
				gameObject.GetComponent<Animator> ().runtimeAnimatorController = null;
				gameObject.GetComponent<SpriteRenderer> ().sprite = Jump;
				gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (Rb.velocity.x, 15, Rb.velocity.z);


			}

			if (Input.GetKey (KeyCode.Space) && grounded) {
				grounded = false;
				gameObject.GetComponent<Animator> ().runtimeAnimatorController = null;
				gameObject.GetComponent<SpriteRenderer> ().sprite = Jump;
				gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (Rb.velocity.x, 15, Rb.velocity.z);


			}
		} else {
			speed = 0;
			gameObject.GetComponent<Animator> ().runtimeAnimatorController = Idle;

		}
		if (!grounded) {
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (Rb.velocity.x, Rb.velocity.y - 0.25f, Rb.velocity.z);


		}

		if (Win && !WinOnce) {
			WinOnce = true;
			Instantiate (WinSplash, new Vector3 (gameObject.transform.position.x, 0, -4), gameObject.transform.rotation);



		}




	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Ground") {

			//grounded = false;

		}



	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Ground") {

			grounded = true;

		}
		if (other.gameObject.tag == "Win") {

			Win = true;

		}		
		if (other.gameObject.tag == "Killzone" && other.gameObject.tag == "Enemy") {

			Ded = true;

		}
		if (other.gameObject.tag == "CantGoRight") {

			if (speed > 0)
			{
				speed = 0;
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x - 0.025f, gameObject.transform.position.y, 0);
			}
		}

		if (other.gameObject.tag == "CantGoLeft") {

			if (speed < 0)
			{
				speed = 0;
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x + 0.025f, gameObject.transform.position.y, 0);

			}
		}

		if (other.gameObject.tag == "Slope") {

			if (speed > 0)
			{
				//gameObject.transform.position = new Vector3 (gameObject.transform.position.x + 0.05f, gameObject.transform.position.y + 0.3f, 0);

				Rigidbody Rb = gameObject.GetComponent<Rigidbody> ();
				gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (Rb.velocity.x, 0, Rb.velocity.z);

			}
		}

		if (other.gameObject.tag == "theend") {

			GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().WInning = true;


		}



	}





}
