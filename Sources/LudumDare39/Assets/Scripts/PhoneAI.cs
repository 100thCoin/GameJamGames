using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneAI : MonoBehaviour {

	public float MaxHP;
	public float HP;

	public GameObject Death;

	public GameObject Bar;

	public RuntimeAnimatorController Idle1;

	public RuntimeAnimatorController Run1;


	public bool Run;
	public bool RunOnce;
	public bool IdleOnce;

	public float speed;

	public float RandomDelay;

	public bool Acidic;

	public GameObject EnemyWep;

	public GameObject EnemyZap;
	public GameObject EnemyZapObject;

	public float EnemyZapTimer;

	// Use this for initialization
	void Start () {

		RandomDelay = 2;
		StartCoroutine (EnemyZappit ());

		gameObject.name = "Phone";

	}

	// Update is called once per frame
	void Update () {

		EnemyWep.gameObject.transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 (gameObject.transform.position.y - GameObject.Find("Proto_Idle_1").gameObject.transform.position.y, gameObject.transform.position.x - GameObject.Find("Proto_Idle_1").gameObject.transform.position.x) * Mathf.Rad2Deg + 90);


		if (Acidic) {
			HP = HP - Time.deltaTime * 12;
			Bar.gameObject.GetComponent<HPBar> ().precent = HP / MaxHP * 100;

		}




		if (HP <= 0) {
			GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Experience = GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Experience + 50;
			Instantiate (Death, gameObject.transform.position, gameObject.transform.rotation);
			Destroy (gameObject);
		}

		if (!Run) {
//			gameObject.GetComponent<Rigidbody> ().velocity = new Vector2 (gameObject.GetComponent<Rigidbody> ().velocity.x * 0.95f, gameObject.GetComponent<Rigidbody> ().velocity.y * 0.95f);
			if(!IdleOnce)
			{
				IdleOnce = true;

				gameObject.GetComponent<Animator>().runtimeAnimatorController = Idle1;


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

	IEnumerator EnemyZappit()
	{
		yield return new WaitForSeconds (EnemyZapTimer);

			EnemyZapObject = Instantiate (EnemyZap, gameObject.transform.position, gameObject.transform.rotation, EnemyWep.transform)as GameObject;
			EnemyZapObject.gameObject.transform.localPosition = new Vector3 (0, 2, -1);
			EnemyZapObject.gameObject.transform.localEulerAngles = new Vector3 (0, 0, 0);
			EnemyZapObject.gameObject.transform.parent = null;


		StartCoroutine (EnemyZappit());

	}

}
