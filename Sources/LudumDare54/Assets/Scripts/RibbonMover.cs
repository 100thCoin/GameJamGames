using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonMover : MonoBehaviour {

	public SpriteRenderer SR;
	public float CurrentSpeed;
	public float RunSpeed;
	public float TopSpeed;

	public Transform SHORTExtremaLeft;
	public Transform SHORTExtremaRight;
	public Transform LONGExtremaLeft;
	public Transform LONGExtremaRight;

	public bool DontShowShiftMSG;

	// Use this for initialization
	void Start () {
		
	}
	






































	public SpriteRenderer SHIFT_L;
	public SpriteRenderer SHIFT_R;



	public float BufferJump;
	public bool CanJump;
	public Rigidbody RB;
	public float JumpHeight;
	public Animator Anim;
	public RuntimeAnimatorController AnimJump;
	public RuntimeAnimatorController AnimIdle;
	public RuntimeAnimatorController AnimRun;
	public float Gravity;
	public float VerticalSpeedLimit;

	public Transform ExtremaLeft;
	public Transform ExtremaRight;
	public bool TouchingExtremaLeft;

	public bool TouchingExtrema;

	void Update () {

		if (Global.Dataholder.RibbonMan.PlayerIsInsideRibbon) {

			if (Global.Dataholder.TriStrip) {

				float pos = (transform.localPosition.x + Global.Dataholder.RibbonLengthInUnits/2f)/Global.Dataholder.RibbonLengthInUnits;
				Global.Dataholder.TwoDPlayerIsAtTValue = pos;
				if (pos > Global.Dataholder.TriStripProperties [0].StartT && pos < Global.Dataholder.TriStripProperties [0].StopT) {
					InteractWithStrip (Global.Dataholder.TriStripProperties [0].ID);
				}
				else if (pos > Global.Dataholder.TriStripProperties [1].StartT && pos < Global.Dataholder.TriStripProperties [1].StopT) {
					InteractWithStrip (Global.Dataholder.TriStripProperties [1].ID);

				}
				else if (pos > Global.Dataholder.TriStripProperties [2].StartT && pos < Global.Dataholder.TriStripProperties [2].StopT) {
					InteractWithStrip (Global.Dataholder.TriStripProperties [2].ID);

				}


			} else {

				float pos = (transform.localPosition.x + Global.Dataholder.RibbonLengthInUnits/2f)/Global.Dataholder.RibbonLengthInUnits;
				Global.Dataholder.TwoDPlayerIsAtTValue = pos;
				InteractWithStrip (Global.Dataholder.LoneStripProperties.ID);

				// single strip, always interacting

			}
			



			if (TouchingExtrema) {

				if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift)) {

					Global.Dataholder.TUT.enabled = false;

					Global.Dataholder.RibbonMan.PlayerIsInsideRibbon = false;
					Global.Dataholder.RibbonMan.MovingLeftEnd = TouchingExtremaLeft;
					if (TouchingExtremaLeft != Global.Dataholder.RibbonMan.RibbonIsOrientedLeft) {
						Global.Dataholder.RibbonMan.Player.transform.position = Global.Dataholder.RibbonMan.PointsTop [Global.Dataholder.RibbonMan.SliceCount - 1] - new Vector3 (0, 2, 0);
						Global.Dataholder.RibbonMan.PlayerLastPosition = Global.Dataholder.RibbonMan.Player.transform.position;
					} else {
						Global.Dataholder.RibbonMan.Player.transform.position = Global.Dataholder.RibbonMan.PointsTop [0] - new Vector3 (0, 2, 0);
						Global.Dataholder.RibbonMan.PlayerLastPosition = Global.Dataholder.RibbonMan.Player.transform.position;

					}

					Global.Dataholder.PlayerMov3D.ExitRibbonTimer = 0;
					Global.Dataholder.PlayerMov3D.Transition_ExitingRibbon = true;
					if (!(TouchingExtremaLeft != Global.Dataholder.RibbonMan.RibbonIsOrientedLeft)) {
						Global.Dataholder.PlayerMov3D.TransitionSPos = new Vector3 (Global.Dataholder.RibbonMan.PointsTop [8].x, Global.Dataholder.RibbonMan.PointsTop [8].y - 2.5f, Global.Dataholder.RibbonMan.PointsTop [8].z);
						Global.Dataholder.PlayerMov3D.TransitionDPos = new Vector3 (Global.Dataholder.RibbonMan.PointsTop [8].x, Global.Dataholder.RibbonMan.PointsTop [8].y - 3, Global.Dataholder.RibbonMan.PointsTop [8].z);
						Vector3 Normal = (new Vector3 (Global.Dataholder.RibbonMan.PointsTop [1].x, 0, Global.Dataholder.RibbonMan.PointsTop [1].y) - new Vector3 (Global.Dataholder.RibbonMan.PointsTop [0].x, 0, Global.Dataholder.RibbonMan.PointsTop [0].y)).normalized;
						Global.Dataholder.PlayerMov3D.TransitionDPos -= Normal*1.25f;
						float Rot = Mathf.Atan2 (Normal.z, Normal.x) * Mathf.Rad2Deg;
						Global.Dataholder.PlayerMov3D.SR.flipX = true;
						Global.Dataholder.PlayerMov3D.TransitionSRot = Rot;
						Global.Dataholder.PlayerMov3D.TransitionDRot = 0;
					} else {
						Global.Dataholder.PlayerMov3D.TransitionSPos = new Vector3 (Global.Dataholder.RibbonMan.PointsTop [Global.Dataholder.RibbonMan.SliceCount - 8].x, Global.Dataholder.RibbonMan.PointsTop [Global.Dataholder.RibbonMan.SliceCount - 8].y - 2.5f, Global.Dataholder.RibbonMan.PointsTop [Global.Dataholder.RibbonMan.SliceCount - 8].z);
						Global.Dataholder.PlayerMov3D.TransitionDPos = new Vector3 (Global.Dataholder.RibbonMan.PointsTop [Global.Dataholder.RibbonMan.SliceCount - 8].x, Global.Dataholder.RibbonMan.PointsTop [Global.Dataholder.RibbonMan.SliceCount - 8].y - 3, Global.Dataholder.RibbonMan.PointsTop [Global.Dataholder.RibbonMan.SliceCount - 8].z);
						Vector3 Normal = (new Vector3 (Global.Dataholder.RibbonMan.PointsTop [Global.Dataholder.RibbonMan.SliceCount - 2].x, 0, Global.Dataholder.RibbonMan.PointsTop [Global.Dataholder.RibbonMan.SliceCount - 2].y) - new Vector3 (Global.Dataholder.RibbonMan.PointsTop [Global.Dataholder.RibbonMan.SliceCount - 1].x, 0, Global.Dataholder.RibbonMan.PointsTop [Global.Dataholder.RibbonMan.SliceCount - 1].y)).normalized;
						Global.Dataholder.PlayerMov3D.TransitionDPos -= Normal*1.25f;
						float Rot = Mathf.Atan2 (Normal.z, Normal.x) * Mathf.Rad2Deg;
						Global.Dataholder.PlayerMov3D.SR.flipX = false;

						Global.Dataholder.PlayerMov3D.TransitionSRot = Rot;
						Global.Dataholder.PlayerMov3D.TransitionDRot = 0;
					}

					Global.Dataholder.CamMov.ThreeDHeight = Global.Dataholder.PlayerMov3D.TransitionDPos.y - 1;




				}
				if (!DontShowShiftMSG) {
					Global.Dataholder.TUT.sprite = Global.Dataholder.TUTshift;

					if (TouchingExtremaLeft) {
						SHIFT_L.enabled = false;
						SHIFT_R.enabled = true;
					} else {
						SHIFT_L.enabled = true;
						SHIFT_R.enabled = false;
					}
				} else {
					SHIFT_L.enabled = false;

					SHIFT_R.enabled = false;
				}
			}
			else
			{

				SHIFT_L.enabled = false;

				SHIFT_R.enabled = false;
				//DontShowShiftMSG = false;
			}

			BufferJump -= Time.deltaTime;


			if (CanJump) {

				if (Input.GetKeyDown (KeyCode.Space) || BufferJump > 0) {
					//Jump.
					TouchingGround = -1;
					Anim.runtimeAnimatorController = AnimJump;
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

			transform.position = new Vector3 (0, -20, 0);

		}
		if (transform.position.x < ExtremaLeft.transform.position.x) {
			transform.position = new Vector3 (ExtremaLeft.transform.position.x, transform.position.y, transform.position.z);
		}
		if (transform.position.x > ExtremaRight.transform.position.x) {
			transform.position = new Vector3 (ExtremaRight.transform.position.x, transform.position.y, transform.position.z);
		}

	}


	public int TouchingGround;
	public int TouchingRightWall;
	public int TouchingLeftWall;
	public int TouchingCeiling;


	void FixedUpdate()
	{


		if (Global.Dataholder.RibbonMan.PlayerIsInsideRibbon) {
			
			
			RB.velocity -= new Vector3 (0, Gravity * Time.fixedDeltaTime, 0);

			if (RB.velocity.y < -VerticalSpeedLimit) {
				RB.velocity = new Vector3 (RB.velocity.x, -VerticalSpeedLimit, 0);
			}




			int dir = 0;
			if ((!Global.Dataholder.CamMov.FlipCameraForReduceMotionSickness && Input.GetKey (KeyCode.A)) || (Global.Dataholder.CamMov.FlipCameraForReduceMotionSickness && Input.GetKey (KeyCode.D))) {
				dir--;
			}
			if ((!Global.Dataholder.CamMov.FlipCameraForReduceMotionSickness && Input.GetKey (KeyCode.D)) || (Global.Dataholder.CamMov.FlipCameraForReduceMotionSickness && Input.GetKey (KeyCode.A))) {
				dir++;
			}

			if (dir <= -1) {
				SR.flipX = false;
				if (CurrentSpeed > 0) {
					CurrentSpeed -= RunSpeed * Time.fixedDeltaTime;
				}
				CurrentSpeed -= RunSpeed * Time.fixedDeltaTime;
				if (CurrentSpeed < -TopSpeed) {
					CurrentSpeed = -TopSpeed;
				}
				if (TouchingGround >= 0) {						
					Anim.runtimeAnimatorController = AnimRun;

				}
			} else if (dir >= 1) {
				SR.flipX = true;
				if (CurrentSpeed < 0) {
					CurrentSpeed += RunSpeed * Time.fixedDeltaTime;
				}
				CurrentSpeed += RunSpeed * Time.fixedDeltaTime;
				if (CurrentSpeed > TopSpeed) {
					CurrentSpeed = TopSpeed;
				}
				if (TouchingGround >= 0) {				
					Anim.runtimeAnimatorController = AnimRun;
				}
			} else {
				CurrentSpeed *= 0.8f;
				if (TouchingGround >= 0) {
					Anim.runtimeAnimatorController = AnimIdle;

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

			if (transform.position.x < ExtremaLeft.transform.position.x) {
				transform.position = new Vector3 (ExtremaLeft.transform.position.x, transform.position.y, transform.position.z);
			}
			if (transform.position.x > ExtremaRight.transform.position.x) {
				transform.position = new Vector3 (ExtremaRight.transform.position.x, transform.position.y, transform.position.z);
			}

			if (Mathf.Abs (transform.position.x - ExtremaLeft.transform.position.x) < 0.25f) {
				TouchingExtrema = true;
				TouchingExtremaLeft = true;
			}
			else if (Mathf.Abs (transform.position.x - ExtremaRight.transform.position.x) < 0.25f) {
				TouchingExtrema = true;
				TouchingExtremaLeft = false;
			} else {
				TouchingExtrema = false;
			}


		} else {
			transform.position = new Vector3 (0, -20, 0);

		}

	}

	void OnTriggerStay(Collider other)
	{

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


	void Land()
	{
		//dust particles

		Anim.runtimeAnimatorController = AnimIdle;

	}


	void InteractWithStrip(int ID)
	{

		Global.Dataholder.TwoDPlayerIsTouchingID = ID;

		if (ID == 1) {

			if (TouchingGround >= 0) {
				//bounce!

				TouchingGround = -1;
				Anim.runtimeAnimatorController = AnimJump;
				RB.velocity = new Vector3 (RB.velocity.x, JumpHeight*0.5f, 0);


			}





		}



	}


}
