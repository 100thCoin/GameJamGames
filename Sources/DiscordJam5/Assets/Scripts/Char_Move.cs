using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Move : MonoBehaviour {




	public bool Hexadecimal;
	public bool AdjustedForHex;
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

	public bool MRGRight;
	public bool MRGLeft;

	public RuntimeAnimatorController Idle16;
	public RuntimeAnimatorController Move16;
	public RuntimeAnimatorController Jump16;
	public RuntimeAnimatorController Kick16;
	public RuntimeAnimatorController IdleHold16;
	public RuntimeAnimatorController MoveHold16;
	public RuntimeAnimatorController JumpHold16;

	public RuntimeAnimatorController Idle10;
	public RuntimeAnimatorController Move10;
	public RuntimeAnimatorController Jump10;
	public RuntimeAnimatorController Kick10;
	public RuntimeAnimatorController IdleHold10;
	public RuntimeAnimatorController MoveHold10;
	public RuntimeAnimatorController JumpHold10;

	public Animator Anim;
	public SpriteRenderer SR;
	public SpriteRenderer SRL;
	public SpriteRenderer SRR;
	public SpriteRenderer SRU;
	public SpriteRenderer SRD;
	public Animator AnimL;
	public Animator AnimR;
	public Animator AnimU;
	public Animator AnimD;

	public bool Holding;
	public Object_Move HeldObject;

	public int KickTimer;
	public Char_JumpDetection JumpDetect;

	public bool Jumping;
	public int JumpTimer;

	public bool WallOnce;

	public bool Dead;

	// Use this for initialization
	void Start () {
		SpeedLimit = 40;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(!Hexadecimal && AdjustedForHex)
		{
			SRL.transform.localPosition = new Vector3(-10,0,0);
			SRR.transform.localPosition = new Vector3(10,0,0);
			SRU.transform.localPosition = new Vector3(0,10,0);
			SRD.transform.localPosition = new Vector3(0,-10,0);
			AdjustedForHex = false;
		}
		else if(Hexadecimal && !AdjustedForHex)
		{
			SRL.transform.localPosition = new Vector3(-16,0,0);
			SRR.transform.localPosition = new Vector3(16,0,0);
			SRU.transform.localPosition = new Vector3(0,16,0);
			SRD.transform.localPosition = new Vector3(0,-16,0);
			AdjustedForHex = true;
		}


		WallOnce = false;
		MoveLeft = false;
		MoveRight = false;

		if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Dead)
		{
			MoveLeft = true;
			SR.flipX = true;
			if(KickTimer <=0)
			{
				Anim.runtimeAnimatorController = Holding ? Hexadecimal ? MoveHold16 : MoveHold10 : Hexadecimal ? Move16 : Move10;
			}
		}
		else if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Dead)
		{
			MoveRight = true;
			SR.flipX = false;
			if(KickTimer <=0)
			{
				Anim.runtimeAnimatorController = Holding ? Hexadecimal ? MoveHold16 : MoveHold10 : Hexadecimal ? Move16 : Move10;
			}
		}
		else
		{
			if(KickTimer <=0)
			{
				Anim.runtimeAnimatorController = Holding ? Hexadecimal ? IdleHold16 : IdleHold10 : Hexadecimal ? Idle16 : Idle10;
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
				if(PosHBX >=16)
				{
					PosHBX = 0;
					PosLBX++;
					if(PosLBX >=16)
					{
						PosLBX = 0;
					}
				}
			}
			while(SubpixelX < 0)
			{
				SubpixelX += 10;
				PosHBX--;
				if(PosHBX <0)
				{
					PosHBX = 15;
					PosLBX--;
					if(PosLBX <0)
					{
						PosLBX = 15;
					}
				}
			}
		}
		else
		{
			while(SubpixelX >= 10)
				{
					SubpixelX -= 10;
					PosHBX++;
					if(PosHBX >=10)
					{
						PosHBX = 0;
						PosLBX++;
						if(PosLBX >=10)
						{
							PosLBX = 0;
						}
					}
				}
			while(SubpixelX < 0)
			{
				SubpixelX += 10;
				PosHBX--;
				if(PosHBX <0)
				{
					PosHBX = 9;
					PosLBX--;
					if(PosLBX <0)
					{
						PosLBX = 9;
					}
				}
			}
		}

		if(!JumpDetect.CanJump || Dead)
		{
			SpeedY -= 2;
		}
		JumpTimer--;
		if(!Jumping && JumpDetect.CanJump && Input.GetKey(KeyCode.Space))
		{
			Jumping = true;
			JumpTimer = 2; 
		}
		if(JumpTimer > 0)
		{
			if(Hexadecimal)
			{
				SpeedY = 36;
			}
			else
			{
				SpeedY = 26;
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
				Anim.runtimeAnimatorController = Holding ? Hexadecimal ? JumpHold16 : JumpHold10 : Hexadecimal ? Jump16 : Jump10;
			}
		}

		if(Hexadecimal)
		{
			if(SpeedY < -36)
			{
				SpeedY = -36;
			}
			SubpixelY += SpeedY;
		}
		else
		{
			if(SpeedY < -22)
			{
				SpeedY = -22;
			}
			SubpixelY += SpeedY;
		}

		if(Hexadecimal)
		{
			while(SubpixelY >= 10)
			{
				SubpixelY -= 10;
				PosHBY++;
				if(PosHBY >=16)
				{
					PosHBY = 0;
					PosLBY++;
					if(PosLBY >=16)
					{
						PosLBY = 0;
						if(Dead)
						{
							PosHBY = 15;
							PosLBY = 15;
							gameObject.SetActive(false);
						}
					}
				}
			}
			while(SubpixelY < 0)
			{
				SubpixelY += 10;
				PosHBY--;
				if(PosHBY <0)
				{
					PosHBY = 15;
					PosLBY--;
					if(PosLBY <0)
					{
						PosLBY = 15;
						if(Dead)
						{
							PosHBY = 0;
							PosLBY = 0;
							gameObject.SetActive(false);
						}
					}
				}
			}
		}
		else
		{
			while(SubpixelY >= 10)
			{
				SubpixelY -= 10;
				PosHBY++;
				if(PosHBY >=10)
				{
					PosHBY = 0;
					PosLBY++;
					if(PosLBY >=10)
					{
						PosLBY = 0;
						if(Dead)
						{
							PosHBY = 9;
							PosLBY = 9;
							gameObject.SetActive(false);
						}
					}
				}
			}
			while(SubpixelY < 0)
			{
				SubpixelY += 10;
				PosHBY--;
				if(PosHBY <0)
				{
					PosHBY = 9;
					PosLBY--;
					if(PosLBY <0)
					{
						PosLBY = 9;
						if(Dead)
						{
							PosHBY = 0;
							PosLBY = 0;
							gameObject.SetActive(false);
						}
					}
				}
			}
		}

		if(Hexadecimal)
		{
			transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f);
		}
		else
		{
			transform.position = new Vector3(PosLBX + PosHBX/10f,PosLBY + PosHBY/10f);
		}
		KickTimer--;

		if(Holding)
		{
			if((!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)) || Dead)
			{
				Holding = false;

				//Kick object
				KickTimer = 16;
				Anim.runtimeAnimatorController = Hexadecimal ? Kick16 : Kick10;
				HeldObject.OnGroundGraceTimer = 0;
				HeldObject.HeldBy = null;
				HeldObject.Held = false;
				if(HeldObject.InsideWall)
				{
					HeldObject.Dead = true;
					HeldObject.SpeedY = 24;
					HeldObject.SR.flipY = true;
					if(HeldObject.SelfWall != null){Destroy(HeldObject.SelfWall.gameObject);}
					if(HeldObject.SelfFloor != null){ Destroy(HeldObject.SelfFloor.gameObject);}

					HeldObject.gameObject.layer = 11;

					//int i = 0;
					//while(i < HeldObject.Cols.Length)
					//{
					//	HeldObject.Cols[i].enabled = false;
					//	i++;
					//}
				}
				else
				{

					if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
					{
						if(Hexadecimal)
						{
							HeldObject.SpeedY = 64;
							HeldObject.SpeedX = 0;
						}
						else
						{
							HeldObject.SpeedY = 40;
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
						if(HeldObject.UFO){HeldObject.PosHBY = PosHBY; HeldObject.PosLBY = PosLBY;}
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
							if(HeldObject.PosLBX>=16)
							{
								HeldObject.PosLBX = 0;
							}
							if(HeldObject.PosLBX<0)
							{
								HeldObject.PosLBX = 15;
							}
							HeldObject.SpeedX = SR.flipX ? -52 : 52;
							if(!HeldObject.UFO)
							{
								HeldObject.SpeedY = 36;
							}
							else
							{
								HeldObject.SpeedY = 0;
							}
						}
						else
						{
							if(SR.flipX){HeldObject.PosHBX-=5;}else{HeldObject.PosHBX+=5;}

							if(HeldObject.PosHBX >=10)
							{
								HeldObject.PosLBX++;
								HeldObject.PosHBX-=10;
							}
							if(HeldObject.PosHBX <0)
							{
								HeldObject.PosLBX--;
								HeldObject.PosHBX+=10;
							}
							if(HeldObject.PosLBX>=10)
							{
								HeldObject.PosLBX = 0;
							}
							if(HeldObject.PosLBX<0)
							{
								HeldObject.PosLBX = 9;
							}
							HeldObject.SpeedX = SR.flipX ? -36 : 36;
							if(!HeldObject.UFO)
							{
								HeldObject.SpeedY = 28;
							}
							else
							{
								HeldObject.SpeedY = 0;
							}						
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

		SR.flipY = Dead;
		SRL.flipY = Dead;
		SRR.flipY = Dead;
		SRU.flipY = Dead;
		SRD.flipY = Dead;

		SRL.flipX = SR.flipX;
		SRR.flipX = SR.flipX;
		SRU.flipX = SR.flipX;
		SRD.flipX = SR.flipX;
		AnimL.runtimeAnimatorController = Anim.runtimeAnimatorController;
		AnimR.runtimeAnimatorController = Anim.runtimeAnimatorController;
		AnimU.runtimeAnimatorController = Anim.runtimeAnimatorController;
		AnimD.runtimeAnimatorController = Anim.runtimeAnimatorController;
	}


	void OnTriggerStay(Collider other)
	{
		if(Dead)
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

				Holding = true;

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
						if(PosLBX <0)
						{
							PosLBX = 15;
						}
					}
				}
				else
				{
					if(PosHBX <0)
					{
						PosHBX = 9;
						PosLBX--;
						if(PosLBX <0)
						{
							PosLBX = 9;
						}
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
						if(PosLBX >=16)
						{
							PosLBX = 0;
						}
					}
				}
				else
				{
					if(PosHBX >=10)
					{
						PosHBX = 0;
						PosLBX++;
						if(PosLBX >=10)
						{
							PosLBX = 0;
						}
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
		}

		if(other.tag == "Ground")
		{
			if(((transform.position.y >= other.transform.position.y + 12f/16f && Hexadecimal) || (transform.position.y >= other.transform.position.y + 5f/10f && !Hexadecimal)) && SpeedY < 0)
			{
				SpeedY = 0;

				int i = 0;
				while(transform.position.y < other.transform.position.y+1 && i < 4)
				{
					PosHBY++;
					if(Hexadecimal)
					{
						if(PosHBY >=16)
						{
							PosHBY = 0;
							PosLBY++;
							if(PosLBY >=16)
							{
								PosLBY = 0;
							}
						}
						transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f);
					}
					else
					{
						if(PosHBY >=10)
						{
							PosHBY = 0;
							PosLBY++;
							if(PosLBY >=10)
							{
								PosLBY = 0;
							}
						}
						transform.position = new Vector3(PosLBX + PosHBX/10f,PosLBY + PosHBY/10f);
					}		
					i++;
				}
			}
		}

		if(other.tag == "Ceiling")
		{
			if(transform.position.y < other.transform.position.y + 0.5f && SpeedY > 0)
			{
				SpeedY = 0;

				int i = 0;
				while(transform.position.y > other.transform.position.y-1 && i < 4)
				{
					PosHBY--;
					if(Hexadecimal)
					{
						if(PosHBY <0)
						{
							PosHBY = 15;
							PosLBY--;
							if(PosLBY <0)
							{
								PosLBY = 15;
							}
						}
						transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f,0.2f);
					}
					else
					{
						if(PosHBY <0)
						{
							PosHBY = 9;
							PosLBY--;
							if(PosLBY <0)
							{
								PosLBY = 9;
							}
						}
						transform.position = new Vector3(PosLBX + PosHBX/10f,PosLBY + PosHBY/10f,0.2f);
					}
					i++;
				}
			}
		}

		if(other.tag == "Fire")
		{
			SpeedX = 0;
			SpeedY = 48;
			Dead = true;
		}

	}



}
