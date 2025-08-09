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

	public bool UFO;
	public int UFOKillCount;

	public int PlayerColGrace;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(UFO && Dead)
		{
			Cols[0].enabled = false;


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
		}

		if(Hexadecimal)
		{
			if(Held)
			{
				int PLeft  = HeldBy.SR.flipX ? -1:1;
				PosLBX = HeldBy.PosLBX;
				PosHBX = HeldBy.PosHBX + 8*PLeft;
				PosLBY = HeldBy.PosLBY;
				PosHBY = HeldBy.PosHBY + 4;

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
				if(PosLBX>=16)
				{
					PosLBX = 0;
				}
				if(PosLBX<0)
				{
					PosLBX = 15;
				}

				if(PosHBY >=16)
				{
					PosLBY++;
					PosHBY-=16;
				}
				if(PosLBY>=16)
				{
					PosLBY = 0;
				}

				transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f,0.2f);

			}
			else if(!Dead)
			{
				SubpixelX += SpeedX;

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
				if(!UFO)
				{
					SpeedY -= 2;
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
					if(PosHBY >15)
					{
						PosHBY = 0;
						PosLBY++;
						if(PosLBY >15)
						{
							PosLBY = 0;
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
						}
					}
				}
				transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f,0.2f);

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
					if(PosHBY >15)
					{
						PosHBY = 0;
						PosLBY++;
						if(PosLBY >15)
						{
							PosLBY = 15;
							PosHBY = 15;
							Disabled = true;
							gameObject.SetActive(false);
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
						if(PosLBY <=-1)
						{
							PosLBY = 0;
							PosHBY = 0;
							Disabled = true;
							gameObject.SetActive(false);
						}
					}
				}
				transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f,0.2f);

			}
		}
		else ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			if(Held)
			{
				int PLeft  = HeldBy.SR.flipX ? -1:1;
				PosLBX = HeldBy.PosLBX;
				PosHBX = HeldBy.PosHBX + 5*PLeft;
				PosLBY = HeldBy.PosLBY;
				PosHBY = HeldBy.PosHBY + 2;

				if(PosHBX >=10)
				{
					PosLBX++;
					PosHBX-=10;
				}
				if(PosHBX <0)
				{
					PosLBX--;
					PosHBX+=10;
				}
				if(PosLBX>=10)
				{
					PosLBX = 0;
				}
				if(PosLBX<0)
				{
					PosLBX = 9;
				}

				if(PosHBY >=10)
				{
					PosLBY++;
					PosHBY-=10;
				}
				if(PosLBY>=10)
				{
					PosLBY = 0;
				}

				transform.position = new Vector3(PosLBX + PosHBX/10f,PosLBY + PosHBY/10f,0.2f);

			}
			else if(!Dead)
			{
				SubpixelX += SpeedX;

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
				SpeedY -= 2;


				if(SpeedY < -22)
				{
					SpeedY = -22;
				}
					SubpixelY += SpeedY;
				while(SubpixelY > 10)
				{
					SubpixelY -= 10;
					PosHBY++;
					if(PosHBY >9)
					{
						PosHBY = 0;
						PosLBY++;
						if(PosLBY >9)
						{
							PosLBY = 0;
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
						}
					}
				}
				transform.position = new Vector3(PosLBX + PosHBX/10f,PosLBY + PosHBY/10f,0.2f);

			}
			else if(Dead)
			{
				SpeedY -= 1;


					if(SpeedY < -22)
					{
						SpeedY = -22;
					}
					SubpixelY += SpeedY;

					while(SubpixelY >= 10)
					{
						SubpixelY -= 10;
						PosHBY++;
						if(PosHBY >9)
						{
							PosHBY = 0;
							PosLBY++;
							if(PosLBY >9)
							{
								PosLBY = 9;
								PosHBY = 9;

								Disabled = true;
								gameObject.SetActive(false);
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
							if(PosLBY <=-1)
							{
							PosLBY = 0;
							PosHBY = 0;

								Disabled = true;
								gameObject.SetActive(false);
							}
						}
					}
					transform.position = new Vector3(PosLBX + PosHBX/10f,PosLBY + PosHBY/10f,0.2f);

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
		if(other.tag == "Wall")
		{
			if(Held)
			{
				InsideWall = true;
				InsideWallgrace = 1;
			}
			else if(!UFO)
			{
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
								if(PosLBX <0)
								{
									PosLBX = 15;
								}
							}
							transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f,0.2f);
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
							transform.position = new Vector3(PosLBX + PosHBX/10f,PosLBY + PosHBY/10f,0.2f);
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
								if(PosLBX >=16)
								{
									PosLBX = 0;
								}
							}

							transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f,0.2f);
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

							transform.position = new Vector3(PosLBX + PosHBX/10f,PosLBY + PosHBY/10f,0.2f);
						}
						i++;
					}
				}
				SpeedX = 0;
			}
			else
			{
				if(transform.position.x > other.transform.position.x)
				{
					SpeedX = Mathf.Abs(SpeedX); 
				}
				else
				{
					SpeedX = -Mathf.Abs(SpeedX); 
				}
			}
		}

		if(other.tag == "Player" && !Held && GraceFrames <0 && !UFO && PlayerColGrace < 0)//wait hold up, is playerColGrace the same as regular grace?. oh well.
		{

			SpeedX = 0;
			
		}

		if(other.tag == "Ground" && !Held)
		{
			if(!UFO)
			{
				OnGround = true;
				OnGroundGraceTimer = 2;
				if(transform.position.y > other.transform.position.y + 0.5f)
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
							transform.position = new Vector3(PosLBX + PosHBX/16f,PosLBY + PosHBY/16f,0.2f);
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
							transform.position = new Vector3(PosLBX + PosHBX/10f,PosLBY + PosHBY/10f,0.2f);
						}
						i++;
					}
				}
			}
			else
			{
				SpeedY = Mathf.Abs(SpeedY);
			}
		}

		if(other.tag == "Ceiling" && !Held)
		{
			if(!UFO)
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
			else
			{
				SpeedY = -Mathf.Abs(SpeedY);
			}

		}

		if(other.tag == "Holdable" && UFO && !Held)
		{
			Object_Move OtherMove = other.GetComponent<Object_Move>();
			if(!OtherMove.Held && !OtherMove.Dead)
			{
				if(OtherMove.SpeedX != 0 || Mathf.Abs(OtherMove.SpeedY) >= 6)
				{
					Dead = true;
					SR.flipY = true;
					SpeedY = 48;
					Cols[0].enabled = false;
					gameObject.layer = 11;

				}
				else
				{
					OtherMove.Dead = true;
					OtherMove.SR.flipY = true;
					OtherMove.SpeedY = 48;
					OtherMove.gameObject.layer = 11;
					if(OtherMove.SelfWall != null){Destroy(OtherMove.SelfWall.gameObject);}
					if(OtherMove.SelfFloor != null){ Destroy(OtherMove.SelfFloor.gameObject);}
				}
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
