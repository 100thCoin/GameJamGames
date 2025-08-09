using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Move : MonoBehaviour {
	public bool Hexadecimal;

	public int PosLBX;
	public int PosHBX;

	public int PosLBY;
	public int PosHBY;

	public int SpeedX;
	public int SubpixelX;

	public int SpeedY;
	public int SubpixelY;

	public bool Held;

	public Char_Move HeldBy;
	public GameObject SelfWall;
	public GameObject SelfFloor;

	public Collider GrabCol;

	public bool InsideWall;
	public int InsideWallgrace;

	public bool Dead;

	public bool Disabled;

	public SpriteRenderer SR;

	public Collider[] Cols;

	public int GraceFrames;
	public int WallReturnGrace;

	public bool OnGround;
	public int OnGroundGraceTimer;


	public int PlayerColGrace;

	public DataHolder Main;

	public bool PhysBlock;
	public string PhysState;

	public GameObject SolidCollision;

	public float Zpos = 0.2f;

	// Use this for initialization
	void Start () {
		Main = GameObject.Find("Main").GetComponent<DataHolder>();
		if(PhysBlock)
		{
			Main.CrateObject = this;
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(Main.PseudoPause)
		{
			return;
		}

		if(PhysBlock)
		{
			SolidCollision.SetActive(PhysState == "Solid" || PhysState == "Idle");
		}

		if(OnGround)
		{
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

			if(PhysBlock)
			{

				if(Mathf.Abs(SpeedX) < 4)
				{
					if(PhysState != "Held" && PhysState != "Solid" && PhysState != "Idle")
					{
						PhysState = "Idle";
						Main.CrateState = "Idle";
						Main.CrateStateTimer = 0;
					}
				}
			}

		}

		if(Hexadecimal)
		{
			if(Held)
			{
				int PLeft  = HeldBy.SR.flipX ? -1:1;
				PosLBX = HeldBy.PosLBX;
				PosHBX = HeldBy.PosHBX + 16*PLeft;
				PosLBY = HeldBy.PosLBY;
				PosHBY = HeldBy.PosHBY + 8;

				if(PosHBX >=16)
				{
					PosLBX++;
					PosHBX-=16;
				}
				if(PosHBX <0)
				{
					PosLBX--;
					PosHBX+=16;
				}



				transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f,Zpos);

			}
			else if(!Dead)
			{
				SubpixelX += SpeedX;

				while(SubpixelX >= 10)
				{
					SubpixelX -= 10;
					PosHBX++;
				}
				while(SubpixelX < 0)
				{
					SubpixelX += 10;
					PosHBX--;
				}
				if(PhysState != "Solid")
				{
					SpeedY -= 2;
				}
				else
				{
					SpeedY = 0;
				}
				if(Hexadecimal)
				{
					if(SpeedY < -36)
					{
						SpeedY = -36;
					}
					SubpixelY += SpeedY;
				}
				while(SubpixelY > 10)
				{
					SubpixelY -= 10;
					PosHBY++;
				}
				while(SubpixelY < 0)
				{
					SubpixelY += 10;
					PosHBY--;
				}
				transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f,Zpos);

			}
			else if(Dead)
			{
				SpeedY -= 1;

				if(Hexadecimal)
				{
					if(SpeedY < -36)
					{
						SpeedY = -36;
					}
					SubpixelY += SpeedY;
				}
				while(SubpixelY >= 10)
				{
					SubpixelY -= 10;
					PosHBY++;
				}
				while(SubpixelY < 0)
				{
					SubpixelY += 10;
					PosHBY--;
				}
				transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f,Zpos);

			}
		}


		if(InsideWallgrace < 0)
		{
			InsideWall = false;
		}
		InsideWallgrace--;
		if(OnGroundGraceTimer < 0)
		{
			OnGround = false;
		}
		if(WallReturnGrace <0 && !Held)
		{
			if(SelfWall != null)
			{
				SelfWall.transform.localPosition = new Vector3(0,0,0);
			}
			if(SelfFloor != null)
			{
				SelfFloor.transform.localPosition = new Vector3(0,0,0);
			}
		}
		GraceFrames--;
		OnGroundGraceTimer--;
		WallReturnGrace--;
		PlayerColGrace--;
	}



	void OnTriggerStay(Collider other)
	{
		if(Dead)
		{
			return;
		}


		if(other.tag == "Player" && !Held && GraceFrames <0 && PlayerColGrace < 0)//wait hold up, is playerColGrace the same as regular grace?. oh well.
		{

			SpeedX = 0;
			
		}

		if(other.tag == "Ground" && !Held)
		{
				OnGround = true;
				OnGroundGraceTimer = 2;
				if(transform.position.y > other.transform.position.y + 1.2f)
				{
					SpeedY = 0;
					int i = 0;
					while(transform.position.y < other.transform.position.y+2 && i < 4)
					{
						PosHBY++;
						if(Hexadecimal)
						{
							if(PosHBY >=16)
							{
								PosHBY -= 16;
								PosLBY++;
							}
							//print(transform.position.y + " before touchground");
							transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f,Zpos);
							//print(transform.position.y + " after touchground");

						}
						i++;
					}
				}

		}

		if(other.tag == "Ceiling" && !Held)
		{
				if(transform.position.y < other.transform.position.y + 0.5f && SpeedY > 0)
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
							transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f,Zpos);
						}
						else
						{
							if(PosHBY <0)
							{
								PosHBY = 9;
								PosLBY--;
							} 
							transform.position = new Vector3(PosLBX + PosHBX/10f,PosLBY + PosHBY/10f,Zpos);
						}
						i++;
					}
				}
		}


		if(other.tag == "Wall")
		{
			if(Held)
			{
				InsideWall = true;
				InsideWallgrace = 1;
			}

			if(transform.position.x < other.transform.position.x)
			{
				int i = 0;
				while(transform.position.x > other.transform.position.x -1 && i < 4)
				{
					PosHBX--;
					if(Hexadecimal)
					{
						if(PosHBX <0)
						{
							PosHBX = 15;
							PosLBX--;

						}
						transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f,Zpos);
					}
					else
					{
						if(PosHBX <0)
						{
							PosHBX = 9;
							PosLBX--;

						}
						transform.position = new Vector3(PosLBX + PosHBX/10f,PosLBY + PosHBY/10f,Zpos);
					}
					i++;
				}
			}
			if(transform.position.x > other.transform.position.x)
			{
				int i = 0;
				while(transform.position.x < other.transform.position.x +1 && i < 4)
				{
					PosHBX++;
					if(Hexadecimal)
					{
						if(PosHBX >=16)
						{
							PosHBX = 0;
							PosLBX++;

						}

						transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f,Zpos);
					}
					else
					{
						if(PosHBX >=10)
						{
							PosHBX = 0;
							PosLBX++;

						}

						transform.position = new Vector3(PosLBX + PosHBX/10f,PosLBY + PosHBY/10f,Zpos);
					}
					i++;
				}
			}
				SpeedX = 0;

			if(PhysBlock && PhysState != "Solid" && PhysState != "Held")
			{
				PhysState = "Solid";
				Main.CrateState = "Solid";
				Main.CrateStateTimer = 0;
			}

		}
		

		if(other.tag == "Fire" && !Held)
		{
			Dead = true;
			SR.flipY = true;
			SpeedY = 32;
			Cols[0].enabled = false;
			if(SelfWall != null){Destroy(SelfWall.gameObject);}
			if(SelfFloor != null){ Destroy(SelfFloor.gameObject);}
			gameObject.layer = 11;
		}

	}

}
