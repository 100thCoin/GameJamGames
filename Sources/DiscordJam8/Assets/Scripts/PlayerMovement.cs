using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


	public RuntimeAnimatorController RACIdle;
	public RuntimeAnimatorController RACRun;
	public RuntimeAnimatorController RACJump;
	public RuntimeAnimatorController RACCrouch;
	public Animator Anim;
	public SpriteRenderer SR;

	public bool CanJump;

	public GameObject TallCollider;
	public GameObject TallColliderSolid;

	public Rigidbody RB;

	public float JumpHeight;
	public float Gravity;
	public float VerticalSpeedLimit;
	public float RunSpeed;
	public float TopSpeed;
	public float CurrentSpeed;

	public int TouchingGround;
	public int TouchingRightWall;
	public int TouchingLeftWall;
	public int TouchingCeiling;

	public bool CutsceneLock;
	public bool Dead;

	public float BufferJump;

	public bool crouch;
	public int uncrouchtimer;

	public float crouchJumpTopSpeedDebuff;
	public float crouchJumpTopSpeedDebuff_Const = 6;

	public GameObject DeadParticles;
	public GameObject DeadSFX;
	public int IFrames;

	public int HeadInWall;

	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {

		if (Global.DataHolder.InLevelEditor) {
			transform.position = new Vector3 (transform.position.x, 0, -1500);
			return;
		} else if (transform.position.z < -400) {
			transform.position = new Vector3 (-21, -8, 0);
		}

		if ((Global.DataHolder.GameIsPaused || Global.DataHolder.PlayerTouchedAxe || !Global.DataHolder.InGame)) {
			return;
		} 

		if (Super.Dataholder.GetReboundInputDown (KeyCode.R)) {

			Global.DataHolder.ResetRoom ();
			transform.position = Global.DataHolder.SpawnPos;
			SR.flipX = true;
			Dead = false;
			CurrentSpeed = 0;
			RB.velocity = Vector3.zero;
			TouchingGround = 6;
			Anim.runtimeAnimatorController = RACIdle;
			RB.isKinematic = false;
		}

		if (CutsceneLock) {
			return;
		}
			




		if (Dead) {
			RB.isKinematic = true;
			transform.position = new Vector3 (0, 0, -600); //let's keep this away from everything else.
			return;
		}


		//code copied from LD53, just commented this part out

		/*
		Vector3 MousePos = Global.DataHolder.ForceCamera.ScreenToWorldPoint (Input.mousePosition) - new Vector3(0,150,0);
		Vector2 Displacement = -new Vector2 (transform.position.x - MousePos.x, transform.position.y - MousePos.y);

		float Dist = Displacement.magnitude;
		float ModDist = Mathf.Clamp01 (Dist)*4;
		Displacement = -new Vector2 (transform.position.x - MousePos.x, transform.position.y - MousePos.y).normalized*ModDist;

		DEBUGGERY.transform.localPosition = new Vector3 (Displacement.x, Displacement.y, 0);
		DEBUGGERY.transform.position = new Vector3 (Mathf.Round (DEBUGGERY.transform.position.x * 16) / 16f, Mathf.Round (DEBUGGERY.transform.position.y * 16) / 16f, 0);

		if (!NoBombsNow) {

			if (Super.Dataholder.GetReboundInputDown (KeyCode.Mouse0)) {

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
		*/

		BufferJump -= Time.deltaTime;


		if (CanJump && HeadInWall < 0) {

			if (Super.Dataholder.GetReboundInputDown (KeyCode.Space) || BufferJump >0) {
				//Jump.
				TouchingGround = -1;
				if (!crouch) {
					Anim.runtimeAnimatorController = RACJump;
				}
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
	void FixedUpdate()
	{
		IFrames--;
		if (Global.DataHolder.InLevelEditor) {
			RB.velocity = Vector3.zero;
			transform.position = new Vector3 (transform.position.x, 0, -1500);
			return;
		}
		if ((Global.DataHolder.GameIsPaused || Global.DataHolder.PlayerTouchedAxe || !Global.DataHolder.InGame)) {
			return;
		} 


		if (CanJump) {
			crouchJumpTopSpeedDebuff = 0;
			crouch = false;
		}
		if (Super.Dataholder.GetReboundInput (KeyCode.S) || crouch) {
			crouch = true;
			crouchJumpTopSpeedDebuff = crouchJumpTopSpeedDebuff_Const;
			uncrouchtimer = -1;
		}

		RB.velocity -= new Vector3 (0, Gravity * Time.fixedDeltaTime, 0);

		if (transform.position.x < Global.DataHolder.AxeObject.transform.position.x + 0.5f) {

		
			if (!crouch || !CanJump) {
				int dir = 0;
				if (Super.Dataholder.GetReboundInput (KeyCode.A)) {
					dir--;
				}
				if (Super.Dataholder.GetReboundInput (KeyCode.D)) {
					dir++;
				}

				if (dir <= -1) {
					SR.flipX = true;
					if (HeadInWall < 0) {
						if (CurrentSpeed > 0) {
							CurrentSpeed -= RunSpeed * Time.fixedDeltaTime;
						}
						CurrentSpeed -= RunSpeed * Time.fixedDeltaTime;
						if (CurrentSpeed < -(TopSpeed) + crouchJumpTopSpeedDebuff) {
							CurrentSpeed = -TopSpeed + crouchJumpTopSpeedDebuff;
						}
					
						if (TouchingGround >= 0) {						
							Anim.runtimeAnimatorController = RACRun;

						}
					}
					else {
						Anim.runtimeAnimatorController = RACIdle;
						CurrentSpeed *= 0.8f;

					}
				} else if (dir >= 1) {
					SR.flipX = false;
					if (HeadInWall < 0) {
						
						if (CurrentSpeed < 0) {
							CurrentSpeed += RunSpeed * Time.fixedDeltaTime;
						}
						CurrentSpeed += RunSpeed * Time.fixedDeltaTime;
						if (CurrentSpeed > TopSpeed - crouchJumpTopSpeedDebuff) {
							CurrentSpeed = TopSpeed - crouchJumpTopSpeedDebuff;
						}
					
						if (TouchingGround >= 0) {				
							Anim.runtimeAnimatorController = RACRun;
						}
					} else {
						Anim.runtimeAnimatorController = RACIdle;
						CurrentSpeed *= 0.8f;

					}
				} else {
					if (!crouch) {
						CurrentSpeed *= 0.8f;
					} else {
						CurrentSpeed *= 0.95f;
					}
					if (TouchingGround >= 0) {
						Anim.runtimeAnimatorController = RACIdle;

					}
				}
			} else {
				//crouching and on ground
				CurrentSpeed *= 0.98f;
				Anim.runtimeAnimatorController = RACCrouch;
			}
		} else {
			CurrentSpeed = 0;
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

		if (crouch) {
			TallCollider.SetActive (false);
			TallColliderSolid.SetActive (false);
		} else {
			TallCollider.SetActive (true);
			uncrouchtimer++;
		}
		if (uncrouchtimer >= 3) {
			TallColliderSolid.SetActive (true);

		}

		if (transform.position.x <= -26 && !(Global.DataHolder.InsideStringLevel || Global.DataHolder.StringLevelMode)) {
			Global.DataHolder.EnterLevelEditor ();
			transform.position = new Vector3 (0, 0, -1500);
		}

		HeadInWall--;
	}

	void OnTriggerStay(Collider other)
	{
		if (CutsceneLock) {
			return;
		}
		switch (other.tag) {
		case "Ground":
			{
				if (other.transform.position.y < transform.position.y - 2.5f && RB.velocity.y <= 0.5f) {
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
				if (!crouch && uncrouchtimer < 3) {
					uncrouchtimer = -1;
					transform.position += new Vector3(0.1f,0,0);
					HeadInWall = 2;
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
		if (other.tag == "Killzone" && IFrames < 0) {

			Instantiate (DeadParticles, transform.position,transform.rotation);
			if (DeadSFX != null) {
				Instantiate (DeadSFX);
			}
			transform.position = new Vector3 (-21, -8, 0);
			Instantiate (DeadParticles, new Vector3 (-21, -8, 0),transform.rotation);
			IFrames = 6;
			CurrentSpeed = 0;
			SR.flipX = false;
			RB.velocity = Vector3.zero;
		}
		if (other.tag == "Axe") {

			Global.DataHolder.PlayerTouchedAxe = true;
			BridgeCollapseSequence BCS = other.GetComponent<BridgeCollapseSequence> ();
			BCS.SR.enabled = false;
			BCS.Collapse = true;
			RB.velocity = Vector3.zero;
			Destroy (BCS.MusicHolder);
		}
	}



	void Land()
	{
		//dust particles
		if (crouch) {
			Anim.runtimeAnimatorController = RACCrouch;
		} else {
			Anim.runtimeAnimatorController = RACIdle;
		}
	}


}
