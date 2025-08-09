using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementThreeD : MonoBehaviour {

	public Transform HiddenPlayer;
	public bool HoldingRibbon;

	public SpriteRenderer SR;
	public float CurrentSpeedX;
	public float CurrentSpeedY;

	public float RunSpeed;
	public float TopSpeed;

	public Vector2 Movamajig;

	public bool Transition_EnteringRibbon;
	public float EnterRibbonTimer;
	public bool Transition_ExitingRibbon;
	public float ExitRibbonTimer;

	public Vector3 TransitionSPos;
	public Vector3 TransitionDPos;
	public float TransitionSRot;
	public float TransitionDRot;

	public bool GotBounced;

	public bool CanOpenQMenu;

	public Collider[] Cols;

	public GameObject SFX_BooWomp;

	// Use this for initialization
	void Start () {

	}











































	public float BufferJump;
	public bool CanJump;
	public Rigidbody RB;
	public float JumpHeight;
	public Animator Anim;
	public RuntimeAnimatorController AnimJump;
	public RuntimeAnimatorController AnimIdle;
	public RuntimeAnimatorController AnimRun;

	public RuntimeAnimatorController AnimJumpArm;
	public RuntimeAnimatorController AnimIdleArm;
	public RuntimeAnimatorController AnimRunArm;

	public float Gravity;
	public float VerticalSpeedLimit;

	public GameObject RibbonCustomizeMenu;

	void Update () {

		if (Global.Dataholder.GameEnded) {
			return;
		}

		if (!CanOpenQMenu) {

			if (Input.GetKeyUp (KeyCode.Q)) {
				CanOpenQMenu = true;
			}

		}

		if (!Global.Dataholder.RibbonMan.PlayerIsInsideRibbon) {





			RB.isKinematic = false;

			if (Transition_EnteringRibbon || Transition_ExitingRibbon) {
			
				if (Transition_EnteringRibbon) {

					EnterRibbonTimer += Time.deltaTime;

					if (EnterRibbonTimer > 1) {
						EnterRibbonTimer = 1;
					}

					Vector3 DestPos = new Vector3(
						DataHolder.SinLerp(TransitionSPos.x,TransitionDPos.x,EnterRibbonTimer,1),
						DataHolder.SinLerp(TransitionSPos.y,TransitionDPos.y,EnterRibbonTimer,1),
						DataHolder.SinLerp(TransitionSPos.z,TransitionDPos.z,EnterRibbonTimer,1)
					);

					float DestRot = DataHolder.SinLerp (TransitionSRot, TransitionDRot, EnterRibbonTimer, 1);

					transform.position = DestPos;
					transform.eulerAngles = new Vector3(0,DestRot,0);
					Global.Dataholder.PlayerMov2D.SHIFT_L.enabled = false;
					Global.Dataholder.PlayerMov2D.SHIFT_R.enabled = false;
					if (EnterRibbonTimer >= 1) {
						EnterRibbonTimer = 0;
						Transition_EnteringRibbon = false;
						Global.Dataholder.RibbonMan.PlayerIsInsideRibbon = true;
						Global.Dataholder.PlayerMov2D.transform.position = (Global.Dataholder.RibbonMan.MovingLeftEnd ? Global.Dataholder.PlayerMov2D.ExtremaLeft.transform.position : Global.Dataholder.PlayerMov2D.ExtremaRight.transform.position) - new Vector3(0,0.5f,0);

						Global.Dataholder.PlayerMov2D.SR.flipX = Global.Dataholder.RibbonMan.MovingLeftEnd;
						transform.position = new Vector3 (0, -20, 0);
						Global.Dataholder.PlayerMov2D.DontShowShiftMSG = true;

					}

				}
				if (Transition_ExitingRibbon) {

					ExitRibbonTimer += Time.deltaTime;

					if (ExitRibbonTimer > 1) {
						ExitRibbonTimer = 1;
					}

					Vector3 DestPos = new Vector3(
						DataHolder.SinLerp(TransitionSPos.x,TransitionDPos.x,ExitRibbonTimer,1),
						DataHolder.SinLerp(TransitionSPos.y,TransitionDPos.y,ExitRibbonTimer,1),
						DataHolder.SinLerp(TransitionSPos.z,TransitionDPos.z,ExitRibbonTimer,1)
					);

					float DestRot = DataHolder.SinLerp (TransitionSRot, TransitionDRot, ExitRibbonTimer, 1);

					transform.position = DestPos;
					transform.eulerAngles = new Vector3(0,DestRot,0);

					if (ExitRibbonTimer >= 1) {
						ExitRibbonTimer = 0;
						Transition_ExitingRibbon = false;

						int m = 0;
						while (m < Cols.Length) {
							Cols [m].enabled = true;
							m++;
						}

					}

				}









				return;
			} else {

				if (Global.Dataholder.RibbonCollectibles [1] >= Global.Dataholder.ImplementedRibbonsPerType [1] || Global.Dataholder.DEBUGMODE) {
					if (Global.Dataholder.CanPause) {
						if (CanOpenQMenu && Input.GetKeyDown (KeyCode.Q)) {
							Global.Dataholder.CanPause = false;
							RibbonCustomizeMenu.SetActive (true);

						}
					}
				}





				if (HoldingRibbon && Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift)) {
					EnterRibbonTimer = 0;
					Transition_EnteringRibbon = true;
					TransitionSPos = transform.position;
					TransitionSRot = transform.eulerAngles.y;
					Global.Dataholder.TimesEnteredTheRibbon++;
					int m = 0;
					while (m < Cols.Length) {
						Cols [m].enabled = false;
						m++;
					}
					if (Global.Dataholder.RibbonMan.RibbonIsOrientedLeft) {
						
						TransitionDPos = new Vector3 (Global.Dataholder.RibbonMan.PointsTop [16].x, Global.Dataholder.RibbonMan.PointsTop [16].y - 2.5f, Global.Dataholder.RibbonMan.PointsTop [16].z);

						Vector3 Normal = (new Vector3 (Global.Dataholder.RibbonMan.PointsTop [1].x, 0, Global.Dataholder.RibbonMan.PointsTop [1].z) - new Vector3 (Global.Dataholder.RibbonMan.PointsTop [0].x, 0, Global.Dataholder.RibbonMan.PointsTop [0].z)).normalized;
						Normal = new Vector3 (Normal.z, 0, Normal.x);

						float Rot = Mathf.Atan2 (Normal.z, Normal.x) * Mathf.Rad2Deg - 90;
						TransitionDRot = Rot;
						if (Normal.z > 0) {
							Global.Dataholder.CamMov.FlipCameraForReduceMotionSickness = false;
						} else {
							Global.Dataholder.CamMov.FlipCameraForReduceMotionSickness = true;
						}
					} else {

						TransitionDPos = new Vector3 (Global.Dataholder.RibbonMan.PointsTop [16].x, Global.Dataholder.RibbonMan.PointsTop [16].y - 2.5f, Global.Dataholder.RibbonMan.PointsTop [16].z);

						Vector3 Normal = (new Vector3 (Global.Dataholder.RibbonMan.PointsTop [1].x, 0, Global.Dataholder.RibbonMan.PointsTop [1].z) - new Vector3 (Global.Dataholder.RibbonMan.PointsTop [0].x, 0, Global.Dataholder.RibbonMan.PointsTop [0].z)).normalized;
						Normal = new Vector3 (Normal.z, 0, Normal.x);

						float Rot = Mathf.Atan2 (Normal.z, Normal.x) * Mathf.Rad2Deg - 90;
						TransitionDRot = Rot;

						if (Normal.z > 0) {
							Global.Dataholder.CamMov.FlipCameraForReduceMotionSickness = true;
						} else {
							Global.Dataholder.CamMov.FlipCameraForReduceMotionSickness = false;
						}


					}

					CurrentSpeedX = 0;
					CurrentSpeedY = 0;

				}


			}

			BufferJump -= Time.deltaTime;

			float totaldist = (HiddenPlayer.transform.position - (transform.position + new Vector3 (0, 1, 0))).magnitude;

			if (totaldist > 1f) {

				float Tval = 1 - (1f / totaldist);
				Vector3 NewPosition = new Vector3 (
					                      Mathf.Lerp (HiddenPlayer.transform.position.x, transform.position.x, Tval),
					                      Mathf.Lerp (HiddenPlayer.transform.position.y, transform.position.y + 1, Tval),
					                      Mathf.Lerp (HiddenPlayer.transform.position.z, transform.position.z, Tval));

				HiddenPlayer.transform.position = NewPosition;
			}


			if (CanJump) {

				if (Input.GetKeyDown (KeyCode.Space) || BufferJump > 0) {
					//Jump.
					TouchingGround = -1;
					Anim.runtimeAnimatorController = HoldingRibbon ? AnimJumpArm : AnimJump;
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
		} else {
			transform.position = new Vector3 (0, -2000, 0);
			RB.isKinematic = true;

		}


	}


	public int TouchingGround;
	public int TouchingRightWall;
	public int TouchingLeftWall;
	public int TouchingCeiling;


	void FixedUpdate()
	{
		if (Global.Dataholder.GameEnded) {
			RB.velocity = Vector3.zero;
			return;
		}

		if (!Global.Dataholder.RibbonMan.PlayerIsInsideRibbon) {

			if (Transition_EnteringRibbon || Transition_ExitingRibbon) {
				return;
			}
				
			RB.velocity -= new Vector3 (0, Gravity*Time.fixedDeltaTime, 0);

			if (RB.velocity.y < -VerticalSpeedLimit) {
				RB.velocity = new Vector3 (RB.velocity.x, -VerticalSpeedLimit, 0);
			}




			Movamajig = Vector2.zero;
			if(Input.GetKey(KeyCode.A))
			{
				Movamajig += new Vector2(-1,0);
			}
			if(Input.GetKey(KeyCode.D))
			{
				Movamajig += new Vector2(1,0);
			}
			if(Input.GetKey(KeyCode.W))
			{
				Movamajig += new Vector2(0,1);
			}
			if(Input.GetKey(KeyCode.S))
			{
				Movamajig += new Vector2(0,-1);
			}
			Movamajig = Movamajig.normalized;

			if (false) {





			} else {
				// free movement
				if (Movamajig.x <= -0.01f) {
					SR.flipX = false;
					if (CurrentSpeedX > 0) {
						CurrentSpeedX -= RunSpeed * Time.fixedDeltaTime * Mathf.Abs(Movamajig.x);
					}
					CurrentSpeedX -= RunSpeed * Time.fixedDeltaTime * Mathf.Abs(Movamajig.x);
					if (CurrentSpeedX < -TopSpeed) {
						CurrentSpeedX = -TopSpeed;
					}
					if (TouchingGround >= 0) {						
						Anim.runtimeAnimatorController = HoldingRibbon ? AnimRunArm : AnimRun;

					}
				} else if (Movamajig.x >= 0.01f) {
					SR.flipX = true;
					if (CurrentSpeedX < 0) {
						CurrentSpeedX += RunSpeed * Time.fixedDeltaTime * Mathf.Abs(Movamajig.x);
					}
					CurrentSpeedX += RunSpeed * Time.fixedDeltaTime * Mathf.Abs(Movamajig.x);
					if (CurrentSpeedX > TopSpeed) {
						CurrentSpeedX = TopSpeed;
					}
					if (TouchingGround >= 0) {				
						Anim.runtimeAnimatorController = HoldingRibbon ? AnimRunArm : AnimRun;
					}
				} else {
					CurrentSpeedX *= 0.8f;
				}

				if (Movamajig.y <= -0.01f) {
					if (CurrentSpeedY > 0) {
						CurrentSpeedY -= RunSpeed * Time.fixedDeltaTime * Mathf.Abs(Movamajig.y);
					}
					CurrentSpeedY -= RunSpeed * Time.fixedDeltaTime * Mathf.Abs(Movamajig.y);
					if (CurrentSpeedY < -TopSpeed) {
						CurrentSpeedY = -TopSpeed;
					}
					if (TouchingGround >= 0) {						
						Anim.runtimeAnimatorController = HoldingRibbon ? AnimRunArm : AnimRun;

					}
				} else if (Movamajig.y >= 0.01f) {
					if (CurrentSpeedY < 0) {
						CurrentSpeedY += RunSpeed * Time.fixedDeltaTime * Mathf.Abs(Movamajig.y);
					}
					CurrentSpeedY += RunSpeed * Time.fixedDeltaTime * Mathf.Abs(Movamajig.y);
					if (CurrentSpeedY > TopSpeed) {
						CurrentSpeedY = TopSpeed;
					}
					if (TouchingGround >= 0) {				
						Anim.runtimeAnimatorController = HoldingRibbon ? AnimRunArm : AnimRun;
					}
				} else {
					CurrentSpeedY *= 0.8f;
				}

				if (Movamajig.x == 0 && Movamajig.y == 0 && TouchingGround > 0) {
					Anim.runtimeAnimatorController = HoldingRibbon ? AnimIdleArm : AnimIdle;

				}

			}

			if (new Vector2 (CurrentSpeedX, CurrentSpeedY).magnitude > TopSpeed) {
				CurrentSpeedX = (new Vector2 (CurrentSpeedX, CurrentSpeedY).normalized * TopSpeed).x;
				CurrentSpeedY = (new Vector2 (CurrentSpeedX, CurrentSpeedY).normalized * TopSpeed).y;
			}


			RB.velocity = new Vector3 (CurrentSpeedX, RB.velocity.y, CurrentSpeedY);

			TouchingGround--;
			TouchingRightWall--;
			TouchingLeftWall--;
			TouchingCeiling--;

			if (TouchingGround < 0) {

				CanJump = false;
				Anim.runtimeAnimatorController = HoldingRibbon ? AnimJumpArm : AnimJump;


			} else {
				CanJump = true;
			}

			if (transform.position.z < -24) {

				transform.position = new Vector3 (transform.position.x, transform.position.y, -24);


			}




		} else {
			transform.position = new Vector3 (0, -2000, 0);

		}
	}

	void OnTriggerStay(Collider other)
	{

		switch (other.tag) {
		case "Ground":
			{
				if (other.transform.position.y < transform.position.y - 1 && RB.velocity.y <= 0.1f) {
					if (TouchingGround < 0) {
						Land ();
					}
					TouchingGround = 8; //for coyote time
					if (RB.velocity.y < 0) {
						RB.velocity = new Vector3 (RB.velocity.x, 0, 0);
					}
					CanJump = true;
					GotBounced = false;
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
		case "Bouncy":
			{
				if(!GotBounced)
				{
					CanJump = false;
					GotBounced = true;
					RB.velocity = new Vector3 (RB.velocity.x, JumpHeight*1.5f, 0);
					Instantiate (SFX_BooWomp, transform.position, transform.rotation);

				}
			}
			break;
		}




	}


	void Land()
	{
		//dust particles

		Anim.runtimeAnimatorController = HoldingRibbon ? AnimIdleArm : AnimIdle;
		Global.Dataholder.CamMov.ThreeDHeight = transform.position.y - 1;

	}


}
