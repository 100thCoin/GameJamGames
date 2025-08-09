using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// movement constants
	[Header("movement constants")]
	public float TopSpeed;
	public float Accelleration;
	public float FastFallAccelleration;
	public float DirectionChangeAccellerationMultiplier = 4;
	public float WallSlideLimitMult;
	public float JumpHeight;
	public float Gravity;
	public float TopFastFallSpeedMult;
	public float Friction = 0.8f;
	public float AirFriction = 0.96f;

	[Header("variables")]
	public bool touchingCeiling = false;
	public bool touchingFloor = false;
	public bool touchingLeft = false;
	public bool touchingRight = false;
	public bool touchingMoon2 = false;

	public float FastFallTimer;
	public float WallClimbTimer;
	public float JumpStretchTimer;
	public float LandSquishTimer;
	public float ChangeDirTimer;
	public bool GetSpacebarDown;
	public float BufferJumpTimer;
	public float CoyoteTimer;
	public float DustTimer;
	public float ShootTimer;
	public float ShootFreq = 0.2f;
	public float ShootForce = 5;
	public float ShootSpread = 1;

	public bool InsideMoon2;
	public float Moon2CollisionCheckTimer;

	public bool PreventMovementForCutscenePurposes;
	public bool PreventMovementForCutscenePurposes_AndShooting;

	[Header("physics references")]
	public Rigidbody2D RB;
	public List<Collider2D> cols;
	public PlayerTriggers PCol_Head;
	public PlayerTriggers PCol_Feet;
	public PlayerTriggers PCol_Left;
	public PlayerTriggers PCol_Right;
	public PlayerTriggers PCol_Moon2;
	public PlayerTriggers PCol_LeftOOB;
	public PlayerTriggers PCol_RightOOB;

	public bool HasCoyoteJump;
	public SpriteRenderer SR;
	// Use this for initialization

	[Header("animations")]
	public Animator Anim;
	public RuntimeAnimatorController RAC_Idle;
	public RuntimeAnimatorController RAC_Run;
	public Sprite[] JumpAnim;
	public Sprite[] WallSlide;
	public GameObject Dust;
	public GameObject Dust_Moon2;
	public float DustInterval = 0.2f;
	public GameObject Dust_Land;
	public SpriteRenderer Cursor;
	public SpriteRenderer Bubble;

	[Header("Misc")]

	public GameObject GravityProjectile;
	public float DistFromMiddle;

	public bool Warping;
	public Transform[] WarpPos;
	public int WarpMode;
	public float WarpTimer;
	public Vector3 WarpStart;
	public bool ForceWarp;
	public GameObject CollisionHolder;
	public Collider2D Collid;


	public GameObject Tut2Voice;
	public bool PreventMovementForCutscenePurposes2;

	public float FireDelay;

	void Start () {
		
	}


	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space) && !PreventMovementForCutscenePurposes2) {
			GetSpacebarDown = true;
		}

		Vector3 MousePos = Input.mousePosition;
		MousePos = Global.Dataholder.MainCamera.ScreenToWorldPoint (MousePos);
		MousePos -= transform.position;
		MousePos = new Vector3 (MousePos.x, MousePos.y, 0);
		MousePos = MousePos.normalized*3;
		Cursor.transform.localPosition = MousePos;

		if (Input.GetKey (KeyCode.Mouse0) && !PreventMovementForCutscenePurposes_AndShooting && !Global.Dataholder.WorkshopOpen && !Global.Dataholder.Paused) {
			ShootTimer += Time.deltaTime;
			if (ShootTimer > ShootFreq) {
				ShootTimer -= ShootFreq;
				GameObject GravBall = Instantiate (GravityProjectile, transform.position + MousePos * 0.2f, transform.rotation);
				GravBall.GetComponent<Rigidbody2D> ().velocity = MousePos * ShootForce + new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),0).normalized*ShootSpread;
			}

		}
		if (Global.Dataholder.TutorialModeNoMoving) {
			FireDelay -= Time.deltaTime;
			if (FireDelay < 0) {
				PreventMovementForCutscenePurposes_AndShooting = false;
			}
		}

	}

	// Update is called once per frame
	void FixedUpdate () {

		if (InsideMoon2) {
			if (Global.Dataholder.TutorialModeNoMoving) {
				Global.Dataholder.VoiceQueue.Add (Tut2Voice);
			}
			Global.Dataholder.TutorialModeNoMoving = false;
		}
		if (transform.position.y > 415) {
			Global.Dataholder.TutorialModeNoClimbing = false;
		}

		//failsafe clears.

		if(PCol_Head.cols.Count > 0 && PCol_Head.cols[0] == null){ PCol_Head.cols.Clear ();}
		if(PCol_Feet.cols.Count > 0 && PCol_Feet.cols[0] == null){ PCol_Feet.cols.Clear ();}
		if(PCol_Right.cols.Count > 0 && PCol_Right.cols[0] == null){ PCol_Right.cols.Clear ();}
		if(PCol_Left.cols.Count > 0 && PCol_Left.cols[0] == null){ PCol_Left.cols.Clear ();}
		if(PCol_Moon2.cols.Count > 0 && PCol_Moon2.cols[0] == null){ PCol_Moon2.cols.Clear ();}
		if(PCol_LeftOOB.cols.Count > 0 && PCol_LeftOOB.cols[0] == null){ PCol_LeftOOB.cols.Clear ();}
		if(PCol_RightOOB.cols.Count > 0 && PCol_RightOOB.cols[0] == null){ PCol_RightOOB.cols.Clear ();}


		if (Warping) {
			Bubble.enabled = true;
			Collid.enabled = false;
			CollisionHolder.SetActive (false);
			RB.velocity = Vector2.zero;
			WarpTimer += Time.deltaTime;
			if (WarpTimer >= 0) {
				transform.position = new Vector3(
					DataHolder.TwoCurveLerp(WarpStart.x,WarpPos[WarpMode].transform.position.x,WarpTimer,3)
					,DataHolder.TwoCurveLerp(WarpStart.y,WarpPos[WarpMode].transform.position.y,WarpTimer,3)
					,0);
			}
			if (WarpTimer >= 3) {
				Warping = false;
				Collid.enabled = true;
				CollisionHolder.SetActive (true);
				RB.AddForce (new Vector2 (0, -0.1f));
				Global.Dataholder.TheHudButton.Inactive = false;
				Bubble.enabled = false;
			}
		}



		bool RightOOB = false;
		bool LeftOOB = false;

		touchingCeiling = false;
		touchingFloor = false;
		touchingLeft = false;
		touchingRight = false;
		touchingMoon2 = false;
		if (!PreventMovementForCutscenePurposes && !PreventMovementForCutscenePurposes_AndShooting && !Global.Dataholder.TutorialModeNoMoving && !PreventMovementForCutscenePurposes2) {
			
			if (Input.GetKey (KeyCode.Space)) {
				BufferJumpTimer += Time.deltaTime;
			} else {
				BufferJumpTimer = 0;
			}
		}
		if (cols.Count > 0) {
			
		}
		if (PCol_Feet.cols.Count > 0) {touchingFloor = true;}
		if (PCol_Head.cols.Count > 0) {touchingCeiling = true;}		
		if (PCol_Left.cols.Count > 0) {touchingLeft = true;}
		if (PCol_Right.cols.Count > 0) {touchingRight = true;}
		if (PCol_Moon2.cols.Count > 0) {touchingMoon2 = true;} 
		if (PCol_RightOOB.cols.Count > 0) {RightOOB = true;} 
		if (PCol_LeftOOB.cols.Count > 0) {LeftOOB = true;} 

		LandSquishTimer += Time.deltaTime*3;
		if (touchingFloor && RB.velocity.y < 1) {
			if (!HasCoyoteJump) {
				LandSquishTimer = 0;
				Instantiate (Dust_Land, transform.position - new Vector3 (0, 1, 0), transform.rotation);
			}
			HasCoyoteJump = true;
		} else if (touchingFloor && RB.velocity.y > 1){

		}

		if (HasCoyoteJump && !touchingFloor && RB.velocity.magnitude > 0.1f) {
			CoyoteTimer += Time.deltaTime;
		}
		if (!touchingFloor && CoyoteTimer > 0.2f) {
			CoyoteTimer = 0;
			HasCoyoteJump = false;
		}
		bool NoFriction = false;
		if (PCol_Feet.cols.Count > 0) {
			for (int i = 0; i < PCol_Feet.cols.Count; i++) {
				Rigidbody2D RB2D = PCol_Feet.cols [i].GetComponent<Rigidbody2D> ();
				if (RB2D == null  && PCol_Feet.cols [i].transform.parent != null) {
					RB2D = PCol_Feet.cols [i].transform.parent.GetComponent<Rigidbody2D> ();
				}
				if (RB2D != null && RB2D.bodyType != RigidbodyType2D.Static) {
					// somehow inherit the speed of this thing?
					NoFriction = true;
					RB.AddRelativeForce (RB2D.velocity);
				}
			}
		}


		if ((touchingFloor && touchingCeiling) || (touchingLeft && touchingRight)) {
			// handle getting crushed. (player doesn't die, just make one of or both objects intangible.
			// possibly add momentum of the object crushing you as you get tossed through the intangible object?
			bool DifferentObjects = false;
			for (int a = 0; a < PCol_Feet.cols.Count; a++) {
				for (int b = 0; b < PCol_Head.cols.Count; b++) {
					if (PCol_Feet.cols [a] != PCol_Head.cols [b]) {
						// also check if these have rigidbody
						bool hasRigidbody = false;
						if (PCol_Feet.cols [a].GetComponent<Rigidbody2D> ()) {
							if (PCol_Feet.cols [a].GetComponent<Rigidbody2D> ().bodyType != RigidbodyType2D.Static) {
								hasRigidbody = true;
							}
						} else {
							if (PCol_Feet.cols [a].transform.parent.GetComponent<Rigidbody2D> ()) {
								hasRigidbody = true;
							}
						}
						if (PCol_Head.cols [b].GetComponent<Rigidbody2D> ()) {
							float vel = PCol_Head.cols [b].GetComponent<Rigidbody2D> ().velocity.y;
							if (vel < -0.5f) {
								hasRigidbody = true;
							}
						} else {
							if (PCol_Head.cols [b].transform.parent.GetComponent<Rigidbody2D> ()) {
								hasRigidbody = true;
							}
						}
						if (hasRigidbody) {
							DifferentObjects = true;
							break;
						}
					}
				}
			}
			if (!DifferentObjects) {
				for (int a = 0; a < PCol_Left.cols.Count; a++) {
					for (int b = 0; b < PCol_Right.cols.Count; b++) {
						if (PCol_Left.cols [a] != PCol_Right.cols [b]) {
							// also check if these have rigidbody
							bool hasRigidbody = false;
							if (PCol_Left.cols [a].GetComponent<Rigidbody2D> ()) {
								hasRigidbody = true;
							} else {
								if (PCol_Left.cols [a].transform.parent.GetComponent<Rigidbody2D> ()) {
									hasRigidbody = true;
								}
							}
							if (PCol_Right.cols [b].GetComponent<Rigidbody2D> ()) {
								hasRigidbody = true;
							} else {
								if (PCol_Right.cols [b].transform.parent.GetComponent<Rigidbody2D> ()) {
									hasRigidbody = true;
								}
							}
							if (hasRigidbody) {
								DifferentObjects = true;
								break;
							} else {
								// screw it- just play it safe. 
								DifferentObjects = true;
							}
						}
					}
				}
			}
			if (DifferentObjects) {
				//we're getting crushed.
				gameObject.layer = 11;
				InsideMoon2 = true;
				Moon2CollisionCheckTimer = 0;
			}

		}
		bool wallsliding = false;
		// get player direction
		ChangeDirTimer += Time.deltaTime*6;
		int dir = 0;
		if (!PreventMovementForCutscenePurposes && !PreventMovementForCutscenePurposes_AndShooting && !Global.Dataholder.TutorialModeNoMoving && !PreventMovementForCutscenePurposes2) {
			
			if (Input.GetKey (KeyCode.A)) {
				dir--;
			}
			if (Input.GetKey (KeyCode.D)) {
				dir++;
			}
		}
		if (RightOOB && dir == 1) {
			dir = 0;
		}
		if (LeftOOB && dir == -1) {
			dir = 0;
		}

		if (!InsideMoon2) {
			if (dir == 1) {
				// move right
				if (!touchingLeft) {
					if (!SR.flipX) {
						ChangeDirTimer = 0;
					}
					SR.flipX = true;
					if (RB.velocity.x < 0) {
						RB.velocity = new Vector2 (RB.velocity.x + Accelleration * Time.deltaTime * DirectionChangeAccellerationMultiplier, RB.velocity.y);
					}
					RB.velocity = new Vector2 (RB.velocity.x + Accelleration * Time.deltaTime, RB.velocity.y);
				}
			} else if (dir == -1) {
				// move left
				if (!touchingRight) {
					if (SR.flipX) {
						ChangeDirTimer = 0;
					}
					SR.flipX = false;
					if (RB.velocity.x > 0) {
						RB.velocity = new Vector2 (RB.velocity.x - Accelleration * Time.deltaTime * DirectionChangeAccellerationMultiplier, RB.velocity.y);
					}
					RB.velocity = new Vector2 (RB.velocity.x - Accelleration * Time.deltaTime, RB.velocity.y);
				}
			} else {
				if (!NoFriction) {
					if (touchingFloor) {
						RB.velocity = new Vector2 (RB.velocity.x * Friction, RB.velocity.y);
					} else {
						RB.velocity = new Vector2 (RB.velocity.x * AirFriction, RB.velocity.y);
					}
				}
			}
		} else {
			if (dir == 1) {
				// move right
					if (!SR.flipX) {
						ChangeDirTimer = 0;
					}
					SR.flipX = true;
					if (RB.velocity.x < 0) {
						RB.velocity = new Vector2 (RB.velocity.x + Accelleration * Time.deltaTime * DirectionChangeAccellerationMultiplier, RB.velocity.y);
					}
					RB.velocity = new Vector2 (RB.velocity.x + Accelleration * Time.deltaTime, RB.velocity.y);
			} else if (dir == -1) {
				// move left
					if (SR.flipX) {
						ChangeDirTimer = 0;
					}
					SR.flipX = false;
					if (RB.velocity.x > 0) {
						RB.velocity = new Vector2 (RB.velocity.x - Accelleration * Time.deltaTime * DirectionChangeAccellerationMultiplier, RB.velocity.y);
					}
					RB.velocity = new Vector2 (RB.velocity.x - Accelleration * Time.deltaTime, RB.velocity.y);
			} else {
				RB.velocity = new Vector2 (RB.velocity.x * Friction, RB.velocity.y);
			}
		}
		if (Mathf.Abs (RB.velocity.x) > TopSpeed) {
			RB.velocity = new Vector2(Mathf.Sign (RB.velocity.x) * TopSpeed,RB.velocity.y);
		}

		int yDir = 0;
		if (!PreventMovementForCutscenePurposes && !PreventMovementForCutscenePurposes_AndShooting && !PreventMovementForCutscenePurposes2) {
			
			if (Input.GetKey (KeyCode.W)) {
				yDir++;
			}
			if (Input.GetKey (KeyCode.S)) {
				yDir--;
			}
		}

		if(Global.Dataholder.TutorialModeNoClimbing)
		{
			touchingLeft = false;
			touchingRight = false;
		}

		if (!InsideMoon2) {
			if ((touchingLeft || touchingRight) && yDir == 1) {
				// go up
				if (RB.velocity.y < 0) {
					RB.velocity = new Vector2 (RB.velocity.x, RB.velocity.y + Accelleration * Time.deltaTime * DirectionChangeAccellerationMultiplier);
				}
				RB.velocity = new Vector2 (RB.velocity.x, RB.velocity.y + Accelleration * Time.deltaTime);
				wallsliding = true;
			} else if (yDir == -1 && !HasCoyoteJump) {
				// go down
				if (RB.velocity.y > 0) {
					RB.velocity = new Vector2 (RB.velocity.x, RB.velocity.y - Accelleration * Time.deltaTime * DirectionChangeAccellerationMultiplier);
				}
				RB.velocity = new Vector2 (RB.velocity.x, RB.velocity.y - Accelleration * Time.deltaTime);
				if (touchingLeft || touchingRight)
					{
				wallsliding = true;
					}
			}
		} else {
			if (yDir == 1) {
				// go up
				if (RB.velocity.y < 0) {
					RB.velocity = new Vector2 (RB.velocity.x, RB.velocity.y + Accelleration * Time.deltaTime * DirectionChangeAccellerationMultiplier);
				}
				RB.velocity = new Vector2 (RB.velocity.x, RB.velocity.y + Accelleration * Time.deltaTime);
			} else if (yDir == -1) {
				// go down
				if (RB.velocity.y > 0) {
					RB.velocity = new Vector2 (RB.velocity.x, RB.velocity.y - Accelleration * Time.deltaTime * DirectionChangeAccellerationMultiplier);
				}
				RB.velocity = new Vector2 (RB.velocity.x, RB.velocity.y - Accelleration * Time.deltaTime);
			}
			else {
				RB.velocity = new Vector2 (RB.velocity.x , RB.velocity.y* Friction);
			}
		}
		JumpStretchTimer += Time.deltaTime * 3;

		if (!InsideMoon2) {
			if (!touchingLeft && !touchingRight) {
				// apply gravity
				RB.velocity = new Vector2 (RB.velocity.x, RB.velocity.y - Gravity * Time.deltaTime);
			} else {
				if (yDir == 0) {
					// apply gravity, but the top speed is reduced.
					RB.velocity = new Vector2 (RB.velocity.x, RB.velocity.y - Gravity * Time.deltaTime);
					if (RB.velocity.y < -TopSpeed * WallSlideLimitMult) {
						RB.velocity = new Vector2 (RB.velocity.x, RB.velocity.y - Accelleration * Time.deltaTime);
						if (RB.velocity.y < -TopSpeed * WallSlideLimitMult) {
							RB.velocity = new Vector2 (RB.velocity.x, -TopSpeed * WallSlideLimitMult);
						}
					}

				}
			}
		
			if ((Mathf.Abs (RB.velocity.y) > TopSpeed && yDir != -1) || (Mathf.Abs (RB.velocity.y) > TopSpeed * TopFastFallSpeedMult && yDir == -1)) {
				if (yDir == -1) {
					RB.velocity = new Vector2 (RB.velocity.x, Mathf.Sign (RB.velocity.y) * TopSpeed * TopFastFallSpeedMult);

				} else {
					RB.velocity = new Vector2 (RB.velocity.x, Mathf.Sign (RB.velocity.y) * TopSpeed);
				}
			}
			if (touchingFloor && (GetSpacebarDown || (BufferJumpTimer < 0.2f && BufferJumpTimer > 0f)) && HasCoyoteJump) {
				HasCoyoteJump = false;
				RB.velocity = new Vector2 (RB.velocity.x, JumpHeight);
				JumpStretchTimer = 0;
				LandSquishTimer = 1;
			}
	
			if ((yDir == -1 && !touchingFloor)) {
				FastFallTimer += Time.deltaTime * 2;
				if (FastFallTimer > 1) {
					FastFallTimer = 1;
				}
			} else {
				FastFallTimer *= 0.8f;
				if (FastFallTimer < 0.05f) {
					FastFallTimer = 0;
				}
			}

			if (((touchingLeft || touchingRight) && (yDir == -1 || yDir == 1))) {
				WallClimbTimer += Time.deltaTime * 2;
				if (WallClimbTimer > 1) {
					WallClimbTimer = 1;
				}
			} else {
				WallClimbTimer *= 0.8f;
				if (WallClimbTimer < 0.05f) {
					WallClimbTimer = 0;
				}
			}
		} else {
			JumpStretchTimer = 1;
			LandSquishTimer = 1;
			FastFallTimer = 0;
			WallClimbTimer = 0;
			if (Mathf.Abs (RB.velocity.y) > TopSpeed) {
				RB.velocity = new Vector2(RB.velocity.x,Mathf.Sign (RB.velocity.y) * TopSpeed);
			}
		}
		if (JumpStretchTimer > 1) {
			JumpStretchTimer = 1;
		}
		if (LandSquishTimer > 1) {
			LandSquishTimer = 1;
		}
		if (ChangeDirTimer > 1) {
			ChangeDirTimer = 1;
		}
		float fastFallStretch = -Mathf.Cos(FastFallTimer*Mathf.PI)*0.5f + 0.5f;
		float wallClimbStretch = -Mathf.Cos(WallClimbTimer*Mathf.PI)*0.5f + 0.5f;
		float jumpstretch = (JumpStretchTimer-1)*(JumpStretchTimer-1);
		float landSquish = (LandSquishTimer-1)*(LandSquishTimer-1);
		float changeDirSquish = -Mathf.Cos (ChangeDirTimer * Mathf.PI * 2) * 0.5f + 0.5f;
		SR.transform.localScale = new Vector3 (1 - (fastFallStretch * 0.25f) - (wallClimbStretch * 0.25f) - (jumpstretch*0.125f) + (landSquish*0.125f) - (changeDirSquish*0.125f), 1 + (fastFallStretch*0.25f)+(wallClimbStretch*0.25f)+(jumpstretch*0.25f)-(landSquish*0.25f), 1);
		SR.transform.localPosition = new Vector3 (0, (fastFallStretch*0.25f) - (landSquish*0.25f), 0);
		if (!InsideMoon2) {
			if (HasCoyoteJump) {
				if (!wallsliding) {
					if (dir == 0) {
						Anim.runtimeAnimatorController = RAC_Idle;
					} else {
						Anim.runtimeAnimatorController = RAC_Run;
					}
				} else {
					if (!touchingRight) {
						SR.flipX = false;
					} else {
						SR.flipX = true;
					}
					Anim.runtimeAnimatorController = null;
					if (RB.velocity.y > 2) {
						SR.sprite = WallSlide [0];
					} else if (RB.velocity.y > -2f) {
						SR.sprite = WallSlide [1];
					} else {
						SR.sprite = WallSlide [2];
					}

				}
			} else {
				// jumping
				if (!wallsliding) {
					Anim.runtimeAnimatorController = null;
					if (RB.velocity.y > 2) {
						SR.sprite = JumpAnim [0];
					} else if (RB.velocity.y > -0.2f) {
						SR.sprite = JumpAnim [1];
					} else if (RB.velocity.y > -4) {
						SR.sprite = JumpAnim [2];
					} else {
						SR.sprite = JumpAnim [3];
					}
				} else {
					if (!touchingRight) {
						SR.flipX = false;
					} else {
						SR.flipX = true;
					}
					Anim.runtimeAnimatorController = null;
					if (RB.velocity.y > 2) {
						SR.sprite = WallSlide [0];
					} else if (RB.velocity.y > -2f) {
						SR.sprite = WallSlide [1];
					} else {
						SR.sprite = WallSlide [2];
					}
				}

			}

			if (HasCoyoteJump) {
				if (dir != 0) {
					DustTimer += Time.deltaTime;
					if (DustTimer > DustInterval) {
						DustTimer -= DustInterval;
						GameObject D = Instantiate (Dust, transform.position - new Vector3 (0, 1f, 0), transform.rotation);
						if (!SR.flipX) {
							D.transform.localScale = new Vector3 (-1, 1, 1);
						}
					}

				} else {
					DustTimer = 0;
				}
			} else {
				DustTimer = 0;
			}
		} else {
			if (dir == 0) {
				Anim.runtimeAnimatorController = RAC_Idle;
			} else {
				Anim.runtimeAnimatorController = RAC_Run;
			}
			if (dir != 0) {
				DustTimer += Time.deltaTime;
				if (DustTimer > DustInterval) {
					DustTimer -= DustInterval;
					GameObject D = Instantiate (Dust_Moon2, transform.position + new Vector3(0,0,-1), transform.rotation);
					if (!SR.flipX) {
						D.transform.localScale = new Vector3 (-1, 1, 1);
					}
				}

			} else {
				DustTimer = 0;
			}
		}
		if (touchingMoon2) {
			// we can enter the ground by pressing shift, or maybe another button I don't know yet
			Moon2CollisionCheckTimer = 0;
			//TEST 
			if (!PreventMovementForCutscenePurposes && !PreventMovementForCutscenePurposes_AndShooting && !PreventMovementForCutscenePurposes2) {
				
				if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.E)) {
					InsideMoon2 = true;
					gameObject.layer = 11;
				}
			}
		} else {
			if (RB.velocity.magnitude > 0.1f) {
				Moon2CollisionCheckTimer += Time.deltaTime;
			}
		}
		if (Moon2CollisionCheckTimer > 0.125f) {
			InsideMoon2 = false;
			gameObject.layer = 9;
		}

		cols.Clear ();
		PCol_Head.cols.Clear ();
		PCol_Feet.cols.Clear ();
		PCol_Left.cols.Clear ();
		PCol_Right.cols.Clear ();
		PCol_Moon2.cols.Clear ();
		PCol_LeftOOB.cols.Clear ();
		PCol_RightOOB.cols.Clear ();
		GetSpacebarDown = false;

		DistFromMiddle = (transform.position - Global.Dataholder.MiddleOfTheMap.position).magnitude;
		if (!Warping && ((transform.position.y < -64f && DistFromMiddle > 180) || ForceWarp)) {
			// warp?
			ForceWarp = false;
			Warping = true;
			if (transform.position.y < -320) {
				WarpMode = 2;
			} else if (transform.position.y < -164){
				WarpMode = 1;
			}
			else
			{
				WarpMode = 0;
			}
			WarpTimer = -0.5f;
			WarpStart = transform.position;
		}

	}

	void OnTriggerStay2D(Collider2D other)
	{
		cols.Add (other);
	}

}
