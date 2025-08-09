using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// move the player around.

public class PlayerController : MonoBehaviour {

	public float jumpHeight;
	public bool grounded;
	public float LastGroundedTheta;
	public CameraController CamMov;

	public Transform Vis;

	public Vector2 walkingVel;
	public Vector2 inAirVel;

	public Rigidbody RB;

	public int bufferedJump;

	public float MovementSpeed;

	public int JumpFrames;
	public int MaxJumpFrames = 32;

	public PlayerGroundDetection PGD;

	public Animator Anim;
	public RuntimeAnimatorController RAC_Idle;
	public RuntimeAnimatorController RAC_Run;
	public RuntimeAnimatorController RAC_Jump;

	public bool AtSpawn;

	public GameObject TouchingDynamiteCutscene;
	public GameObject TouchingDynamiteCutscene2;

	public GameObject TouchingEnemyCutscene;
	public GameObject FireballCutscene;

	public GameObject NecromancerCutscene;

	public int Collectibles;

	public bool InCutscene;

	public Vector3 CurrentVector;

	public float SpeedboostTimer;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Global.Dataholder.Level == 4)
		{
			Collectibles = 0;
		}
		if (InCutscene) {
			return;
		}
		if (AtSpawn) {
			if (Global.Dataholder.Level == 3) {
				if (new Vector3 (transform.position.x, 0, transform.position.z + 28).magnitude > 4) {
					AtSpawn = false;
					FireballCutscene.SetActive (true);
					InCutscene = true;
					Global.Dataholder.MainCamera.SetActive (false);
					transform.position = new Vector3 (0, -50000, 0);
					Global.Dataholder.HeroMov.transform.position = new Vector3 (0, -50000, 0);

					Global.Dataholder.HideLevel ();
					CamMov.enabled = false;
					int j = 0;
					while (j < UnloadLevels.Length) {
						UnloadLevels [j].SetActive (false);
						j++;
					}
				}
			} else {
				if (new Vector3 (transform.position.x, 0, transform.position.z + 28).magnitude > 2) {
					AtSpawn = false;
				}
			}
		}
		float speedmult = 1;
		if (SpeedboostTimer >0) {
			speedmult = 1 + SpeedboostTimer * 2;
		}


		if (grounded) {			
			if (Super.Dataholder.GetReboundInputDown (KeyCode.Space) || bufferedJump > 0) {
				//jump
				Anim.runtimeAnimatorController = RAC_Jump;
				if (bufferedJump > 0) {
					//we probably buffered this jump
					float facingTheta =( CamMov.Cam.transform.eulerAngles.y +180) * Mathf.Deg2Rad;
					LastGroundedTheta = facingTheta;
					Vector2 mov = new Vector2 (0, 0);
					if (Super.Dataholder.GetReboundInput (KeyCode.W)) {
						mov.y += 1;
					}
					if (Super.Dataholder.GetReboundInput (KeyCode.S)) {
						mov.y -= 1;
					}
					if (Super.Dataholder.GetReboundInput (KeyCode.A)) {
						mov.x -= 1;
					}
					if (Super.Dataholder.GetReboundInput (KeyCode.D)) {
						mov.x += 1;
					}
					if (mov.magnitude != 0) {
						// we are walking.
						mov = mov.normalized;

						walkingVel = new Vector2 (mov.x, mov.y).normalized * MovementSpeed* speedmult;
					}
					// instant snap to direction top speed.
				}


				grounded = false;
				RB.velocity = new Vector3 (RB.velocity.x, jumpHeight, RB.velocity.z);
				bufferedJump = 0;
				JumpFrames = 0;
				PGD.gameObject.SetActive (false);
			}
		} else {
			if (JumpFrames >= MaxJumpFrames) {
				if (Super.Dataholder.GetReboundInputDown (KeyCode.Space)) {
					bufferedJump = 10;
				}
				PGD.gameObject.SetActive (true);
			}
		}
	}


	void FixedUpdate()
	{
		if (InCutscene) {
			return;
		}
		float walkAccel = MovementSpeed * 5;
		float facingTheta =( CamMov.Cam.transform.eulerAngles.y +180) * Mathf.Deg2Rad;

		Vector2 mov = new Vector2 (0, 0);
		if (Super.Dataholder.GetReboundInput (KeyCode.W)) {
			mov.y += 1;
		}
		if (Super.Dataholder.GetReboundInput (KeyCode.S)) {
			mov.y -= 1;
		}
		if (Super.Dataholder.GetReboundInput (KeyCode.A)) {
			mov.x -= 1;
		}
		if (Super.Dataholder.GetReboundInput (KeyCode.D)) {
			mov.x += 1;
		}
		float speedmult = 1;
		if (SpeedboostTimer >0) {
			speedmult = 1 + SpeedboostTimer * 2;

			SpeedboostTimer -= Time.fixedDeltaTime;

		}

		if (grounded) {
			inAirVel = Vector2.zero;
			RB.velocity = new Vector3 (RB.velocity.x * 0.85f, 0, RB.velocity.z * 0.85f);
			if (Mathf.Abs (RB.velocity.x) < 0.05f) {
				RB.velocity = new Vector3(0,0,RB.velocity.z);
			}
			if (Mathf.Abs (RB.velocity.y) < 0.05f) {
				RB.velocity = new Vector3(RB.velocity.x,0,0);
			}

			LastGroundedTheta = facingTheta;

			if (Mathf.Sign(walkingVel.x) != Mathf.Sign(mov.x)) {
				//dampen X
				walkingVel.x *= 0.8f;
				if (Mathf.Abs (walkingVel.x) < 0.05f) {
					walkingVel.x = 0;
				}
			} 
			if (Mathf.Sign(walkingVel.y) != Mathf.Sign(mov.y)) {
				//dampen Y
				walkingVel.y *= 0.8f;
				if (Mathf.Abs (walkingVel.y) < 0.05f) {
					walkingVel.y = 0;
				}
			}

			if (mov.magnitude != 0) {
				// we are walking.
				mov = mov.normalized;
				Anim.runtimeAnimatorController = RAC_Run;

				walkingVel += new Vector2 (walkAccel * mov.x, walkAccel * mov.y) * Time.fixedDeltaTime;
			} else {
				Anim.runtimeAnimatorController = RAC_Idle;

			}
			if (mov.x == 0) {
				//dampen X
				walkingVel.x *= 0.8f;
				if (Mathf.Abs (walkingVel.x) < 0.05f) {
					walkingVel.x = 0;
				}
			} 
			if (mov.y == 0) {
				//dampen Y
				walkingVel.y *= 0.8f;
				if (Mathf.Abs (walkingVel.y) < 0.05f) {
					walkingVel.y = 0;
				}
			}
			if (walkingVel.magnitude > MovementSpeed) {
				walkingVel = walkingVel.normalized * MovementSpeed;
			}

			Vector3 TransformPos = new Vector3 (-Mathf.Sin(facingTheta)*walkingVel.y - Mathf.Cos(facingTheta)*walkingVel.x,0,Mathf.Sin(facingTheta)*walkingVel.x-Mathf.Cos(facingTheta)*walkingVel.y);
			CurrentVector = TransformPos;

			RB.MovePosition (RB.position + TransformPos * Time.fixedDeltaTime * speedmult);

			bufferedJump--;

		} else {

			if (mov.magnitude != 0) {
				// we are walking.
				mov = mov.normalized;

				inAirVel += new Vector2 (walkAccel * mov.x, walkAccel * mov.y) * Time.fixedDeltaTime;
			}
			if (mov.x == 0) {
				//dampen X
				inAirVel.x *= 0.8f;
				if (Mathf.Abs (inAirVel.x) < 0.05f) {
					inAirVel.x = 0;
				}
			} 
			if (mov.y == 0) {
				//dampen Y
				inAirVel.y *= 0.8f;
				if (Mathf.Abs (inAirVel.y) < 0.05f) {
					inAirVel.y = 0;
				}
			}
			if (inAirVel.magnitude > MovementSpeed) {
				inAirVel = inAirVel.normalized * MovementSpeed * speedmult;
			}

			Vector3 TransformPos = new Vector3 (-Mathf.Sin(facingTheta)*inAirVel.y - Mathf.Cos(facingTheta)*inAirVel.x,0,Mathf.Sin(facingTheta)*inAirVel.x-Mathf.Cos(facingTheta)*inAirVel.y)*0.33f + new Vector3 (-Mathf.Sin(LastGroundedTheta)*walkingVel.y - Mathf.Cos(LastGroundedTheta)*walkingVel.x,0,Mathf.Sin(LastGroundedTheta)*walkingVel.x-Mathf.Cos(LastGroundedTheta)*walkingVel.y)*0.66f;

			CurrentVector = TransformPos;

			RB.MovePosition (RB.position + TransformPos * Time.fixedDeltaTime);


			if (JumpFrames < MaxJumpFrames && Super.Dataholder.GetReboundInput (KeyCode.Space)) {
				//jump

				//grounded = false;
				RB.velocity = new Vector3 (RB.velocity.x, jumpHeight, RB.velocity.z);
			}
			JumpFrames++;

			if (!Super.Dataholder.GetReboundInput (KeyCode.Space)) {
				if (JumpFrames < MaxJumpFrames) {
					JumpFrames = MaxJumpFrames;
					//small hop
					RB.velocity= new Vector3(RB.velocity.x,RB.velocity.y*0.5f,RB.velocity.z);
				}

			}


		}




	}

	public GameObject[] UnloadLevels;

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Dynamite") && !InCutscene) {
			InCutscene = true;
			Global.Dataholder.MainCamera.SetActive (false);
			if (Global.Dataholder.Level == 0) {
				TouchingDynamiteCutscene.SetActive (true);
			} else {
				TouchingDynamiteCutscene2.SetActive (true);
			}
			transform.position = new Vector3 (0, -50000, 0);
			Global.Dataholder.HideLevel ();
			CamMov.enabled = false;
			int j = 0;
			while (j < UnloadLevels.Length) {
				UnloadLevels [j].SetActive (false);
				j++;
			}
		}

		if(other.CompareTag("Collectible"))
		{
			Collectibles++;
			Destroy (other.gameObject);
		}

		if(other.CompareTag("Enemy"))
		{
			if (Collectibles >= 6) {
				InCutscene = true;
				Global.Dataholder.MainCamera.SetActive (false);
				TouchingEnemyCutscene.SetActive (true);
				transform.position = new Vector3 (0, -50000, 0);
				CamMov.enabled = false;
				int j = 0;
				while (j < UnloadLevels.Length) {
					UnloadLevels [j].SetActive (false);
					j++;
				}
				Global.Dataholder.HideLevel ();

			} else {
				Global.Dataholder.Pmov.SpeedboostTimer = 1.5f;
				other.GetComponent<HeroSmackObject> ().DoEnemyPlayer ();
				if (!grounded) {
					inAirVel *= 3;
				}
			}
		}
		if(other.CompareTag("End"))
		{
				InCutscene = true;
				Global.Dataholder.MainCamera.SetActive (false);
			NecromancerCutscene.SetActive (true);
			Global.Dataholder.TimerActive = false;
				transform.position = new Vector3 (0, -50000, 0);
				CamMov.enabled = false;
				int j = 0;
				while (j < UnloadLevels.Length) {
					UnloadLevels [j].SetActive (false);
					j++;
				}
				Global.Dataholder.HideLevel ();

		}
	}


	void LateUpdate()
	{
		if (InCutscene) {
			return;
		}

		//Vis.eulerAngles = new Vector3 (0, CamMov.transform.eulerAngles.y + 180, 0);
		// this happens in the move camera script.
	}

}
