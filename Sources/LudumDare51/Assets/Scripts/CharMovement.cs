using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour {

	public float speed = 5;
	public float AirMult;
	public Vector2 Movamajig;
	public GameObject vis;
	public Rigidbody rb;
	public CharFlip Flip;
	public SpriteRenderer SR;
	public Animator Anim;
	public RuntimeAnimatorController Idle;
	public RuntimeAnimatorController Run;
	public RuntimeAnimatorController Jump;
	public RuntimeAnimatorController Idle_NW;
	public RuntimeAnimatorController Run_NW;
	public RuntimeAnimatorController Jump_NW;
	public RuntimeAnimatorController Victory;
	public RuntimeAnimatorController VictoryBatteries;
	public RuntimeAnimatorController VictoryFinal;

	Vector3 fpos;

	public bool UpdateCameraFalling;

	public bool Jumping;
	public float JumpingTimer;
	public bool SmallHop;
	public bool Falling;
	public bool CanJump;
	public float JumpBufferTimer;
	public float JumpBufferDuration;
	public Vector3 JumpVelocity;
	public bool NoFallAccell;
	public bool NoMoving;
	public bool InAir;

	public float CantJumpSoftlockFix;
	public JumpCheck JC;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(G.Main.LevelClear)
		{
			Anim.runtimeAnimatorController = G.Main.CurrentLevel == 3 ? VictoryBatteries : G.Main.CurrentLevel == 5 ? VictoryFinal : Victory;
			return;
		}

		if(!CanJump)
		{
			CantJumpSoftlockFix-= Time.deltaTime;
			if(CantJumpSoftlockFix <0)
			{
				CanJump = true;
				InAir = false;
				GameObject Cloud = Instantiate(JC.DustCloud,new Vector3(transform.position.x - 0.5f,transform.position.y+0.5f,transform.position.z - 0.5f),transform.rotation) as GameObject;
				Cloud.transform.eulerAngles = new Vector3(-90,0,0);
			}
		}
		else
		{
			CantJumpSoftlockFix = 1.5f;
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

		if(!InAir)
		{
			if(Movamajig.x >= 0.2f)
			{
				if(!Flip.Right)
				{
					Flip.Turning = true;
				}
				else if(Flip.Turning)
				{
					Flip.RotationTimer =0;
					Flip.Right = false;
				}
			}
			else if(Movamajig.x <= -0.2f)
			{
				if(Flip.Right)
				{
					Flip.Turning = true;
				}
				else if(Flip.Turning)
				{				
					Flip.RotationTimer =0;
					Flip.Right = true;
				}
			}
		}
		if(Movamajig.magnitude > 0 && !InAir)
		{
			Anim.runtimeAnimatorController = G.Main.UnlockedWatch ? Run : Run_NW;
		}
		else if(!InAir)
		{
			Anim.runtimeAnimatorController = G.Main.UnlockedWatch ? Idle : Idle_NW;
		}
		else if(InAir)
		{
			Anim.runtimeAnimatorController = G.Main.UnlockedWatch ? Jump : Jump_NW;
		}

		JumpBufferTimer -= Time.deltaTime;
		if(Input.GetKeyDown(KeyCode.Space))
		{
			JumpBufferTimer = JumpBufferDuration;
		}

		if(((Input.GetKeyDown(KeyCode.Space)) || (Input.GetKey(KeyCode.Space)) && JumpBufferTimer >0) && CanJump && !NoMoving)
		{
			G.Main.CamMov.ZoomDelay = 0;

			JumpBufferTimer = 0;
			CanJump = false;
			Jumping = true;
			SmallHop = false;
			JumpingTimer =0;

			//JumpVelocity = rb.velocity;

			NoFallAccell = false;


			if(Movamajig.magnitude < 0.2f)
			{
				JumpVelocity = new Vector3(0,rb.velocity.y,0);
			}
			if(Movamajig.magnitude >= 0.2f  && Movamajig.magnitude < 0.75f)
			{
				JumpVelocity = new Vector3(Movamajig.x * 0.5f * AirMult,rb.velocity.y, Movamajig.y * 0.5f * AirMult);
			}
			if(Movamajig.magnitude >= 0.75f)
			{
				JumpVelocity = new Vector3(Movamajig.x* AirMult,rb.velocity.y, Movamajig.y * AirMult);
			}

		}


		if(Jumping)
		{
			if( (Input.GetKeyUp(KeyCode.Space) && !NoMoving && !SmallHop))
			{
				SmallHop = true;
				rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y*0.5f,rb.velocity.z);
			}
		}


		float interpolatefactor = (Time.time-Time.fixedTime) / Time.fixedDeltaTime;
		if (Mathf.Abs(Movamajig.x) >= 0.2f || Mathf.Abs(Movamajig.y) >= 0.2f || rb.velocity.x != 0 || rb.velocity.z != 0)
		{
			vis.transform.position = Vector3.Lerp(fpos,rb.position, interpolatefactor);
		}
		else
		{
			vis.transform.position = transform.position;
		}



	}


	void FixedUpdate()
	{
		if(G.Main.LevelClear)
		{
			rb.velocity = Vector3.zero;
			return;
		}

		fpos = rb.position;

		rb.velocity = new Vector3((((Movamajig.x*speed)+rb.velocity.x*2)/3f), rb.velocity.y, (((Movamajig.y*speed)+rb.velocity.z*2)/3f));

		if(Jumping)
		{
			InAir = true;

			if(JumpingTimer ==0)
			{
				if(rb.velocity.y > 1)
				{
					rb.velocity = new Vector3(rb.velocity.x,1,rb.velocity.z);
				}
			}
			JumpingTimer= JumpingTimer+1;
			if(JumpingTimer == 1)
			{
				transform.position += new Vector3(0,0.5f,0);
			}

			if(JumpingTimer <3)
			{
				if(rb.velocity.y < 0)
				{
					transform.position += new Vector3(0,1,0);
					JumpingTimer--;
				}
				rb.velocity = new Vector3(rb.velocity.x,G.Main.CurrentLevel == 5 ? 42 : 38,rb.velocity.z);
			}
			if(JumpingTimer >2)
			{
				rb.AddForce(new Vector3(0,-1,0),ForceMode.Impulse);
			}
			if(JumpingTimer >5)
			{
				rb.AddForce(new Vector3(0,-1,0),ForceMode.Impulse);
			}
			if(JumpingTimer >5 && rb.velocity.y < 0)
			{
				rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);

				JumpingTimer = 0;
				Jumping = false;
				Falling = true;

			}
		}

		if(Falling)
		{
			InAir = true;


			if(rb.velocity.y < -80f)
			{
				//JumpingTimer = 0;
				Jumping = false;
				NoFallAccell = true;
			}
			else
			{
				NoFallAccell = false;
			}
			if(!NoFallAccell)
			{
				JumpingTimer= JumpingTimer+1;
				if(JumpingTimer >=1)
				{
					rb.AddForce(new Vector3(0,-1,0),ForceMode.Impulse);
					if(rb.velocity.y < -25)
					{
						UpdateCameraFalling = true;
					}

				}
			}


		}


	}

}
