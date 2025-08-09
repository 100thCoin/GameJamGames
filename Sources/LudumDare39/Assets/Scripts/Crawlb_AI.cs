using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawlb_AI : MonoBehaviour {

	public float MaxHP;
	public float HP;

	public GameObject Death;

	public GameObject Bar;

	public RuntimeAnimatorController Idle1;
	public RuntimeAnimatorController Idle2;
	public RuntimeAnimatorController Idle3;
	public RuntimeAnimatorController Idle4;

	public RuntimeAnimatorController Run1;
	public RuntimeAnimatorController Run2;
	public RuntimeAnimatorController Run3;
	public RuntimeAnimatorController Run4;

	public float Form;

	public bool Run;
	public bool RunOnce;
	public bool IdleOnce;
	public float FormChange;

	public float speed;

	public float RandomDelay;

	public bool Acidic;

	// Use this for initialization
	void Start () {

		RandomDelay = Random.Range (0, 2);
		StartCoroutine (RunIdle ());
		gameObject.name = "Crawlb";

	}
	
	// Update is called once per frame
	void Update () {

		if (Acidic) {
			HP = HP - Time.deltaTime * 12;
			Bar.gameObject.GetComponent<HPBar> ().precent = HP / MaxHP * 100;

		}


		FormChange = Form;
		if (HP > 150 && HP <= 200) {Form = 1;}
		if (HP > 100 && HP <= 150) {Form = 2;}
		if (HP > 50 && HP<= 100) {Form = 3;}
		if (HP > 0  && HP<= 50) {Form = 4;}



		if (HP <= 0) {
			GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Experience = GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Experience + 20;
			Instantiate (Death, gameObject.transform.position, gameObject.transform.rotation);
			Destroy (gameObject);
		}

		if (!Run) {
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector2 (gameObject.GetComponent<Rigidbody> ().velocity.x * 0.95f, gameObject.GetComponent<Rigidbody> ().velocity.y * 0.95f);
			if(!IdleOnce || FormChange != Form)
			{
				IdleOnce = true;

			if (Form == 1) {gameObject.GetComponent<Animator>().runtimeAnimatorController = Idle1;}
			if (Form == 2) {gameObject.GetComponent<Animator>().runtimeAnimatorController = Idle2;}
			if (Form == 3) {gameObject.GetComponent<Animator>().runtimeAnimatorController = Idle3;}
			if (Form == 4) {gameObject.GetComponent<Animator>().runtimeAnimatorController = Idle4;}

			}


		}

		if (Run) {

			if(!RunOnce || FormChange != Form)
			{
			if (Form == 1) {gameObject.GetComponent<Animator>().runtimeAnimatorController = Run1;}
			if (Form == 2) {gameObject.GetComponent<Animator>().runtimeAnimatorController = Run2;}
			if (Form == 3) {gameObject.GetComponent<Animator>().runtimeAnimatorController = Run3;}
			if (Form == 4) {gameObject.GetComponent<Animator>().runtimeAnimatorController = Run4;}
			}

			gameObject.GetComponent<Rigidbody> ().AddExplosionForce(-20,GameObject.Find("Proto_Idle_1").gameObject.transform.position,10000);

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

		}




	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PlayerAttack") {

			HP = HP - GameObject.Find ("Main").GetComponent<DataHolder> ().PlayerDamage;

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
		RandomDelay = Random.Range (1, 3);
		Run = false;
		IdleOnce = false;
		StartCoroutine (RunIdle());

	}


}
