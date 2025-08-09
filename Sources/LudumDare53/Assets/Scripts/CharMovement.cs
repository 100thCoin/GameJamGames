using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour {

	public float JumpHeight;
	public float Gravity;
	public float VerticalSpeedLimit;
	public float RunSpeed;
	public float TopSpeed;
	public float CurrentSpeed;

	public RuntimeAnimatorController AnimIdle;
	public RuntimeAnimatorController AnimRun;
	public RuntimeAnimatorController AnimJump;
	public RuntimeAnimatorController AnimWIdle;
	public RuntimeAnimatorController AnimWRun;
	public RuntimeAnimatorController AnimWJump;
	public Animator Anim;
	public Animator WAnim;
	public SpriteRenderer SR;
	public SpriteRenderer WSR;

	public bool CanJump;

	public Rigidbody RB;

	public GameObject BombPrefab;
	public GameObject CurrentBomb;
	public GameObject ExplosionPrefab;
	public GameObject BombExplosion_SFX;

	public bool NoBombsNow;

	public GameObject DEBUGGERY;

	public GameObject DeathExplosion;
	public GameObject DeathExplosion_SFX;
	public bool Dead;

	public bool CutsceneLock;

	public bool LevelStart;

	public bool PreBossNoRun;

	public Vector3 PauseVel;
	public bool UnpauseOnce;


	// Use this for initialization
	void Start () {
		
	}

	public float BufferJump;

	public Collider[] Cols;
	public void DisableCollision()
	{
		foreach (Collider C in Cols) {
			C.enabled = false;
		}
	}

	public void EnableCollision()
	{
		foreach (Collider C in Cols) {
			C.enabled = true;
		}
	}
	// Update is called once per frame
	void Update () {

		if ((Global.DataHolder.GameIsPaused || Global.DataHolder.WinGame || !Global.DataHolder.InGame)) {
			return;
		} 

		if (Input.GetKeyDown (KeyCode.R) && Global.DataHolder.LevelID != 7) {

			Global.DataHolder.StartFlash ();
			Global.DataHolder.ResetRoom ();
			transform.position = Global.DataHolder.SpawnPos;
			SR.flipX = true;
			WSR.flipX = true;
			Dead = false;
			CurrentSpeed = 0;
			if (CurrentBomb != null) {
				Destroy (CurrentBomb.gameObject);
			}
			RB.velocity = Vector3.zero;
			TouchingGround = 6;
			Anim.runtimeAnimatorController = AnimIdle;
			WAnim.runtimeAnimatorController = AnimWIdle;
			RB.isKinematic = false;
		}

		if (CutsceneLock) {
			return;
		}

		if (LevelStart) {
			return;
		}




		if (Dead) {
			RB.isKinematic = true;
			transform.position = new Vector3 (0, 0, -600); //let's keep this away from everything else.
			DEBUGGERY.transform.position = new Vector3 (0, 0, -600);
			Global.DataHolder.DeadMSG.SetActive (true);
			return;
		}

		Global.DataHolder.DeadMSG.SetActive (false);

		//figure out the bomb arc

		Vector3 MousePos = Global.DataHolder.ForceCamera.ScreenToWorldPoint (Input.mousePosition) - new Vector3(0,150,0);
		Vector2 Displacement = -new Vector2 (transform.position.x - MousePos.x, transform.position.y - MousePos.y);

		float Dist = Displacement.magnitude;
		float ModDist = Mathf.Clamp01 (Dist)*4;
		Displacement = -new Vector2 (transform.position.x - MousePos.x, transform.position.y - MousePos.y).normalized*ModDist;

		DEBUGGERY.transform.localPosition = new Vector3 (Displacement.x, Displacement.y, 0);
		DEBUGGERY.transform.position = new Vector3 (Mathf.Round (DEBUGGERY.transform.position.x * 16) / 16f, Mathf.Round (DEBUGGERY.transform.position.y * 16) / 16f, 0);

		if (!NoBombsNow) {

			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				if (CurrentBomb != null) {

					Instantiate (ExplosionPrefab, CurrentBomb.transform.position, transform.rotation, transform.parent.parent);
					//SFX isnt here yet, so...
					if (BombExplosion_SFX != null) {
						Instantiate (BombExplosion_SFX, transform.position, transform.rotation);
					}
					Destroy (CurrentBomb.gameObject);
				}

				CurrentBomb = Instantiate (BombPrefab, transform.position, transform.rotation, transform.parent);
				CurrentBomb.GetComponent<Rigidbody> ().velocity = Displacement*10;

			}

		}


		BufferJump -= Time.deltaTime;


		if (CanJump) {

			if (Input.GetKeyDown (KeyCode.Space) || BufferJump >0) {
				//Jump.
				TouchingGround = -1;
				Anim.runtimeAnimatorController = AnimJump;
				WAnim.runtimeAnimatorController = AnimWJump;
				RB.velocity = new Vector3 (RB.velocity.x, JumpHeight, 0);

			}


		} else if (Input.GetKeyDown (KeyCode.Space)) {
			BufferJump = 0.2f;

		} else if (Input.GetKeyUp (KeyCode.Space)) {
			// make jump shorter. unbuffer jump.
			if (RB.velocity.y > 0) {
				RB.velocity = new Vector3 (RB.velocity.x, RB.velocity.y * 0.5f, 0);
			}
			BufferJump = -1;
		}



	}


	public int TouchingGround;
	public int TouchingRightWall;
	public int TouchingLeftWall;
	public int TouchingCeiling;


	void FixedUpdate()
	{
		if ((Global.DataHolder.GameIsPaused || Global.DataHolder.WinGame || !Global.DataHolder.InGame)) {
			if (UnpauseOnce) {
				PauseVel = RB.velocity;
				RB.isKinematic = true;
			}
			UnpauseOnce = false;
			return;
		} else {
			if (!UnpauseOnce) {
				RB.isKinematic = false;
				RB.velocity = PauseVel;
			}
			UnpauseOnce = true;
		}

		if (CutsceneLock) {
			return;
		}
		RB.velocity -= new Vector3 (0, Gravity*Time.fixedDeltaTime, 0);

		if (RB.velocity.y < -VerticalSpeedLimit) {
			RB.velocity = new Vector3 (RB.velocity.x, -VerticalSpeedLimit, 0);
		}

		if (LevelStart) {
			TouchingGround--;
			TouchingRightWall--;
			TouchingLeftWall--;
			TouchingCeiling--;
			return;
		}

		if (PreBossNoRun) {
			return;
		}


		int dir = 0;
		if (Input.GetKey (KeyCode.A)) {
			dir--;
		}
		if (Input.GetKey (KeyCode.D)) {
			dir++;
		}

		if (dir <= -1) {
			SR.flipX = false;
			WSR.flipX = false;
			if (CurrentSpeed > 0) {
				CurrentSpeed -= RunSpeed * Time.fixedDeltaTime;
			}
			CurrentSpeed -= RunSpeed * Time.fixedDeltaTime;
			if (CurrentSpeed < -TopSpeed) {
				CurrentSpeed = -TopSpeed;
			}
			if (TouchingGround >= 0) {						
				Anim.runtimeAnimatorController = AnimRun;
				WAnim.runtimeAnimatorController = AnimWRun;

			}
		} else if (dir >= 1) {
			SR.flipX = true;
			WSR.flipX = true;
			if (CurrentSpeed < 0) {
				CurrentSpeed += RunSpeed * Time.fixedDeltaTime;
			}
			CurrentSpeed += RunSpeed * Time.fixedDeltaTime;
			if (CurrentSpeed > TopSpeed) {
				CurrentSpeed = TopSpeed;
			}
			if (TouchingGround >= 0) {				
				Anim.runtimeAnimatorController = AnimRun;
				WAnim.runtimeAnimatorController = AnimWRun;
			}
		} else {
			CurrentSpeed *= 0.8f;
			if (TouchingGround >= 0) {
				Anim.runtimeAnimatorController = AnimIdle;
				WAnim.runtimeAnimatorController = AnimWIdle;

			}
		}

		RB.velocity = new Vector3 (CurrentSpeed, RB.velocity.y, 0);

		TouchingGround--;
		TouchingRightWall--;
		TouchingLeftWall--;
		TouchingCeiling--;

		if (TouchingGround < 0) {

			CanJump = false;

		} else {
			CanJump = true;
		}


	}

	void OnTriggerStay(Collider other)
	{
		if (CutsceneLock) {
			return;
		}
		switch (other.tag) {
		case "Ground":
			{
				if (other.transform.position.y < transform.position.y - 1) {
					if (TouchingGround < 0) {
						Land ();
					}
					TouchingGround = 6; //for coyote time
					if (RB.velocity.y < 0) {
						RB.velocity = new Vector3 (RB.velocity.x, 0, 0);
					}
					CanJump = true;
				}
			}
			break;
		case "LeftWall":
			{
				if (CurrentSpeed < 0) {
					CurrentSpeed = 0;
				}
				TouchingLeftWall = 2;
				if (RB.velocity.x < 0) {
					RB.velocity = new Vector3 (0, RB.velocity.y, 0);
				}
			}
			break;
		case "RightWall":
			{
				if (CurrentSpeed > 0) {
					CurrentSpeed = 0;
				}
				TouchingRightWall = 2;
				if (RB.velocity.x > 0) {
					RB.velocity = new Vector3 (0, RB.velocity.y, 0);
				}
			}
			break;
		case "Ceiling":
			{
				TouchingCeiling = 2;
				if (RB.velocity.y > 0) {
					RB.velocity = new Vector3 (RB.velocity.x, 0, 0);
				}
			}
			break;

		}




	}

	void OnTriggerEnter(Collider other)
	{
		if (CutsceneLock) {
			return;
		}
		if (other.tag == "Killzone") {
			if (Dead) {
				return;
			}
			Dead = true;
			Instantiate (DeathExplosion, transform.position, transform.rotation, transform.parent.parent);
			//SFX isnt here yet, so...
			if (DeathExplosion_SFX != null) {
				Instantiate (DeathExplosion_SFX, transform.position, transform.rotation, transform.parent.parent);
			}


			transform.position = new Vector3 (0, 0, -600);

		}

	}



	void Land()
	{
		//dust particles

		Anim.runtimeAnimatorController = AnimIdle;
		WAnim.runtimeAnimatorController = AnimIdle;
		LevelStart = false;

	}


}
