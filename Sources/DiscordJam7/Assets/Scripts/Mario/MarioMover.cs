using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioMover : MonoBehaviour {

	public SpriteRenderer SR;
	public Animator Anim;
	public int Powerup;
	public RuntimeAnimatorController Idle;
	public RuntimeAnimatorController Run;
	public RuntimeAnimatorController Pipe;
	public RuntimeAnimatorController Dead;
	public RuntimeAnimatorController Skid;
	public RuntimeAnimatorController IdleBig;
	public RuntimeAnimatorController RunBig;
	public RuntimeAnimatorController PipeBig;
	public RuntimeAnimatorController CrouchBig;
	public RuntimeAnimatorController Uncrouch;
	public RuntimeAnimatorController SkidBig;
	public Collider SmolCol;

	public Collider BigCol;

	public Sprite Jump1;
	public Sprite Jump2;
	public Sprite Jump3;
	public Sprite JumpBig1;
	public Sprite JumpBig2;
	public Sprite JumpBig3;
	public bool StuckInWall;

	public int IFrames;
	public bool IsDead;
	public int deadDelay;

	public bool LevelClearWalkRight;


	public int CoyoteTime;

	public int speed;
	// wall ejecting : 16 - 2px
	// walking : 24 (max)  4 px
	// run no p: 40	 - 5 px
	// P speed : 56  -  7 px

	// PX = SPEED/8

	public int XPosition;
	public int XSubpixel;

	public int Yspeed;
	public int YPosition;
	public int YSubPixel;

	public bool JumpingHoldingA;
	public bool OnGround;
	bool Skidding;
	public bool Crouching;
	public int Uncrouching;
	public bool CrouchedLastFrame;
	public bool LetGoOfJump;
	public bool LetGoOfGround;
	public bool JustJumped;

	public GameObject SFX_Jump;
	public GameObject SFX_Mush;
	public GameObject SFX_Hurt;
	public GameObject SFX_Bonk;
	public GameObject SFX_Coin;

	// Use this for initialization
	void Start () {
		XPosition = Mathf.RoundToInt(transform.position.x * 16);
		YPosition = Mathf.RoundToInt(transform.position.y * 16);
		Main.DataHolder.Mario = this;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(Main.DataHolder.PauseMario)
		{
			if(IsDead && !Main.DataHolder.MarioReturnToMap)
			{
				deadDelay--;
				if(deadDelay == 1)
				{
					Yspeed = 55;
				}
				if(deadDelay <=0)
				{

					Yspeed-=2;

					YSubPixel += Yspeed;
					if(Yspeed >0)
					{
						while(YSubPixel >8)
						{
							YSubPixel-=8;
							YPosition++;
						}
					}
					else
					{
						while(YSubPixel <0)
						{
							YSubPixel+=8;
							YPosition--;
						}
					}


					Yspeed = Mathf.Clamp(Yspeed,-69,69); //nice
					transform.position = new Vector3(XPosition/16f,YPosition/16f,0);

					if(transform.position.y < -16)
					{

						Main.DataHolder.MarioReturnToMap = true;
						Main.DataHolder.MarioReturnToMapTimer = 0;
					}



				}

			}

















			return;
		}


		if(LevelClearWalkRight)
		{
			if(CoyoteTime <=0)
			{
				speed = 0;
			}
			else
			{
				speed = 12;
			}
			SR.flipX = false;
		}



		IFrames--;
		if(IFrames > 0)
		{
			SR.enabled = (IFrames/4) % 2 == 1;
		}
		else
		{
			SR.enabled = true;
		}



		JustJumped = false;
		Skidding = false;
		int dir = 0;
		dir += Input.GetKey(KeyCode.A) ? -1 : 0;
		dir += Input.GetKey(KeyCode.D) ? 1 : 0;

		if(Powerup >0 && OnGround && !LevelClearWalkRight)
		{
			Crouching=false;
			if(Input.GetKey(KeyCode.S))
			{
				Crouching = true;
				CrouchedLastFrame = true;
				Anim.runtimeAnimatorController = CrouchBig;
			}

			if(CrouchedLastFrame && !Crouching)
			{
				Anim.runtimeAnimatorController = Uncrouch;
				Uncrouching = 3;
			}

			if(!Input.GetKey(KeyCode.S))
			{
				{
					CrouchedLastFrame=false;
				}
			}
		}

		Uncrouching--;
		if(Crouching && CoyoteTime >0)
		{
			dir = 0;

		}
		BigCol.enabled = (Powerup > 0) && !Crouching;
		SmolCol.enabled = !BigCol.enabled;
		if(dir != 0 && !LevelClearWalkRight)
		{

			int speedDir = Mathf.Clamp(speed,-1,1);
			if(Input.GetKey(KeyCode.LeftShift))
			{
				speed+= dir;
			
				if(Mathf.Abs(speed) > 40)
				{
					speed = speedDir*40;
				}
			}
			else
			{
				if(Mathf.Abs(speed) > 24)
				{
					speed = speedDir*24;
				}
				else
				{
					speed+= dir;
				}
			}
		}
		else
		{
			if(speed != 0)
			{
				if(speed >0)
				{
					speed--;
				}
				else
				{
					speed++;
				}
			}
		}
		if(dir != 0 && Uncrouching <= 0 && !LevelClearWalkRight)
		{
			if(Mathf.Clamp(speed,-1,1) != dir)
			{
				if(Input.GetKey(KeyCode.LeftShift))
				{
					speed+= dir;
				}
				else
				{
					speed+= dir;
				}
				if(OnGround)
				{
					Skidding = true;
					Anim.runtimeAnimatorController = (Powerup == 0) ? Skid : (Powerup == 1) ? SkidBig : SkidBig;
				}
			}
		}

		if(dir !=0 && OnGround &&!Skidding && Uncrouching <=0)
		{
			Anim.runtimeAnimatorController = (Powerup == 0) ? Run : (Powerup == 1) ? RunBig : RunBig;
		}
		else if(OnGround && !Skidding && Uncrouching <=0 && !Crouching)
		{
			if(Mathf.Abs(speed) > 10)
			{
				Anim.runtimeAnimatorController = (Powerup == 0) ? Run : (Powerup == 1) ? RunBig : RunBig;
			}
			else
			{
				Anim.runtimeAnimatorController = (Powerup == 0) ? Idle : (Powerup == 1) ? IdleBig : IdleBig;
			}
		}


		if((CoyoteTime <= 0 || (!OnGround && Input.GetKey(KeyCode.Space))) && !LevelClearWalkRight)
		{
			if(!Crouching)
			{
				if(Yspeed >5)
				{
					Anim.runtimeAnimatorController = null;
					SR.sprite = (Powerup == 0) ? Jump1 : (Powerup == 1) ? JumpBig1 : JumpBig1;
				}
				if(Yspeed <-5)
				{
					Anim.runtimeAnimatorController = null;
					SR.sprite = (Powerup == 0) ? Jump3 : (Powerup == 1) ? JumpBig3 : JumpBig3;
				}
				else
				{
					Anim.runtimeAnimatorController = null;
					SR.sprite = (Powerup == 0) ? Jump2 : (Powerup == 1) ? JumpBig2 : JumpBig2;
				}

			}


		}




		if(!Skidding && dir != 0)
		{
			SR.flipX = (dir==-1);
		}

		if(StuckInWall)
		{
			speed =8;
		}

		XSubpixel += speed;
		if(speed >0)
		{
			while(XSubpixel >4)
			{
				XSubpixel-=4;
				XPosition++;
			}
		}
		else
		{
			while(XSubpixel <0)
			{
				XSubpixel+=4;
				XPosition--;
			}
		}

		if(!Input.GetKey(KeyCode.Space))
		{
			LetGoOfJump = true;
			if(OnGround)
			{
				LetGoOfGround = true;
			}
		}

		if(!OnGround)
		{		

			if(Yspeed <= 32 || LetGoOfJump)
			{
				Yspeed -= 5;  // GRAVITY
			}
			else
			{
				Yspeed--;  
			}
		}
		else
		{
			Yspeed = 0;

		}
		if((OnGround || CoyoteTime>0) && !LevelClearWalkRight)
		{
			if(Input.GetKey(KeyCode.Space) && LetGoOfJump)
			{
				Yspeed = 55 + Mathf.Abs(speed)/4;
				LetGoOfJump = false;
				LetGoOfGround = false;
				OnGround = false;
				JustJumped = true;
				Instantiate(SFX_Jump);

			}
		}

		YSubPixel += Yspeed;
		if(Yspeed >0)
		{
			while(YSubPixel >8)
			{
				YSubPixel-=8;
				YPosition++;
			}
		}
		else
		{
			while(YSubPixel <0)
			{
				YSubPixel+=8;
				YPosition--;
			}
		}


		Yspeed = Mathf.Clamp(Yspeed,-69,69); //nice






		transform.position = new Vector3(XPosition/16f,YPosition/16f,0);

		if(transform.position.y < -16.5f)
		{
			IsDead = true;
			deadDelay = 120;
			Main.DataHolder.HoldPauseMario = true;

		}




		CoyoteTime--;
		OnGround = false;
		StuckInWall = false;


	}

	void Update()
	{
		 

	}


	public GameObject Points_1000;


	void OnTriggerStay(Collider other)
	{
		if(IsDead)
		{
			return;
		}


		if(other.tag == "Floor")
		{
			CoyoteTime = 6;
			OnGround = true;
			Yspeed = 0;
			while(YPosition % 32 !=0)
			{
				YPosition++;
			}
			transform.position = new Vector3(XPosition/16f,YPosition/16f,0);
			if(Input.GetKey(KeyCode.Space))
			{
				LetGoOfGround = false;
			}
		}
		if(other.tag == "OneWayFloor" && Yspeed <=0 && (YPosition+512)%32 > 22 && transform.position.y > other.transform.position.y+1)
		{
			CoyoteTime = 6;

			OnGround = true;
			Yspeed = 0;
			while(YPosition % 32 !=0)
			{
				YPosition++;
			}
			transform.position = new Vector3(XPosition/16f,YPosition/16f,0);
			if(Input.GetKey(KeyCode.Space))
			{
				LetGoOfGround = false;
			}
		}

		if(other.tag == "WallLeft"&&!StuckInWall)
		{
			speed = 0;
			XSubpixel = 0;
			XPosition++;

			transform.position = new Vector3(XPosition/16f,YPosition/16f,0);
		}
		if(other.tag == "WallRight" &&!StuckInWall)
		{
			speed = 0;
			XSubpixel = 0;
			XPosition--;

			transform.position = new Vector3(XPosition/16f,YPosition/16f,0);
		}
		if(other.tag == "ScreenWallLeft"&&!StuckInWall)
		{
			speed = 0;
			XSubpixel = 0;
			XPosition++;

			transform.position = new Vector3(XPosition/16f,YPosition/16f,0);
		}
		if(other.tag == "ScreenWallRight" &&!StuckInWall)
		{
			speed = 0;
			XSubpixel = 0;
			XPosition--;

			transform.position = new Vector3(XPosition/16f,YPosition/16f,0);
		}
		if(other.tag == "OneWayCeiling" &&!StuckInWall && !OnGround && Yspeed >=0)
		{
			Yspeed = 0;
			while(YPosition % 32 !=0)
			{
				YPosition--;
			}

			transform.position = new Vector3(XPosition/16f,YPosition/16f,0);
		}
		if(other.tag == "CeilingBonk" &&!StuckInWall && !OnGround && Yspeed >=0)
		{
			Yspeed = Mathf.Abs(Yspeed)*-1;
			while(YPosition % 32 !=0)
			{
				YPosition--;
			}
			transform.position = new Vector3(XPosition/16f,YPosition/16f,0);
			//Bonk anim?
			other.transform.parent.GetComponent<Bonkable>().Bonking = true;
		}
		if(other.tag == "BonkDestroy" &&!StuckInWall && !OnGround && Yspeed >=0)
		{
			Yspeed = Mathf.Abs(Yspeed)*-1;
			while(YPosition % 32 !=0)
			{
				YPosition--;
			}
			transform.position = new Vector3(XPosition/16f,YPosition/16f,0);
			//Bonk anim?
			other.transform.parent.GetComponent<Bonkable>().Bonking = true;
			if(Powerup >0)
			{
				other.transform.parent.GetComponent<Bonkable>().Break = true;

			}
		}
		if(other.tag == "InsideWall")
		{
			StuckInWall = true;
		}

		if(other.tag == "Goomba")
		{

			if(transform.position.y > other.transform.position.y + 1.25f)
			{
				//kill goomba
				Yspeed = 55;
				LetGoOfJump = false;
				LetGoOfGround = false;
				OnGround = false;
				JustJumped = true;
				other.GetComponent<Goomba>().Dead = true;
				Instantiate(SFX_Bonk);

			}
			else if(IFrames <=0)
			{
				Hurt();
			}

		}
		if(other.tag == "Mushroom")
		{

			if(Powerup == 0)
			{

				CollectMushroom();
			}

			Instantiate(Points_1000,transform.position,transform.rotation);
			Destroy(other.gameObject);

		}
		if(other.tag == "Coin")
		{
			Instantiate(SFX_Coin);

			Destroy(other.gameObject);

		}
		if(other.tag == "LevelGoal")
		{

			speed = 0;
			if(Yspeed > 0)
			{
				Yspeed = 0;
			}
			other.GetComponent<GoalCard>().Happened = true;			
			LevelClearWalkRight = true;
			Main.DataHolder.MarioClearWalkRight = true;
		}
		if(other.tag == "Hurt")
		{
			if(IFrames <=0)
			{
				Hurt();
			}

		}
		if(other.tag == "Lava")
		{
			Powerup = 0;			
			Hurt();

		}

		if(other.tag == "Axe")
		{
			Win();

		}
	}


	void Hurt()
	{
		Powerup--;
		if(Powerup <= -1)
		{
			Main.DataHolder.HoldPauseMario = true;
			IsDead = true;
			Anim.runtimeAnimatorController = Dead;
			deadDelay = 60;
		}
		else
		{
			Main.DataHolder.MPauseFrames = 60;
			IFrames = 120;
		}
		Instantiate(SFX_Hurt);

	}

	void CollectMushroom()
	{
		Powerup = 1;
		Main.DataHolder.MPauseFrames = 60;
		Instantiate(SFX_Mush);
	}

	void Win()
	{
		Main.DataHolder.MarioComplete = true;
		Main.DataHolder.HoldPauseMario = true;
	}

}
