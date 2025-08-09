using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour {
	public DataHolder Main;

	public float Speed;
	public float JumpHeight;
	public bool Dead;
	public float Gravity;
	public int WalkDir;
	public bool DetectingLadder;
	public bool OnLadder;

	public bool Jumping;
	public bool OnGround;
	public Vector3 JumpVel;
	public Vector3 JumpInit;

	public SpriteRenderer SR;
	public Animator Anim;
	public Sprite Jump;
	public RuntimeAnimatorController Idle;
	public RuntimeAnimatorController Walk;
	public RuntimeAnimatorController Balance;
	public Sprite Climb;

	public DetectJumped DJ;
	public bool DetectedJumpedOver;

	public int jumpTimer; //used to prevent the early score bug

	public CameraMove CamMov;

	public float stepTimer;
	public GameObject StepSound;
	public GameObject JumpSound;
	public float LadderXPos;
	public float LadderYPos;
	public float LadderLossyScale;
	public float ClimbFlipTimer;
	public bool Indoors;

	public int WallDir;

	public bool Hammering;
	public float HammerTimer;
	public float SwingTimer;
	public GameObject HammerHold;
	public SpriteRenderer HammerSprite;
	public bool HammerSwing;

	public GameObject HammerMusic;
	public GameObject SpawnedHammerMusic;

	public GameObject PointBubble;
	public GameObject PointJingle;
	public Sprite Points100;
	public Sprite Points300;
	public Sprite Points500;
	public Sprite Points800;

	public RuntimeAnimatorController DeadAnim;

	public bool YOUWIN;
	public Transform VictoryPoint;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(YOUWIN)
		{
			transform.position = VictoryPoint.transform.position;
			SR.flipX = true;
			return;

		}



		if(Dead)
		{
			if(SpawnedHammerMusic != null)
			{
				Destroy(SpawnedHammerMusic);
			}

		}

		if(!Dead && Main.FreezeTimer <=0)
		{
			if(Input.GetKeyDown(KeyCode.Space) && OnGround)
			{
				Jumping = true;
				OnGround = false;
				JumpVel = JumpInit;
				WalkDir = 0;
				if(Input.GetKey(KeyCode.A))
				{
					WalkDir -= 1;
				}
				if(Input.GetKey(KeyCode.D))
				{
					WalkDir += 1;
				}
				if(WallDir == WalkDir)
				{
					WalkDir = 0;
				}
				JumpVel += new Vector3(WalkDir*Speed,0,0);
				Anim.runtimeAnimatorController = null;
				SR.sprite = Jump;
				Instantiate(JumpSound,transform.position,transform.rotation);
				DJ.JumpOrigin = transform.position;
			}

			if(!OnLadder)
			{
				WalkDir = 0;
				if(Input.GetKey(KeyCode.A))
				{
					WalkDir -= 1;
				}
				if(Input.GetKey(KeyCode.D))
				{
					WalkDir += 1;
				}
				if(WallDir == WalkDir)
				{
					WalkDir = 0;
				}
				if(WalkDir != 0 && !Jumping)
				{
					stepTimer -= Time.deltaTime;
					if(stepTimer <= 0)
					{
						stepTimer += 0.2f;
						Instantiate(StepSound,transform.position,transform.rotation,transform);
					}
				}
			}
			else
			{
				WalkDir = 0;
				if(Input.GetKey(KeyCode.W))
				{
					WalkDir -= 1;
				}
				if(Input.GetKey(KeyCode.S))
				{
					WalkDir += 1;
				}

				if(WalkDir != 0)
				{
					stepTimer -= Time.deltaTime*0.5f;
					if(stepTimer <= 0)
					{
						stepTimer += 0.2f;
						Instantiate(StepSound,transform.position,transform.rotation,transform);
					}
				}
			}

		}
	}

	void FixedUpdate () {


		if(YOUWIN)
		{
			return;
		}


		if(!Dead && Main.FreezeTimer <=0)
		{
			if(DetectingLadder && !OnLadder)
			{
				if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
				{
					if(!(Input.GetKey(KeyCode.W) && transform.position.y > LadderYPos +LadderLossyScale*0.5f))
					{
						OnLadder = true;
						Jumping = false;
						transform.position = new Vector3(LadderXPos,transform.position.y,transform.position.z);
						Anim.runtimeAnimatorController = null;
						SR.sprite = Climb;
					}
				}
			}

			if(OnLadder && !DetectingLadder)
			{
				OnLadder = false;
			}


			if(transform.position.y < -9 && transform.position.y > -20)
			{
				transform.position = new Vector3(transform.position.x,-7,0);
			}

			if(!Jumping)
			{
				



				if(!OnLadder)
				{
					WalkDir = 0;
					if(Input.GetKey(KeyCode.A))
					{
						WalkDir -= 1;
					}
					if(Input.GetKey(KeyCode.D))
					{
						WalkDir += 1;
					}
					if(WallDir == WalkDir)
					{
						WalkDir = 0;
					}
					if(WalkDir != 0)
					{
						Anim.runtimeAnimatorController = Walk;
						SR.flipX = WalkDir == -1;
						transform.position += new Vector3(WalkDir*Speed,0,0);
					}
					else
					{
						Anim.runtimeAnimatorController = Idle;
					}
					if(!OnGround)
					{
						Jumping = true;
						WalkDir = 0;
						if(Input.GetKey(KeyCode.A))
						{
							WalkDir -= 1;
						}
						if(Input.GetKey(KeyCode.D))
						{
							WalkDir += 1;
						}
						if(WallDir == WalkDir)
						{
							WalkDir = 0;
						}
						JumpVel = new Vector3(WalkDir*Speed,0,0);
						Anim.runtimeAnimatorController = null;
						SR.sprite = Jump;
					}

				}
				else // on ladder
				{
					WalkDir = 0;
					if(Input.GetKey(KeyCode.S))
					{
						WalkDir -= 1;
					}
					if(Input.GetKey(KeyCode.W))
					{
						WalkDir += 1;
					}
					if(WalkDir != 0)
					{
						ClimbFlipTimer -= Time.fixedDeltaTime;
						if(ClimbFlipTimer <= 0)
						{
							ClimbFlipTimer += 0.2f;
							SR.flipX = !SR.flipX;
						}

						transform.position += new Vector3(0,WalkDir*Speed*0.6f,0);
					}
				}
			}
			else // jumping
			{
				WalkDir = 0;
				if(Input.GetKey(KeyCode.A))
				{
					WalkDir -= 1;
				}
				if(Input.GetKey(KeyCode.D))
				{
					WalkDir += 1;
				}

				if(WallDir == WalkDir)
				{
					WalkDir = 0;
				}

				if(WalkDir != 0)
				{

					JumpVel += new Vector3(WalkDir*0.1f*Speed,0,0);
					if(JumpVel.x > Speed)
					{
						JumpVel = new Vector3(Speed,JumpVel.y,0);
					}
					else if(JumpVel.x < -Speed)
					{
						JumpVel = new Vector3(-Speed,JumpVel.y,0);
					}
				}

				transform.position += JumpVel;
				if(JumpVel.y < 0 && !DetectedJumpedOver && jumpTimer > 20)
				{
					jumpTimer = -1000;
					DetectedJumpedOver = true;
					DJ.Active = true;
				}
				if(JumpVel.y > -Speed)
				{
					JumpVel -= new Vector3(0,Gravity,0);
				}
				jumpTimer++;

			}
		}

		if(Hammering && !Dead)
		{
			HammerHold.transform.position = transform.position;
			HammerHold.SetActive(true);
			HammerTimer -= Time.fixedDeltaTime;

			if(HammerTimer <= 0)
			{
				Hammering = false;
				Main.Music.pitch = 1;
				Destroy(SpawnedHammerMusic);
			}

			SwingTimer += Time.fixedDeltaTime;
			if(SwingTimer > 0.15f)
			{
				HammerSwing = !HammerSwing;
				SwingTimer -= 0.15f;
			}

			if(HammerSwing)
			{
				HammerSprite.flipX = !SR.flipX;

				if(SR.flipX)
				{
					HammerHold.transform.eulerAngles = new Vector3(0,0,90);

				}
				else
				{
					HammerHold.transform.eulerAngles = new Vector3(0,0,-90);

				}
			}
			else
			{
				HammerSprite.flipX = !SR.flipX;
				HammerHold.transform.eulerAngles = Vector3.zero;
			}

		}
		else
		{
			HammerHold.SetActive(false);
		}



		OnGround = false;
		DetectingLadder = false;
		WallDir = 0;
		CamMov.Move();
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Ground" && JumpVel.y <= 0 && !OnLadder)
		{
			if(transform.position.y > other.transform.position.y + other.transform.lossyScale.y*0.4f)
			{
				transform.position = new Vector3(transform.position.x,other.transform.position.y + other.transform.lossyScale.y*0.5f + 0.5f,transform.position.z);
				Jumping = false;
				OnGround = true;
				jumpTimer = 0;
				DetectedJumpedOver = false;
			}
		}

		if(other.tag == "Ground" && OnLadder && transform.position.y < LadderYPos)
		{
			if(transform.position.y > other.transform.position.y + other.transform.lossyScale.y*0.4f)
			{
				OnLadder = false;
				transform.position = new Vector3(transform.position.x,other.transform.position.y + other.transform.lossyScale.y*0.5f + 0.5f,transform.position.z);
				Jumping = false;
				OnGround = true;
				jumpTimer = 0;
				DetectedJumpedOver = false;
			}
		}

		if(other.tag == "GroundZero" && JumpVel.y <= 0) //exists so ladders can pass through floors, but not ground zero
		{
			if(transform.position.y > other.transform.position.y + other.transform.lossyScale.y*0.4f)
			{
				transform.position = new Vector3(transform.position.x,other.transform.position.y + other.transform.lossyScale.y*0.5f + 0.5f,transform.position.z);
				Jumping = false;
				OnGround = true;
				jumpTimer = 0;
				DetectedJumpedOver = false;
				OnLadder = false;

			}
		}

		if(other.tag == "KillZone")
		{
			Main.FreezeUntilFalse = true;
			Dead = true;
		}

		if(other.tag == "Ladder")
		{
			DetectingLadder = true;
			LadderXPos = other.transform.position.x;
			LadderYPos = other.transform.position.y;
			LadderLossyScale = 2;
		}

		if(other.tag == "Wall")
		{
			WallDir = other.transform.position.x > transform.position.x ? 1 : -1;
			JumpVel = new Vector3(0,JumpVel.y,0);
		}


		if(other.tag == "HammerItem" && !Hammering)
		{
			Hammering = true;
			HammerTimer = 8;
			Destroy(other.gameObject);

			SpawnedHammerMusic = Instantiate(HammerMusic,transform.position,transform.rotation);

			Main.Music.pitch = 0;

		}

		if(other.tag == "Item")
		{


			GameObject SpawnedBubble = Instantiate(PointBubble,transform.position,transform.rotation);
			Instantiate(PointJingle,transform.position,transform.rotation);

			int Score = 500;
			int i = Main.ItemsCollected;
			switch(i)
			{
				default: Score = 800; break;
				case 0: Score = 300; break;
				case 1: Score = 500; break;
				case 2: Score = 800; break;
				
			}
			Main.ItemsCollected++;
			switch(Score)
			{
			case 100: SpawnedBubble.GetComponent<SpriteRenderer>().sprite = Points100; break;
			case 300: SpawnedBubble.GetComponent<SpriteRenderer>().sprite = Points300; break;
			case 500: SpawnedBubble.GetComponent<SpriteRenderer>().sprite = Points500; break;
			case 800: SpawnedBubble.GetComponent<SpriteRenderer>().sprite = Points800; break;
			}
			Main.Score += Score;
			Destroy(other.gameObject);

		}



	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "win")
		{
			YOUWIN = true;
			Destroy(other.gameObject);
			Main.YouWin = true;
		}


	}


}
