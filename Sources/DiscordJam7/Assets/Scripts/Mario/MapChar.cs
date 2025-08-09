using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChar : MonoBehaviour {

	public bool Lerping;
	public bool DeadLerping;
	public int DeadReturnDelay;
	public float LerpTimer;
	public Vector2 StartLerp;
	public Vector2 EndLerp;
	public Vector2 Pos_Start;
	public Vector2 Pos_One;
	public Vector2 Pos_Two;
	public Vector2 Pos_Fort;
	public Vector2 Pos_RoadOne;
	public Vector2 Pos_RoadTwo;
	public Vector2 Pos_RoadThree;

	public bool Done;

	// Use this for initialization
	void OnEnable () {

		if(Main.DataHolder.MarioReturnToMap && !Main.DataHolder.MarioLevelClear)
		{
			//died.
			if(!Main.DataHolder.MarioLevel1Clear)
			{
				StartLerp = Pos_One;
				EndLerp = Pos_Start;
			}
			else if(!Main.DataHolder.MarioLevel2Clear)
			{
				StartLerp = Pos_Two;
				EndLerp = Pos_One;
			}
			else
			{
				StartLerp = Pos_Fort;
				EndLerp = Pos_Two;
			}
			DeadLerping = true;
			LerpTimer =0;
			DeadReturnDelay = 60;
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Main.DataHolder.MarioEnteringLevel)
		{
			return;
		}

		if(Lerping)
		{
			LerpTimer+= Time.fixedDeltaTime*3;
			if(LerpTimer >= 1)
			{
				Lerping = false;
				LerpTimer = 1;
			}

			float X = Mathf.Lerp(StartLerp.x,EndLerp.x,LerpTimer);
			float Y = Mathf.Lerp(StartLerp.y,EndLerp.y,LerpTimer);
			transform.position = new Vector3(X,Y,0);
		}

		if(DeadLerping)
		{
			DeadReturnDelay--;
			if(DeadReturnDelay <=0)
			{
				LerpTimer+= Time.fixedDeltaTime;
				if(LerpTimer >= 1)
				{
					DeadLerping = false;
					LerpTimer = 1;
				}

				float X = Mathf.Lerp(StartLerp.x,EndLerp.x,LerpTimer);
				float Y = Mathf.Lerp(StartLerp.y,EndLerp.y,LerpTimer);
				transform.position = new Vector3(X,Y,0);
			}
		}
		else
		{

			if(!Lerping)
			{
				if(Input.GetKey(KeyCode.Space))
				{
					// enter level if able.
					if(transform.position.x == -9 && transform.position.y == 3)
					{
						if(!Main.DataHolder.MarioLevel1Clear)
						{
							EnterLevel(0);
						}
					}
					else if(transform.position.x == -1)
					{
						if(!Main.DataHolder.MarioLevel2Clear)
						{
							EnterLevel(1);
						}
					}
					else if(transform.position.x == 7)
					{
						EnterLevel(2);
					}
				}

				else if(Input.GetKey(KeyCode.A))
				{
					if(transform.position.x == -9 && transform.position.y == -1)
					{
						BeginLerp(Pos_RoadOne,Pos_Start);
					}
					else if(transform.position.x == -5 && transform.position.y == 3)
					{
						BeginLerp(Pos_RoadTwo,Pos_One);
					}
					else if(transform.position.x == -1 && transform.position.y == 3)
					{
						BeginLerp(Pos_Two,Pos_RoadTwo);
					}
					else if(transform.position.x == 3 && transform.position.y == 3)
					{
						BeginLerp(Pos_RoadThree,Pos_Two);
					}
					else if(transform.position.x == 7 && transform.position.y == 3)
					{
						BeginLerp(Pos_Fort,Pos_RoadThree);
					}
				}
				else if(Input.GetKey(KeyCode.D))
				{
					if(transform.position.x == -13 && transform.position.y == -1)
					{
						BeginLerp(Pos_Start,Pos_RoadOne);
					}
					else if(transform.position.x == -9 && transform.position.y == 3 && Main.DataHolder.MarioLevel1Clear)
					{
						BeginLerp(Pos_One,Pos_RoadTwo);
					}
					else if(transform.position.x == -5 && transform.position.y == 3)
					{
						BeginLerp(Pos_RoadTwo,Pos_Two);
					}
					else if(transform.position.x == -1 && transform.position.y == 3&& Main.DataHolder.MarioLevel2Clear)
					{
						BeginLerp(Pos_Two,Pos_RoadThree);
					}
					else if(transform.position.x == 3 && transform.position.y == 3)
					{
						BeginLerp(Pos_RoadThree,Pos_Fort);
					}
				}
				else if(Input.GetKey(KeyCode.W))
				{
					if(transform.position.x == -9 && transform.position.y == -1)
					{
						BeginLerp(Pos_RoadOne,Pos_One);
					}

				}
				else if(Input.GetKey(KeyCode.S))
				{
					if(transform.position.x == -9 && transform.position.y == 3)
					{
						BeginLerp(Pos_One,Pos_RoadOne);
					}

				}



			}


		}


	}

	void EnterLevel(int id)
	{
		Main.DataHolder.MarioLevelID = id;
		Main.DataHolder.MarioEnteringLevel = true;
		Main.DataHolder.MarioEnterLevelTimer = 0;
	}

	void BeginLerp(Vector2 Spos, Vector2 DPos)
	{
		StartLerp = Spos;
		EndLerp = DPos;
		Lerping = true;
		LerpTimer = 0;
	}

}
