using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron_AI : MonoBehaviour {
	public float MaxHP;
	public float HP;

	public GameObject Death;

	public GameObject Bar;

	public Sprite Idle1;
	public Sprite Hurt;



	public bool Run;
	public bool RunOnce;
	public bool IdleOnce;

	public float speed;

	public float RandomDelay;

	public bool Acidic;

	// Use this for initialization
	void Start () {

		RandomDelay = Random.Range (0, 2);
		StartCoroutine (RunIdle ());
		gameObject.name = "Iron";

	}

	// Update is called once per frame
	void Update () {

		if (Acidic) {
			HP = HP - Time.deltaTime * 12;
			Bar.gameObject.GetComponent<HPBar> ().precent = HP / MaxHP * 100;

		}






		if (HP <= 0) {
			GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Experience = GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Experience + 100;
			Instantiate (Death, gameObject.transform.position, gameObject.transform.rotation);
			Destroy (gameObject);
		}

		if (!Run) {
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector2 (gameObject.GetComponent<Rigidbody> ().velocity.x * 0.95f, gameObject.GetComponent<Rigidbody> ().velocity.y * 0.95f);


			gameObject.GetComponent<SpriteRenderer>().sprite = Idle1;


			gameObject.name = "IronIdle";



		}

		if (Run) {
			gameObject.GetComponent<SpriteRenderer>().sprite = Idle1;

			gameObject.GetComponent<Rigidbody> ().AddExplosionForce(-50,GameObject.Find("Proto_Idle_1").gameObject.transform.position,10000);

			if (gameObject.GetComponent<Rigidbody> ().velocity.x > speed) {
				gameObject.GetComponent<Rigidbody> ().velocity = new Vector2(speed,gameObject.GetComponent<Rigidbody> ().velocity.y);
			}
			if (gameObject.GetComponent<Rigidbody> ().velocity.x < -speed) {
				gameObject.GetComponent<Rigidbody> ().velocity = new Vector2(-speed,gameObject.GetComponent<Rigidbody> ().velocity.y);
			}
			if (gameObject.GetComponent<Rigidbody> ().velocity.y > speed) {
				gameObject.GetComponent<Rigidbody> ().velocity = new Vector2(gameObject.GetComponent<Rigidbody> ().velocity.x,speed);
			}
			if (gameObject.GetComponent<Rigidbody> ().velocity.y < -speed) {
				gameObject.GetComponent<Rigidbody> ().velocity = new Vector2(gameObject.GetComponent<Rigidbody> ().velocity.x,-speed);
			}


			if (gameObject.GetComponent<Rigidbody> ().velocity.x > 0) {

				gameObject.GetComponent<SpriteRenderer> ().flipX = true;

			}

			if (gameObject.GetComponent<Rigidbody> ().velocity.x <= 0) {

				gameObject.GetComponent<SpriteRenderer> ().flipX = false;

			}
			gameObject.name = "Iron";

		}




	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PlayerAttack") {

			HP = HP - GameObject.Find ("Main").GetComponent<DataHolder> ().PlayerDamage;
			gameObject.GetComponent<SpriteRenderer>().sprite = Hurt;

			Bar.gameObject.GetComponent<HPBar> ().precent = HP / MaxHP * 100;

			if (GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().ItemType3 == 1) {

				Acidic = true;

			}


		}



	}

	IEnumerator RunIdle()
	{
		yield return new WaitForSeconds (RandomDelay);
		RandomDelay = Random.Range (2, 8);
		Run = true;
		RunOnce = false;
		yield return new WaitForSeconds (RandomDelay);
		RandomDelay = Random.Range (3, 6);
		Run = false;
		IdleOnce = false;
		StartCoroutine (RunIdle());

	}


}
