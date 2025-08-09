using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Move : MonoBehaviour {




	public bool Hexadecimal;
	public int PosLBX;
	public int PosHBX;

	public int PosLBY;
	public int PosHBY;

	public int SpeedX;
	public int SubpixelX;

	public int SpeedY;
	public int SubpixelY;

	public bool MoveLeft;
	public bool MoveRight;

	public int SpeedLimit;

	public bool MRGRight; //used for wall direction detection
	public bool MRGLeft;

	public RuntimeAnimatorController Idle16;
	public RuntimeAnimatorController Move16;
	public RuntimeAnimatorController Jump16;
	public RuntimeAnimatorController Kick16;
	public RuntimeAnimatorController IdleHold16;
	public RuntimeAnimatorController MoveHold16;
	public RuntimeAnimatorController JumpHold16;


	public Animator Anim;
	public SpriteRenderer SR;
	public SpriteRenderer FaceSR;

	public bool Holding;
	public Object_Move HeldObject;

	public int KickTimer;
	public Char_JumpDetection JumpDetect;

	public bool Jumping;
	public int JumpTimer;

	public bool WallOnce;

	public bool Dead;

	public int JumpHeight;

	public DataHolder Main;
	public CameraMove Cam;

	public Sprite Deathframe;
	public GameObject DeathParts;
	public float DeathTimer;

	public float InteractTimer;
	public SpriteRenderer InteractWarn;

	public bool CanInteract;

	public bool Yump;

	// Use this for initialization
	void Start () {
		//SpeedLimit = 40;
	}
	
	// Update is called once per frame

	void Update()
	{
		if(CanInteract)
		{
			if(Input.GetKeyDown(KeyCode.W))
			{
				CanInteract = false;
				//interact?
				if(Main.currentlevel == 1)
				{
					if(Main.PlayerName == "")
					{
						Main.OutOfNametagsTimer = 4;
						Main.NameTagObject.Initial = true;
						Main.PseudoPause = true;
						Main.PostNametag.SetActive(false);
						Main.outOfNametags.SetActive(false);
					}
					else
					{
						Main.OutOfNametagsTimer = 1;
						Main.outOfNametags.SetActive(true);
					}
				}
				else if(Main.currentlevel == 3 || Main.currentlevel == 5)
				{
					if(Main.Vend.VendDid)
					{
						Main.OutOfNametagsTimer = 1;
						Main.VendAllOut.SetActive(true);
					}
					else if(Main.Coins >= 10)
					{
						Main.Vend.VendDo = true;
						Main.Coins -= 10;
						Main.CoinTimer = 0;
						Main.Vend.Anim.runtimeAnimatorController = Main.Vend.VendSquish;
					}
					else
					{
						Main.OutOfNametagsTimer = 1;
						Main.VendExpensive.SetActive(true);
					}
				}
				else if(Main.currentlevel == 6)
				{
					Main.OutOfNametagsTimer = 1;
					Main.ReflectionText.SetActive(true);
				}

			}
		}
	}

	void FixedUpdate () {

		if(Main.LoadNextRoom)
		{
			return;
		}

		if(Main.PseudoPause)
		{
			InteractWarn.enabled = false;
			return;
		}

		if(Dead)
		{
			if(DeathTimer > 0.5f)
			{
				SR.enabled = false;

				return;
			}
			DeathTimer += Time.fixedDeltaTime;
			if(DeathTimer >= 0.4f)
			{
				SR.enabled = true;
			}
			SR.sprite = Deathframe;
			Anim.runtimeAnimatorController = null;
			if(DeathTimer >= 0.5f)
			{
				Instantiate(DeathParts,transform.position,transform.rotation,Main.CurrentLoadedLevel.transform);
				SR.enabled = false;
				FaceSR.enabled = false;
			}
			return;
		}

		InteractTimer += Time.fixedDeltaTime;
		InteractWarn.color = new Vector4(Mathf.Abs(Mathf.Sin(InteractTimer*6))*0.5f+0.5f,0,0,1);
		InteractWarn.transform.localPosition = new Vector3(0,1.5f + Mathf.Abs(Mathf.Sin(InteractTimer*5f)),0);


		WallOnce = false;
		MoveLeft = false;
		MoveRight = false;

		if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Dead)
		{
			MoveLeft = true;
			SR.flipX = true;
			if(KickTimer <=0)
			{
				Anim.runtimeAnimatorController = Holding ? MoveHold16 : Move16 ;
			}
		}
		else if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Dead)
		{
			MoveRight = true;
			SR.flipX = false;
			if(KickTimer <=0)
			{
				Anim.runtimeAnimatorController = Holding ? MoveHold16 : Move16;
			}
		}
		else
		{
			if(KickTimer <=0)
			{
				Anim.runtimeAnimatorController = Holding ? IdleHold16 : Idle16;
			}
		}

		if(MoveRight && !Dead)
		{
			if(Hexadecimal)
			{
				SpeedX += 2;
				if(SpeedX < 0)
				{
					SpeedX += 8;
				}
				if(SpeedX > SpeedLimit)
				{
					SpeedX = SpeedLimit;
				}
				SubpixelX += SpeedX;
			}
			else
			{
				SpeedX += 2;
				if(SpeedX < 0)
				{
					SpeedX += 8;
				}
				if(SpeedX > 30)
				{
					SpeedX = 30;
				}
				SubpixelX += SpeedX;
			}
		}

		else if(MoveLeft && !Dead)
		{
			if(Hexadecimal)
			{
				SpeedX -= 2;
				if(SpeedX > 0)
				{
					SpeedX -= 8;
				}
				if(SpeedX < -SpeedLimit)
				{
					SpeedX = -SpeedLimit;
				}
				SubpixelX += SpeedX;
			}
			else
			{
				SpeedX -= 2;
				if(SpeedX > 0)
				{
					SpeedX -= 8;
				}
				if(SpeedX < -30)
				{
					SpeedX = -30;
				}
				SubpixelX += SpeedX;
			}
		}
		else
		{
			//if(Hexadecimal)
			//{
				if(SpeedX > 0)
				{
					SpeedX -= 4;
				}
				else if(SpeedX < 0)
				{
					SpeedX += 4;
				}
				if(Mathf.Abs(SpeedX) < 4)
				{
					SpeedX = 0;
				}
				SubpixelX += SpeedX;
			//}
		}

		if(Hexadecimal)
		{
			while(SubpixelX >= 10)
			{
				SubpixelX -= 10;
				PosHBX++;
				Main.PlayerPosXTimer = 0;
				if(PosHBX >=16)
				{
					PosHBX = 0;
					PosLBX++;
				}
			}
			while(SubpixelX < 0)
			{
				SubpixelX += 10;
				PosHBX--;
				Main.PlayerPosXTimer = 0;

				if(PosHBX <0)
				{
					PosHBX = 15;
					PosLBX--;
				}
			}
		}


		if(!JumpDetect.CanJump || Dead)
		{
			SpeedY -= 2;
		}
		JumpTimer--;
		if(!Jumping && JumpDetect.CanJump && Input.GetKey(KeyCode.Space) || Yump)
		{
			Jumping = true;
			JumpTimer = 2; 
			Yump = false;
		}
		if(JumpTimer > 0)
		{
			if(Hexadecimal)
			{
				SpeedY = JumpHeight;
			}
		}

		if(Jumping && !Input.GetKey(KeyCode.Space))
		{
			if(SpeedY > 0)
			{
				SpeedY /= 2;
			}
			Jumping = false;
		}
		if(!JumpDetect.CanJump)
		{
			if(KickTimer <= 0)
			{
				Anim.runtimeAnimatorController = Holding ? JumpHold16 : Jump16;
			}
		}

		if(Hexadecimal)
		{
			if(SpeedY < -JumpHeight)
			{
				SpeedY = -JumpHeight;
			}
			SubpixelY += SpeedY;
		}

		if(Hexadecimal)
		{
			while(SubpixelY >= 10)
			{
				SubpixelY -= 10;
				PosHBY++;
				Main.PlayerPosYTimer =0;
				if(PosHBY >=16)
				{
					PosHBY = 0;
					PosLBY++;

				}
			}
			while(SubpixelY < 0)
			{
				SubpixelY += 10;
				PosHBY--;
				Main.PlayerPosYTimer =0;
				if(PosHBY <0)
				{
					PosHBY = 15;
					PosLBY--;

				}
			}
		}
		else
		{
			while(SubpixelY >= 10)
			{
				SubpixelY -= 10;
				PosHBY++;
				Main.PlayerPosYTimer =0;
				if(PosHBY >=10)
				{
					PosHBY = 0;
					PosLBY++;

				}
			}
			while(SubpixelY < 0)
			{
				SubpixelY += 10;
				PosHBY--;
				Main.PlayerPosYTimer =0;
				if(PosHBY <0)
				{
					PosHBY = 9;
					PosLBY--;
				}
			}
		}

		if(PosLBX == -2)
		{
			SpeedX = 0;
			PosLBX = -1;
			PosHBX = 0;
		}

		if(PosLBX == 22 + Cam.ExtraRoomSize)
		{

			Main.LoadNextRoom = true;
			return;
		}

		if(Hexadecimal)
		{
			transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f);
		}
		KickTimer--;

		if(Holding)
		{
			if((!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)) || Dead)
			{
				Holding = false;

				//Kick object
				KickTimer = 16;
				if(HeldObject != null)
				{
				Anim.runtimeAnimatorController = Kick16;
				Instantiate(Main.SFX_Key2,transform.position,transform.rotation,Main.transform);

				}
				HeldObject.OnGroundGraceTimer = 0;
				HeldObject.HeldBy = null;
				HeldObject.Held = false;

				if(HeldObject.PhysBlock)
				{
					HeldObject.PhysState = "Physics";
					Main.CrateState = "Physics";
					Main.CrateStateTimer = 0;
				}

				if(HeldObject.InsideWall)
				{
					//HeldObject.Dead = true;
					//HeldObject.SpeedY = 24;
					//HeldObject.SR.flipY = true;
					//if(HeldObject.SelfWall != null){Destroy(HeldObject.SelfWall.gameObject);}
					//if(HeldObject.SelfFloor != null){ Destroy(HeldObject.SelfFloor.gameObject);}

					//HeldObject.gameObject.layer = 11;


					HeldObject.PosLBX = PosLBX;
					HeldObject.PosHBX = PosHBX;
					HeldObject.PosLBY = PosLBY;
					HeldObject.PosHBY = PosHBY;

				}
				else
				{

					if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
					{
						if(Hexadecimal)
						{
							HeldObject.SpeedY = 64 + SpeedY;
							HeldObject.SpeedX = 0;
						}
					}
					else if(!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
					{
						HeldObject.SpeedY = 0;
						HeldObject.SpeedX = 0;

					}
					else
					{
						if(Hexadecimal)
						{
							if(SR.flipX){HeldObject.PosHBX-=12;}else{HeldObject.PosHBX+=12;}

							if(HeldObject.PosHBX >=16)
							{
								HeldObject.PosLBX++;
								HeldObject.PosHBX-=16;
							}
							if(HeldObject.PosHBX <0)
							{
								HeldObject.PosLBX--;
								HeldObject.PosHBX+=16;
							}
							HeldObject.SpeedX = SR.flipX ? -52 + SpeedX/2 : 52 + SpeedX/2;
							HeldObject.SpeedY = 36 + SpeedY/2;
						}

						HeldObject.GraceFrames = 2;
					}
					HeldObject.WallReturnGrace = 2;

				}
				HeldObject.PlayerColGrace = 8;
				HeldObject = null;
			}
		}

		if(SpeedX > 0)
		{
			MRGLeft = false;
			MRGRight = true;
		}
		else if(SpeedX < 0)
		{
			MRGLeft = true;
			MRGRight = false;
		}


		InteractWarn.enabled = false;
		CanInteract = false;

		SR.flipY = Dead;
		Cam.Move();
	}


	void OnTriggerStay(Collider other)
	{
		if(Dead)
		{
			return;
		}

		if(Main.PseudoPause)
		{
			return;
		}

		if(other.tag == "Holdable")
		{
			if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && !Holding)
			{
				HeldObject = other.GetComponent<Object_Move>(); //It better have this. Otherwise the tag shouldn't be there
				HeldObject.HeldBy = this;
				HeldObject.Held = true;
				if(HeldObject.SelfWall != null){HeldObject.SelfWall.transform.localPosition = new Vector3(0,0,-500);}
				if(HeldObject.SelfFloor != null){HeldObject.SelfFloor.transform.localPosition = new Vector3(0,0,-500);}

				if(HeldObject.PhysBlock)
				{
					HeldObject.PhysState = "Held";
					Main.CrateState = "Held";
					if(!Holding) //just in case
					{
						Main.CrateStateTimer = 0;

					}
				}

				if(!Holding)
				{
					Instantiate(Main.SFX_Key1,transform.position,transform.rotation,Main.transform);

				}
				Holding = true;

			}				

		}



		if(other.tag == "Ground")
		{
			if((transform.position.y >= other.transform.position.y + 1.2f && Hexadecimal) && SpeedY < 0)
			{
				SpeedY = 0;

				int i = 0;
				while(transform.position.y < other.transform.position.y+2f && i < 4)
				{
					PosHBY++;
					if(Hexadecimal)
					{
						if(PosHBY >=16)
						{
							PosHBY = 0;
							PosLBY++;
						}
						transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f);
					}
					else
					{
						if(PosHBY >=10)
						{
							PosHBY = 0;
							PosLBY++;
						}
						transform.position = new Vector3(PosLBX + PosHBX/10f,PosLBY + PosHBY/10f);
					}		
					i++;
				}
			}
		}

		if(other.tag == "Ceiling")
		{
			if(transform.position.y < other.transform.position.y + 1.5f && SpeedY > 0)
			{
				SpeedY = 0;

				int i = 0;
				while(transform.position.y > other.transform.position.y-2 && i < 4)
				{
					PosHBY--;
					if(Hexadecimal)
					{
						if(PosHBY <0)
						{
							PosHBY = 15;
							PosLBY--;
						}
						transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f,0.2f);
					}
					else
					{
						if(PosHBY <0)
						{
							PosHBY = 9;
							PosLBY--;
						}
						transform.position = new Vector3(PosLBX + PosHBX/10f,PosLBY + PosHBY/10f,0.2f);
					}
					i++;
				}
			}
		}

		if(other.tag == "Killzone" && !Dead)
		{
			Dead = true;
			DeathTimer = 0;
			Instantiate(Main.SFX_Dead,transform.position,transform.rotation,Main.transform);


		}

		if(other.tag == "Interact" && !Dead)
		{
			InteractWarn.enabled = true;
			CanInteract = true;

		}

		if(other.tag == "Coin" && !Dead)
		{
			Main.Coins++;
			Main.CoinTimer = 0;
			Destroy(other.gameObject);
			Instantiate(Main.SFX_Coin,transform.position,transform.rotation,Main.transform);
		}

		if(other.tag == "CoinBrick" && !Dead)
		{
			Main.Coins++;
			Main.CoinTimer = 0;
			Destroy(other.transform.parent.gameObject);
			Instantiate(Main.SFX_Coin,transform.position,transform.rotation,Main.transform);

		}

		if(other.tag == "PSwitch")
		{
			Main.ButtonPressed = 1;
			SpeedY = 0;
			if(Input.GetKey(KeyCode.Space) && !Jumping)
			{
				Yump = true;
			}
		}

		if(other.tag == "Wall" && !WallOnce)
		{
			WallOnce = true;
			if(MRGRight && (transform.position.x < other.transform.position.x || (transform.position.x > other.transform.position.x +8)))
			{
				PosHBX--;
				if(Hexadecimal)
				{
					if(PosHBX <0)
					{
						PosHBX = 15;
						PosLBX--;
					}
				}
				else
				{
					if(PosHBX <0)
					{
						PosHBX = 9;
						PosLBX--;
					}
				}
			}
			if(MRGLeft && (transform.position.x > other.transform.position.x || (transform.position.x < other.transform.position.x -8)))
			{
				PosHBX++;
				if(Hexadecimal)
				{
					if(PosHBX >=16)
					{
						PosHBX = 0;
						PosLBX++;
					}
				}
				else
				{
					if(PosHBX >=10)
					{
						PosHBX = 0;
						PosLBX++;
					}
				}
			}
			SpeedX = 0;
			if(Hexadecimal)
			{
				transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f);
			}
			else
			{
				transform.position = new Vector3(PosLBX + PosHBX/10f,PosLBY + PosHBY/10f);
			}
			Cam.Move();

		}


	}



}
