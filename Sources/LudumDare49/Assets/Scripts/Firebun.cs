using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebun : MonoBehaviour {

	public SpriteRenderer SR;
	public DataHolder Main;
	public int WalkDir;
	public bool ClimbLadder;
	public float XDist;
	public float YDist;

	public float Speed;

	public bool Patrol;

	public float PatrolTimer;

	public bool RunLeft;
	public bool RunRight;

	public int LadderDir;
	public bool ContactLadder;

	public Vector3 JumpVel;
	public float LadderYPos;

	public bool Jumping;
	public bool Falling;
	public bool OnGround;
	public float Gravity;

	public Sprite PBubble;
	public GameObject Points;
	public GameObject PointsJingle;
	public GameObject Kaboom;

	public bool DieOnce;

	// Use this for initialization
	void Start () {
	
		Main = GameObject.Find("Main").GetComponent<DataHolder>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(transform.position.x < -30)
		{
			Destroy(gameObject);
		}

		if(Main.DestroyAllSprites)
		{
			Destroy(gameObject);
		}

		if(Main.FreezeUntilFalse)
		{
			return;
		}

		XDist = Mathf.Abs(transform.position.x - Main.P.transform.position.x);
		YDist = Mathf.Abs(transform.position.y - Main.P.transform.position.y);

		if(!ClimbLadder)
		{
			if(transform.position.x > 12)
			{
				RunLeft = true;
			}
			if(transform.position.x < 9)
			{
				RunLeft = false;
			}

			if(transform.position.x < -10)
			{
				RunRight = true;
			}
			if(transform.position.x > -6)
			{
				RunRight = false;
			}

			if(YDist < 1.5f && XDist < 3)
			{
				Patrol = false;
				if(XDist > 0.3f)
				{
					WalkDir = transform.position.x > Main.P.transform.position.x ? -1 : 1;
				}
				else
				{
					WalkDir = 0;
				}
			}
			else
			{
				if(!Patrol)
				{
					Patrol = true;
					WalkDir = 0;
				}
			}

			if(Patrol)
			{
				PatrolTimer-=Time.fixedDeltaTime;
				if(PatrolTimer < 0)
				{
					PatrolTimer += Random.Range(0.5f,3f);
					WalkDir = Random.Range(-1,2);
				}
			}

			if(RunLeft)
			{
				WalkDir = -1;
			}
			if(RunRight)
			{
				WalkDir = 1;
			}

			if(WalkDir != 0)
			{
				SR.flipX = WalkDir == 1;
			}

			if(!OnGround)
			{
				if(!Falling)
				{
					Falling = true;
					JumpVel = new Vector3(WalkDir*Speed,0,0);
				}
				transform.position += JumpVel;
				JumpVel -= new Vector3(0,Gravity,0);
			}


		}
		else //climbing ladder
		{

			transform.position += new Vector3(0,LadderDir * Speed *0.7f,0);


		}



		if(!ClimbLadder && OnGround)
		{
			transform.position += new Vector3(WalkDir * Speed,0,0);
		}

		if(!ContactLadder && ClimbLadder)
		{

			ClimbLadder = false;

		}

		ContactLadder = false;
		OnGround = false;

	}




	void OnTriggerEnter(Collider other)
	{




		if(other.tag == "Ladder" || other.tag == "Scaffold" || other.tag == "Scaffoldtop")
		{
			if(YDist >0 && other.transform.position.y > transform.position.y)
			{
				ClimbLadder = true;
				LadderDir = 1;
				ContactLadder = true;
				transform.position = new Vector3(other.transform.position.x,transform.position.y,0);
			}
			else if (YDist <0 && other.transform.position.y < transform.position.y)
			{
				ClimbLadder = true;
				LadderDir = -1;
				ContactLadder = true;
				transform.position = new Vector3(other.transform.position.x,transform.position.y,0);

			}
			else // non- gaurunteed climb
			{

				int r = Random.Range(0,4);
				bool DoClimb = r == 1;

				if(DoClimb)
				{
					if(other.transform.position.y > transform.position.y)
					{
						ClimbLadder = true;
						LadderDir = 1;
						ContactLadder = true;
						transform.position = new Vector3(other.transform.position.x,transform.position.y,0);

					}
					else
					{
						ClimbLadder = true;
						LadderDir = -1;
						ContactLadder = true;
						transform.position = new Vector3(other.transform.position.x,transform.position.y,0);

					}
				}
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if((other.tag == "Ladder" || other.tag == "Scaffold" || other.tag == "Scaffoldtop") && ClimbLadder)
		{
			ContactLadder = true;
		}

		if(other.tag == "Ground" && JumpVel.y <= 0 && !ClimbLadder)
		{
			if(transform.position.y > other.transform.position.y + other.transform.lossyScale.y*0.4f)
			{
				transform.position = new Vector3(transform.position.x,other.transform.position.y + other.transform.lossyScale.y*0.5f + 0.5f,transform.position.z);
				Jumping = false;
				OnGround = true;
				JumpVel = new Vector3(0,0,0);
				Falling = false;

			}
		}

		if(other.tag == "Ground" && ClimbLadder && transform.position.y < LadderYPos)
		{
			if(transform.position.y > other.transform.position.y + other.transform.lossyScale.y*0.4f)
			{
				ClimbLadder = false;
				transform.position = new Vector3(transform.position.x,other.transform.position.y + other.transform.lossyScale.y*0.5f + 0.5f,transform.position.z);
				Jumping = false;
				OnGround = true;
				JumpVel = new Vector3(0,0,0);
				Falling = false;

			}
		}

		if(other.tag == "GroundZero" && JumpVel.y <= 0) //exists so ladders can pass through floors, but not ground zero
		{
			if(transform.position.y > other.transform.position.y + other.transform.lossyScale.y*0.4f)
			{
				transform.position = new Vector3(transform.position.x,other.transform.position.y + other.transform.lossyScale.y*0.5f + 0.5f,transform.position.z);
				Jumping = false;
				OnGround = true;
				ClimbLadder = false;
				JumpVel = new Vector3(0,0,0);
				Falling = false;
			}
		}


		if(other.tag == "Hammer" && !DieOnce)
		{
			DieOnce = true;

			Instantiate(Kaboom,transform.position,transform.rotation);
			GameObject SpawnedBubble = Instantiate(Points,transform.position + new Vector3(0,0.5f,0), transform.rotation);
			Instantiate(PointsJingle,transform.position + new Vector3(0,0.5f,0), transform.rotation);

			SpawnedBubble.transform.position *= 16;
			SpawnedBubble.transform.position = new Vector3(Mathf.Round(SpawnedBubble.transform.position.x),Mathf.Round(SpawnedBubble.transform.position.y),Mathf.Round(SpawnedBubble.transform.position.z));
			SpawnedBubble.transform.position /= 16;
			Main.FireballsPresent--;


			int Score = 500;
			SpawnedBubble.GetComponent<SpriteRenderer>().sprite = PBubble;

			Main.Score += Score;
			Main.NewestScore = Score;
			Destroy(gameObject);

		}
	}

}
