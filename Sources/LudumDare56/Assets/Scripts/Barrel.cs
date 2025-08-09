using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour {

	public float speed;
	public float JumpHeight;

	public float TopSpeed;
	public float currentSpeed;
	public float currentYVel;
	public float JumpTime;
	public float gravity;
	public float MaxFallSpeed;
	public float MaxJumpSpeed;

	public float JumpDuration;


	public PlayerHitbox PHit_Floor;
	public PlayerHitbox PHit_Wall_L;
	public PlayerHitbox PHit_Wall_R;
	public PlayerHitbox PHit_Ceiling;
	public PlayerHitbox PHit_Shoulder_L;
	public PlayerHitbox PHit_Shoulder_R;

	public float coyoteTime;
	public bool HasCoyoteJump;

	public float bufferingJump;
	public bool BufferShortHop;



	public bool InsideBirdJumpZone;
	public bool InsideFrogZone;
	public bool InsideStage;
	public bool InsideVoid;
	public bool InsideClearStage;

	public float FrogHeight;
	public float FrogTime;

	public bool BetweenLevels;

	public bool BarrelGoLeft;


	// Use this for initialization
	void Start () {

	}

	public bool isjumping;
	public bool onGround;

	// Update is called once per frame
	void Update () {
		if (BetweenLevels) {
			return;
		}


	}

	void FixedUpdate()
	{
		if (BetweenLevels) {
			return;
		}
		float RealJumpDuration = JumpDuration + Global.Dataholder.CountHarmonyFrogs () * 0.08f;
		if (InsideVoid) {
			PHit_Floor.Active = false;
			PHit_Ceiling.Active = false;

		}
		int dir = 0;
		if (!BarrelGoLeft) {
			dir++;
		}
		if (BarrelGoLeft) {
			dir--;
		}
		if (dir != 0) {

			if (dir == 1) {
				currentSpeed += speed * Time.fixedDeltaTime;
			} else {
				currentSpeed -= speed * Time.fixedDeltaTime;
			}

			if (dir == 1 && currentSpeed < 0) {
				currentSpeed += speed * Time.fixedDeltaTime;
			} else if (dir == -1 && currentSpeed > 0){
				currentSpeed -= speed * Time.fixedDeltaTime;
			}

	
		} else {

		

			currentSpeed *= 0.5f;

		}

		if (currentSpeed > TopSpeed) {
			currentSpeed = TopSpeed;
		}
		if (currentSpeed < -TopSpeed) {
			currentSpeed = -TopSpeed;
		}

		if (currentYVel > 0) {
			FrogHeight = transform.position.y;
		}

		JumpTime += Time.fixedDeltaTime;
		coyoteTime -= Time.deltaTime;

		if (coyoteTime < -0.05f) {
			HasCoyoteJump = false;

		}



	
		if (!isjumping && coyoteTime <= 0 && !PHit_Floor.Active && onGround) {
			onGround = false;

		}

		if ((!onGround && (JumpTime > RealJumpDuration || !isjumping) && coyoteTime <= 0) && FrogTime <= 0) {
			if (InsideBirdJumpZone) {
				if (currentYVel < 0) {
					currentYVel -= Time.fixedDeltaTime * gravity * 0.25f;		

				} else {
					currentYVel -= Time.fixedDeltaTime * gravity;		

				}

			} else {
				if (currentYVel < 0 && coyoteTime > -0.2f) {
					currentYVel -= Time.fixedDeltaTime * gravity*0.25f;		
				} else {
					currentYVel -= Time.fixedDeltaTime * gravity;		

				}
			}

		}

		if (PHit_Floor.Active) {

			coyoteTime = 0.125f;
			currentYVel = 0;
			onGround = true;
			transform.position = new Vector3(transform.position.x, PHit_Floor.Col.bounds.center.y + PHit_Floor.Col.bounds.extents.y+ 0.25f, transform.position.z);
			HasCoyoteJump = true;
			FrogHeight = transform.position.y;
		}

		if (PHit_Ceiling.Active && currentYVel >= 0.01f) {
			currentYVel = 0;
			//transform.position = new Vector3(transform.position.x, PHit_Ceiling.Col.bounds.center.y - PHit_Ceiling.Col.bounds.extents.y -1.5f, transform.position.z);
		}


		if (currentYVel < -0.4f) {
			currentYVel = -0.4f;
		}
		if (currentYVel > MaxJumpSpeed) {
			//currentYVel = MaxJumpSpeed;
		}

		if (PHit_Wall_L.Active && currentSpeed < 0) {
			currentSpeed = Mathf.Abs(currentSpeed);
			BarrelGoLeft = false;
		}
		if (PHit_Wall_R.Active && currentSpeed > 0) {
			currentSpeed = -Mathf.Abs(currentSpeed);
			BarrelGoLeft = true;

		}

		transform.position += new Vector3 (currentSpeed, currentYVel, 0);

		FrogTime -= Time.deltaTime;

		PHit_Floor.Active = false;
		PHit_Wall_L.Active = false;
		PHit_Wall_R.Active = false;
		PHit_Ceiling.Active = false;
		PHit_Shoulder_L.Active = false;
		PHit_Shoulder_R.Active = false;

		InsideBirdJumpZone = false;
		InsideFrogZone = false;
		InsideStage = false;
		InsideVoid = false;
		InsideClearStage = false;

	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag ("Barrel")) {
			Instantiate (Global.Dataholder.CircleSmall, transform.position, transform.rotation);
			Instantiate (Global.Dataholder.CircleSmall, other.transform.position, transform.rotation);
			Destroy (other.gameObject);
			Destroy(gameObject);
		}
	}

}

