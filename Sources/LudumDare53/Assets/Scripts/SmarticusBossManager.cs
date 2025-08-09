using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmarticusBossManager : MonoBehaviour {

	public int HP;
	public bool Struck;
	public float StrikeTimer;

	public bool DoOnce;

	public GameObject LeftBoarder;
	public GameObject RightBoarder;

	public float BoarderDistance;

	public SpriteRenderer BossMidLine;
	public bool GoRight;
	public float Speed;
	public float CurrentSpeed;
	public float TopSpeed;

	public float FlipTimer;
	public SpriteRenderer SR;

	public GameObject Explosion;
	public GameObject BombBreak;

	public bool FightStarted;

	public GameObject[] Attacks;
	public float AttackTimer;

	public int phase;

	// Use this for initialization
	void Start () {

		Global.DataHolder.Player.PreBossNoRun = false;
		Global.DataHolder.BossMan = this;
	}
	
	// Update is called once per frame
	void Update () {




		FlipTimer += Time.deltaTime;
		if (FlipTimer > 0.5f) {

			SR.flipX = !SR.flipX;
			FlipTimer -= 0.5f;
		}


		if (Global.DataHolder.ScreenShiftMode || Global.DataHolder.GameIsPaused) {
			//dont move around
			AttackTimer = 2f;
			return;
		}

		if (Global.DataHolder.Player.TouchingGround > 0 && Global.DataHolder.Player.transform.position.y < -1) {
			FightStarted = true;
		}

		if (FightStarted) {
			AttackTimer += Time.deltaTime;

			if (AttackTimer > 2.5f) {

				if (Attacks [phase] != null) {
					Instantiate (Attacks [phase], transform.position, transform.rotation, transform.parent);
				}
				AttackTimer -= 2.5f;
			}


		} else {
			AttackTimer = 0;
		}

	}


	void FixedUpdate()
	{

		if (Global.DataHolder.ScreenShiftMode || Global.DataHolder.GameIsPaused) {
			//dont move around
			return;
		}
		BombBreak.SetActive (false);

		if (!GoRight) {

			if (CurrentSpeed > 0) {
				CurrentSpeed -= Speed * Time.fixedDeltaTime*Speed;
			}
			CurrentSpeed -= Speed * Time.fixedDeltaTime;
			if (CurrentSpeed < -TopSpeed) {
				CurrentSpeed = -TopSpeed;
			}

		} else {


			if (CurrentSpeed < 0) {
				CurrentSpeed += Speed * Time.fixedDeltaTime*Speed;
			}
			CurrentSpeed += Speed * Time.fixedDeltaTime;
			if (CurrentSpeed > TopSpeed) {
				CurrentSpeed = TopSpeed;
			}

		}

		transform.position += new Vector3 (CurrentSpeed, 0, 0) * Time.fixedDeltaTime;

		if (transform.position.x > RightBoarder.transform.position.x - 35 + TopSpeed/15f || transform.position.x > 18) {
			GoRight = false;
		}
		if (transform.position.x < LeftBoarder.transform.position.x + 35 - TopSpeed/15f || transform.position.x < -18) {
			GoRight = true;
		}


	}



	void OnTriggerEnter(Collider other)
	{
		if (DoOnce) {
			return;
		}

		if (other.tag == "Bomb") {
			BombBreak.SetActive (true);
			Global.DataHolder.BossCursor.LeftBoarder = LeftBoarder;
			Global.DataHolder.BossCursor.RightBoarder = RightBoarder;
			Global.DataHolder.BossCursor.BoarderDistance = BoarderDistance;
			Global.DataHolder.BossCursor.BossMidLine = BossMidLine;

			BoarderDistance -= 4;
			Speed += 5;
			TopSpeed += 5;
								phase++;
			if (BoarderDistance < 32) {

				//You win the game.
				Instantiate(Explosion,transform.position,transform.rotation,transform.parent);
				Global.DataHolder.WinGame = true;
				Global.DataHolder.BeginVictorySequence ();
				Destroy (gameObject);

			} else {

				Global.DataHolder.ActivateScreenShiftModeBossVersion ();

				Instantiate (Global.DataHolder.Player.ExplosionPrefab, other.transform.position, transform.rotation, transform.parent.parent);
				//SFX isnt here yet, so...
				if (Global.DataHolder.Player.BombExplosion_SFX != null) {
					Instantiate (Global.DataHolder.Player.BombExplosion_SFX, transform.position, transform.rotation);
				}

				Destroy (other.gameObject);

				DoOnce = true;
			}
		}
	}



}
