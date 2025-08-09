using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp_AI : MonoBehaviour {

	public float MaxHP;
	public float HP;

	public GameObject Death;

	public GameObject Bar;

	public RuntimeAnimatorController Idle1;

	public Sprite Jump;

	public float JumpProgress;

	public float Distance;

	public Vector2 Goal;
	public Vector2 StartPos;

	public bool Run;
	public bool RunOnce;
	public bool IdleOnce;

	public float speed;

	public float RandomDelay;

	public bool Acidic;

	public GameObject EnemyZap;

	public GameObject Shadow;

	// Use this for initialization
	void Start () {

		RandomDelay = Random.Range (0, 2);
		StartCoroutine (RunIdle ());

		gameObject.name = "Lamp";

	}

	// Update is called once per frame
	void Update () {


		if (Acidic) {
			HP = HP - Time.deltaTime * 12;
			Bar.gameObject.GetComponent<HPBar> ().precent = HP / MaxHP * 100;

		}




		if (HP <= 0) {
			GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Experience = GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Experience + 25;
			Instantiate (Death, gameObject.transform.position, gameObject.transform.rotation);
			Destroy (gameObject);
		}

		if (!Run) {
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector2 (gameObject.GetComponent<Rigidbody> ().velocity.x * 0.95f, gameObject.GetComponent<Rigidbody> ().velocity.y * 0.95f);
			if(!IdleOnce)
			{
				IdleOnce = true;

				gameObject.GetComponent<Animator>().runtimeAnimatorController = Idle1;


			}
			gameObject.name = "Lamp";


		}

		if (Run) {

			if(!RunOnce)
			{
				gameObject.name = "JumpLamp";
				RunOnce = true;
				gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
				gameObject.GetComponent<SpriteRenderer> ().sprite = Jump;
				Goal = new Vector3 (GameObject.Find ("Proto_Idle_1").gameObject.transform.position.x , GameObject.Find ("Proto_Idle_1").gameObject.transform.position.y, 0);

				if( gameObject.transform.position.x +(Goal.x - gameObject.transform.position.x) <gameObject.transform.position.x -10){Goal = new Vector2 ( gameObject.transform.position.x-10, Goal.y);}
				if( gameObject.transform.position.x +(Goal.x - gameObject.transform.position.x) >gameObject.transform.position.x +10){Goal = new Vector2 (gameObject.transform.position.x +10, Goal.y);}
				if( gameObject.transform.position.y +(Goal.y - gameObject.transform.position.y) <gameObject.transform.position.y -10){Goal = new Vector2 (Goal.x,gameObject.transform.position.y -10);}
				if( gameObject.transform.position.y +(Goal.y - gameObject.transform.position.y) >gameObject.transform.position.y +10){Goal = new Vector2 (Goal.x,gameObject.transform.position.y + 10);}
				StartPos = gameObject.transform.position;
				JumpProgress = 0;
			
			}

			if (JumpProgress < 1) {

				JumpProgress = JumpProgress + Time.deltaTime;

				gameObject.transform.position = new Vector3 (
					((Goal.x - StartPos.x) / 1) * JumpProgress + StartPos.x,
					((Goal.y - StartPos.y) / 1) * JumpProgress + StartPos.y + (-40 *(Mathf.Pow(JumpProgress,2)) + 40 * JumpProgress), -2);

				Shadow.gameObject.transform.position = new Vector3 (
					((Goal.x - StartPos.x) / 1) * JumpProgress + StartPos.x,
					((Goal.y - StartPos.y) / 1) * JumpProgress + StartPos.y - 0.3f, 2);
				



			}
			if (JumpProgress >= 2) {
				Shadow.gameObject.transform.position = new Vector3 (Goal.x, Goal.y - 0.3f, 2);
				gameObject.transform.position = Goal;
				Run = false;
			}





			if (gameObject.transform.position.x - Goal.x > 0) {

				gameObject.GetComponent<SpriteRenderer> ().flipX = false;

			}

			if (gameObject.transform.position.x - Goal.x <= 0) {

				gameObject.GetComponent<SpriteRenderer> ().flipX = true;

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
		RandomDelay = 1;
		Run = true;
		RunOnce = false;
		yield return new WaitForSeconds (RandomDelay);
		Instantiate (EnemyZap, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y-1,2), gameObject.transform.rotation);
		RandomDelay = Random.Range (4, 8);
		Run = false;
		IdleOnce = false;
		StartCoroutine (RunIdle());

	}


}
