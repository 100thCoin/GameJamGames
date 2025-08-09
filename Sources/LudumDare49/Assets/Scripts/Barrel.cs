using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour {

	public float PhaseTimer;
	int Phase;
	public SpriteRenderer SR;
	public Vector3[] PhaseRots;
	public Sprite BarrelTop;
	public Sprite[] BarrelSides;
	public int Dir = 1;
	public bool FrontFacing;

	public bool OnGround;
	public bool OnLadder;
	public Vector3 JumpVel;
	public float Gravity;

	public int Movedir =1;
	public float speed;

	public bool Falling;

	public DataHolder Main;

	public bool DoneFirstRoll;

	public float LadderHeight;

	public int LastDir;

	public bool PigentyFigmentyCrosserTosser;

	public bool TinyHop;

	public float LastDirChange;

	public GameObject Points;
	public GameObject Kaboom;
	public GameObject ScoreSound;

	public int Score;
	public bool BlueBarrel;

	public Sprite score100;
	public Sprite score300;
	public Sprite score500;
	public Sprite score800;

	public bool GZeroed;

	public GameObject Firebun;

	public bool DieOnce;

	// Use this for initialization
	void Start () {
		Main = GameObject.Find("Main").GetComponent<DataHolder>();

		PigentyFigmentyCrosserTosser = Random.Range(0,4) == 1;

		if(PigentyFigmentyCrosserTosser)
		{
			Falling = true;
			JumpVel = new Vector3(speed*Movedir*0.75f *Random.Range(0.3f,2.75f),Random.Range(0.1f,0.25f),0);
			FrontFacing = true;
			OnLadder = false;
			TinyHop = false;
		}

	}
	
	// Update is called once per frame
	void Update () {

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

		if(PigentyFigmentyCrosserTosser)
		{
			Falling = true;
			FrontFacing = true;
			OnLadder = false;
		}
		PhaseTimer += Time.deltaTime*Dir*2;

		if(PhaseTimer > 1 && Dir == 1)
		{
			PhaseTimer -= 1;
		}
		else if(PhaseTimer < 0 && Dir == -1)
		{
			PhaseTimer += 1;
		}
		Phase = Mathf.RoundToInt(PhaseTimer*4 -0.5f);
		if(Phase == -1)
		{
			Phase = 0;
		}
		if(!FrontFacing)
		{
			SR.transform.eulerAngles = PhaseRots[Phase];
			SR.sprite = BarrelTop;
		}
		else
		{
			SR.transform.eulerAngles = PhaseRots[0];
			SR.sprite = BarrelSides[Phase];
		}

	}

	void FixedUpdate()
	{

		if(Main.DestroyAllSprites)
		{
			Destroy(gameObject);
		}

		if(Main.FreezeUntilFalse)
		{
			return;
		}


		if(OnLadder)
		{
			transform.position += new Vector3(0,-1 * speed * 0.7f,0);
			FrontFacing = true;
			JumpVel = new Vector3(0,-1 * speed * 0.7f,0);
			TinyHop = false;
		}
		else
		{

			if(!OnGround)
			{
				if(!Falling)
				{
					Falling = true;
					if(JumpVel.y > -0.01f)
					{
						JumpVel = new Vector3(speed*Movedir*0.75f,0,0);
					}
				}

				transform.position += 	JumpVel;
				JumpVel = new Vector3(JumpVel.x,JumpVel.y - Gravity,0);
				if(TinyHop)
				{
					JumpVel = new Vector3(JumpVel.x,JumpVel.y - Gravity*2,0);

				}
			}
			else if(!OnLadder)
			{
				transform.position += new Vector3(Movedir * speed,0,0);

			}
		}


		OnLadder = false;
		OnGround = false;
	}



	void OnTriggerStay(Collider other)
	{
		if(!PigentyFigmentyCrosserTosser)
		{
			if(other.tag == "Ground" && JumpVel.y <= 0 && !FrontFacing)
			{
				if(transform.position.y > other.transform.position.y + other.transform.lossyScale.y*0.4f)
				{

					transform.position = new Vector3(transform.position.x,other.transform.position.y + other.transform.lossyScale.y*0.5f + 0.4f,transform.position.z);
					OnGround = true;
					JumpVel = Vector3.zero;
					Falling = false;

				}
			}

			if(other.tag == "GroundZero" && JumpVel.y <= 0) 
			{
				if(transform.position.y > other.transform.position.y + other.transform.lossyScale.y*0.4f)
				{
					transform.position = new Vector3(transform.position.x,other.transform.position.y + other.transform.lossyScale.y*0.5f + 0.4f,transform.position.z);
					OnGround = true;
					JumpVel = Vector3.zero;
					Movedir = -1;
					Falling = false;
					FrontFacing = false;

				}
			}

			if(other.tag == "Ladder") 
			{
				if(FrontFacing)
				{
					OnLadder = true;
					//LadderHeight = other.transform.position.y;
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(!PigentyFigmentyCrosserTosser)
		{
			if((other.tag == "Ground" || other.tag == "GroundZero") && JumpVel.y < 0 && !OnLadder && Falling)
			{
				if(transform.position.y > other.transform.position.y + other.transform.lossyScale.y*0.4f)
				{

					transform.position = new Vector3(transform.position.x,other.transform.position.y + other.transform.lossyScale.y*0.5f + 0.4f,transform.position.z);

					FrontFacing = false;
					if(TinyHop)
					{
						OnGround = true;
						JumpVel = Vector3.zero;
						Falling = false;
						TinyHop = false;

					}
					else
					{
						TinyHop = true;
						if(DoneFirstRoll)
						{
							if(other.tag == "GroundZero")
							{
								GZeroed = true;
								Movedir = -1;
							}
							else
							{
								if(transform.position.y < LastDirChange-2)
								{
									Movedir *= -1;
									if(Movedir == 0)
									{
										Movedir = -LastDir;
									}
									LastDirChange = transform.position.y;
								}
							}
						}
						JumpVel = new Vector3(Movedir*speed,0.1f,0);
					}

					DoneFirstRoll = true;
				}
			}
			if(other.tag == "Ground" && OnLadder && transform.position.y < LadderHeight)
			{
				if(transform.position.y > other.transform.position.y + other.transform.lossyScale.y*0.4f)
				{
					transform.position = new Vector3(transform.position.x,other.transform.position.y + other.transform.lossyScale.y*0.5f + 0.4f,transform.position.z);
					OnGround = true;
					JumpVel = Vector3.zero;
					Falling = false;
					if(DoneFirstRoll)
					{
						Movedir *= -1;
						if(Movedir == 0)
						{
							Movedir = -LastDir;
						}
					}
					DoneFirstRoll = true;
					OnLadder = false;
					FrontFacing = false;
				}
			}
			if(!GZeroed)
			{
			if(other.tag == "Ladder" && transform.position.y > other.transform.position.y)
			{
				//barrel steering
				if(transform.position.x < Main.P.transform.position.x && Main.P.WalkDir == -1)
				{
					OnLadder = true;
					transform.position = new Vector3(other.transform.position.x,transform.position.y,0);
					if(Movedir != 0)
					{
						LastDir = Movedir;
					}
					LadderHeight = other.transform.position.y;
					Movedir = 0;
				}
				else if(transform.position.x > Main.P.transform.position.x && Main.P.WalkDir == 1)
				{
					OnLadder = true;
					transform.position = new Vector3(other.transform.position.x,transform.position.y,0);
					if(Movedir != 0)
					{
						LastDir = Movedir;
					}
					LadderHeight = other.transform.position.y;
					Movedir = 0;

				}
				else if(Random.Range(0,4) == 1)
				{
					OnLadder = true;
					transform.position = new Vector3(other.transform.position.x,transform.position.y,0);
					if(Movedir != 0)
					{
						LastDir = Movedir;
					}
					LadderHeight = other.transform.position.y;
					Movedir = 0;

				}
				}
			}
		}
		else //crosser tosser
		{

			if(other.tag == "Ground" && JumpVel.y < -0.06f)
			{
				if(transform.position.y > other.transform.position.y + other.transform.lossyScale.y*0.4f)
				{
					JumpVel = new Vector3(-JumpVel.x,0,0);	

				}
			}
			if(other.tag == "GroundZero" && JumpVel.y < 0)
			{
				GZeroed = true;
				if(transform.position.y > other.transform.position.y + other.transform.lossyScale.y*0.4f)
				{
					PigentyFigmentyCrosserTosser = false;	
					FrontFacing = false;
					Movedir = -1;
					OnGround = true;
				}
			}
		}

		if(other.tag == "Hammer" && !DieOnce)
		{
			DieOnce = true;
			Instantiate(Kaboom,transform.position,transform.rotation);
			GameObject SpawnedBubble = Instantiate(Points,transform.position + new Vector3(0,0.5f,0), transform.rotation);
			Instantiate(ScoreSound,transform.position + new Vector3(0,0.5f,0), transform.rotation);

			SpawnedBubble.transform.position *= 16;
			SpawnedBubble.transform.position = new Vector3(Mathf.Round(SpawnedBubble.transform.position.x),Mathf.Round(SpawnedBubble.transform.position.y),Mathf.Round(SpawnedBubble.transform.position.z));
			SpawnedBubble.transform.position /= 16;

			if(BlueBarrel)
			{
				
				Main.FireballsPresent--;
				int i = Random.Range(0,3);
				switch(i)
				{
				default: Score = 300; break;
				case 1: Score = 500; break;
				case 2 : Score= 800; break;
				}
			}
			else
			{
				Score = 100;
			}

			switch(Score)
			{
			case 100: SpawnedBubble.GetComponent<SpriteRenderer>().sprite = score100; break;
			case 300: SpawnedBubble.GetComponent<SpriteRenderer>().sprite = score300; break;
			case 500: SpawnedBubble.GetComponent<SpriteRenderer>().sprite = score500; break;
			case 800: SpawnedBubble.GetComponent<SpriteRenderer>().sprite = score800; break;
			}
			Main.Score += Score;
			Main.NewestScore = Score;
			Destroy(gameObject);

		}

		if(other.tag == "Oil" && BlueBarrel)
		{
			Instantiate(Firebun,transform.position + new Vector3(0.25f,1,0),transform.rotation,Main.HazardMap);
			Destroy(gameObject);
		}

	}

}
