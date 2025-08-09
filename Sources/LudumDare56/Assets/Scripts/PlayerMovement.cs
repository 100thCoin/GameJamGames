using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

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

	public RuntimeAnimatorController RAC_Idle;
	public RuntimeAnimatorController RAC_Run;
	public Animator Anim;
	public SpriteRenderer SR;

	public Sprite Sprite_Jump_Down;
	public Sprite Sprite_Jump_Mid;
	public Sprite Sprite_Jump_Up;
	public Sprite Sprite_Idle;

	public bool InsideBirdJumpZone;
	public bool InsideFrogZone;
	public bool InsideStage;
	public bool InsideVoid;
	public bool InsideClearStage;

	public float FrogHeight;
	public float FrogTime;

	public bool BetweenLevels;

	// Use this for initialization
	void Start () {
		
	}

	public bool isjumping;
	public bool onGround;

	// Update is called once per frame
	void Update () {
		Global.Dataholder.SpeedrunTime += Time.deltaTime;
		if (BetweenLevels) {
			return;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {

			Global.Dataholder.Paused = true;
			Global.Dataholder.Pause ();
		}


		if (transform.position.y < -10) {
			Global.Dataholder.ResetLevel ();
		}

		if (Super.Dataholder.GetReboundInputDown (KeyCode.R)) {

			Global.Dataholder.ResetLevel ();

		}

		bufferingJump -= Time.deltaTime;
		if (Super.Dataholder.GetReboundInputDown (KeyCode.Space) && !(HasCoyoteJump || onGround)) {
			bufferingJump = 0.25f;
		}
		if(Super.Dataholder.GetReboundInputDown (KeyCode.Space))
		{
			BufferShortHop = false;
		}
		if ((Super.Dataholder.GetReboundInputDown (KeyCode.Space) || bufferingJump > 0) && (HasCoyoteJump || onGround || InsideBirdJumpZone)) {

			currentYVel = JumpHeight;
			JumpTime = 0;
			onGround = false;
			PHit_Floor.Active = false;
			isjumping = true;
			coyoteTime = -1;
			HasCoyoteJump = false;

			if (InsideBirdJumpZone) {
				// determine which bird group is closer to you
				int i = 0;
				float dist = 9999;
				float temp = 0;
				int best = -1;
				while (i < Global.Dataholder.BirdCubePos.Length) {
					temp = (transform.position - Global.Dataholder.BirdCubePos [i]).magnitude;
					if (temp < dist) {
						dist = temp;
						best = i;
					}

					i++;
				}

				Global.Dataholder.HarmonySwish[best] = 0;
			}

		}

		if (bufferingJump > 0) {
			if (Super.Dataholder.GetReboundInputUp (KeyCode.Space) ) {
				BufferShortHop = true;
			}
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
		if (Super.Dataholder.GetReboundInput (KeyCode.D)) {
			dir++;
		}
		if (Super.Dataholder.GetReboundInput (KeyCode.A)) {
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

			if (onGround) {

				Anim.runtimeAnimatorController = RAC_Run;
				SR.flipX = dir == -1;
			}

		} else {

			if (onGround) {

				Anim.runtimeAnimatorController = RAC_Idle;

			}

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

		if (!Super.Dataholder.GetReboundInput (KeyCode.Space) && isjumping) {

			isjumping = false;
			JumpTime = 1;
			coyoteTime = -1;
			if (currentYVel > 0) {
				currentYVel *=0.5f;
			}
		}

		if (JumpTime > RealJumpDuration || BufferShortHop) {

			if (isjumping && !Super.Dataholder.GetReboundInput (KeyCode.Space)) {
				isjumping = false;
				JumpTime = 1;
				coyoteTime = -1;
				if (currentYVel > 0) {
					currentYVel *=0.5f;
				}
			}
		}

		if (isjumping && currentYVel > 0.1f) {
			PHit_Floor.Active = false;
		}



		if (!onGround) {
			Anim.runtimeAnimatorController = null;
			if (currentYVel > 0.1f) {
				SR.sprite = Sprite_Jump_Up;
			} else if (currentYVel > -0.1f) {
				SR.sprite = Sprite_Jump_Mid;

			} else {
				SR.sprite = Sprite_Jump_Down;

			}


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

		if (PHit_Floor.Active && currentYVel <= 0.3f) {

			coyoteTime = 0.125f;
			currentYVel = 0;
			onGround = true;
			transform.position = new Vector3(transform.position.x, PHit_Floor.Col.bounds.center.y + PHit_Floor.Col.bounds.extents.y, transform.position.z);
			HasCoyoteJump = true;
			FrogHeight = transform.position.y;
		}

		if (PHit_Ceiling.Active && currentYVel >= 0.01f) {
			currentYVel = 0;
			transform.position = new Vector3(transform.position.x, PHit_Ceiling.Col.bounds.center.y - PHit_Ceiling.Col.bounds.extents.y -1.5f, transform.position.z);
		}

		if (PHit_Shoulder_R.Active) {
			transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
		}
		if (PHit_Shoulder_L.Active) {
			transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
		}

		if (InsideBirdJumpZone) {
			if (currentYVel < -MaxFallSpeed/2f) {
				currentYVel = -MaxFallSpeed/2f;
			}
		}
		if (currentYVel < -MaxFallSpeed) {
			currentYVel = -MaxFallSpeed;
		}
		if (currentYVel > MaxJumpSpeed) {
			currentYVel = MaxJumpSpeed;
		}

		if (PHit_Wall_L.Active && currentSpeed < 0) {
			currentSpeed = 0;
		}
		if (PHit_Wall_R.Active && currentSpeed > 0) {
			currentSpeed = 0;
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
		if (BetweenLevels) {
			return;
		}
		if(other.CompareTag("BirdDoubleJump"))
		{
			InsideBirdJumpZone = true;
		}
		else if(other.CompareTag("FrogCube"))
		{
			if (currentYVel < 0) {
				InsideFrogZone = true;
				currentYVel = Mathf.Abs (currentYVel);
				// if the height for the jump is beyond the terminal velocity...
				float dist = (FrogHeight - other.transform.position.y);
				float termina = 4.2f;
				if (dist > termina) {
					FrogTime = (dist - termina)*(2.2f/50f);
				}
			}
		}
		else if(other.CompareTag("Stage"))
		{
			InsideStage = true;
			if (!Global.Dataholder.PlayerIsCarryingGoal) {
				Global.Dataholder.MostRecentPlatformPosition = transform.position;
			}
		}
		else if(other.CompareTag("LevelClearStage"))
		{
			InsideClearStage = true;
		}
		else if(other.CompareTag("Void"))
		{
			InsideVoid = true;
		}
		else if(other.CompareTag("Orb"))
		{
			Instantiate (Global.Dataholder.CircleSmall, other.transform.position, transform.rotation);
			Destroy (other.gameObject);
			Global.Dataholder.PlayerIsCarryingGoal = true;
		}
		else if(other.CompareTag("Cage"))
		{
			Cage C = other.GetComponent<Cage> ();
			if (C.Bird) {
				Global.Dataholder.AHUD.Birds++;
			}
			if (C.Frog) {
				Global.Dataholder.AHUD.Frogs++;
			}
			if (C.Rabbit) {
				Global.Dataholder.AHUD.Rabbits++;
			}

			Instantiate (Global.Dataholder.CircleSmall, other.transform.position, transform.rotation);
			Destroy (other.gameObject);
		}
		else if(other.CompareTag("Barrel"))
			{
				Global.Dataholder.ResetLevel ();
				Destroy (other.gameObject);
			}
	}

}

