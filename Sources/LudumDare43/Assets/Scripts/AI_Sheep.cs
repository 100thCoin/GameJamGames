using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Sheep : MonoBehaviour {

	// THIS SCRIPT GOES ON THE SHEEP PARENT OBJECT
	public bool KillOnce;

	public int SheepType;
	public float DeathTimer;

	public bool StatusFollow;
	public bool StatusHeld;
	public bool StatusAttack;
	public bool StatusRun;
	public bool StatusDead;

	public GameObject LambDrop;

	public float Timer;

	public bool OnGround;

	public GameObject Mary;
	public GameObject SheepVisual;
	public GameObject Shadow;

	public Vector3 ThreeAxisVel;

	public float Height;
	public float Speed;
	public float JumpHeight;
	public float Gravity;

	public float RandomDelay;

	public bool FairyRepeat;
	public GameObject FairyTarget;

	public Sprite DED;

	// Use this for initialization
	void Start () {
		gameObject.name = "SheepType_" + SheepType;	

		bool yougood = false;

		int i = 0;

		if(Mary.GetComponent<MaryController>().SheepType.Length >0 && Mary.GetComponent<MaryController>().SheepCount.Length >0)
		{
			//Check if new sheep type

			while (i < Mary.GetComponent<MaryController>().SheepType.Length)
			{
				if(SheepType == Mary.GetComponent<MaryController>().SheepType[i])
				{
					yougood = true;
					break;
				}
			
				i++;
			}

			if(!yougood)
			{
				int[] NewList = new int[Mary.GetComponent<MaryController>().SheepType.Length + 1];
				int[] NewCount = new int[Mary.GetComponent<MaryController>().SheepCount.Length + 1];

				i = 0;
				while (i < Mary.GetComponent<MaryController>().SheepType.Length)
				{
					NewList[i] = Mary.GetComponent<MaryController>().SheepType[i];
					NewCount[i] = Mary.GetComponent<MaryController>().SheepCount[i];

					i++;
				}
				NewList[i] = SheepType;
				NewCount[i] = 1;

				Mary.GetComponent<MaryController>().SheepType = NewList;
				Mary.GetComponent<MaryController>().SheepCount = NewCount;

			}
			else
			{
				Mary.GetComponent<MaryController>().SheepCount[i] += 1;


			}
		}
		else
		{
				Mary.GetComponent<MaryController>().SheepType = new int[1];
				Mary.GetComponent<MaryController>().SheepCount = new int[1];

				Mary.GetComponent<MaryController>().SheepType[0] = SheepType;
				Mary.GetComponent<MaryController>().SheepCount[0] = 1;


		}



	}

	void Update()
	{

		if(Mary.GetComponent<MaryController>().Callsheep)
		{

			gameObject.transform.position = new Vector2(Random.Range(-6,-4),Random.Range(-3,3));

		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {


		if(StatusDead)
		{

			gameObject.transform.position = new Vector3(gameObject.transform.position.x + ThreeAxisVel.x, gameObject.transform.position.y + ThreeAxisVel.y,0);

				ThreeAxisVel = new Vector3(ThreeAxisVel.x,ThreeAxisVel.y,ThreeAxisVel.z - Gravity);

				Height = Height + ThreeAxisVel.z;

				SheepVisual.gameObject.transform.localPosition = new Vector3(0,Height,0);

				if(Height <= 0)
				{
					OnGround = true;
					SheepVisual.gameObject.transform.localPosition = new Vector3(0,0,0);

				}

			ThreeAxisVel = new Vector3(ThreeAxisVel.x * 0.94f,ThreeAxisVel.y * 0.94f,ThreeAxisVel.z);


		}





		if(DeathTimer >0)
		{
			DeathTimer += Time.fixedDeltaTime;

			if(DeathTimer > 4)
			{
				Destroy(gameObject);
			}

		}


		Timer += Time.fixedDeltaTime;

		if(StatusFollow)
		{
			
				



		SheepVisual.gameObject.transform.eulerAngles = new Vector3(0,0,0);

		if(OnGround && Timer >= RandomDelay)
		{
				gameObject.transform.Find("ThrowBox").gameObject.SetActive(false);

			Jump();

			Timer = 0;
				if(!gameObject.GetComponent<SphereCollider>().enabled)
				{
					gameObject.GetComponent<SphereCollider>().enabled = true;

				}

		}

		if(!OnGround)
		{
				gameObject.transform.position = new Vector3(gameObject.transform.position.x + ThreeAxisVel.x, gameObject.transform.position.y + ThreeAxisVel.y,0);
				
				if(SheepType != 5 && SheepType != 6)
				{
					ThreeAxisVel = new Vector3(ThreeAxisVel.x,ThreeAxisVel.y,ThreeAxisVel.z - Gravity);

					Height = Height + ThreeAxisVel.z;

					SheepVisual.gameObject.transform.localPosition = new Vector3(0,Height,0);

					if(Height <= 0)
					{
						OnGround = true;
						SheepVisual.gameObject.transform.localPosition = new Vector3(0,0,0);

					}
				}
				else
				{
					//Height = 2.5f;
					SheepVisual.gameObject.transform.localPosition = new Vector3(0,Height,0);

					if( Timer >= RandomDelay )
					{
						if(FairyRepeat)
						{
							if(FairyTarget != null)
							{
							FlyAttack();
							}
							else
							{
								FlyJump();
								FairyRepeat = false;
							}
						}
						else
						{

						FlyJump();
						}
					}

				}




		}

		//  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH
		gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-1 + ((gameObject.transform.position.y - 1) * 0.0001f));
		}


		else if(StatusHeld)
		{
			Mary.GetComponent<MaryController>().Arrow.SetActive(true);

			Shadow.gameObject.SetActive(false);

			SheepVisual.gameObject.transform.position = new Vector3(Mary.transform.position.x,Mary.transform.position.y + 0.5f,Mary.transform.position.z);
			SheepVisual.gameObject.transform.eulerAngles = new Vector3(0,0,180);

			if(!Input.GetKey(KeyCode.Mouse0))
			{
				Throw();

			}

		}




	}

	void Jump()
	{
		//get direction

		Vector2 Dir = new Vector2(Mary.transform.position.x - gameObject.transform.position.x,Mary.transform.position.y - gameObject.transform.position.y - 1).normalized;

		Vector2 Dist = new Vector2(Mary.transform.position.x - gameObject.transform.position.x,Mary.transform.position.y - gameObject.transform.position.y - 1);

		if(SheepType != 3)
		{
			RandomDelay = 0.05f;

		
		if(Mathf.Abs(Dist.x) < 4 && Mathf.Abs(Dist.y) < 4)
		{
			return;
		}
		}
		else
		{
			if(Mathf.Abs(Dist.x) < 2 && Mathf.Abs(Dist.y) < 2)
			{
				return;
			}
		}



		if(Dir.x < 0)
		{
			SheepVisual.GetComponent<SpriteRenderer>().flipX = true;
		}
		else
		{
			SheepVisual.GetComponent<SpriteRenderer>().flipX = false;

		}

		OnGround = false;

		ThreeAxisVel = new Vector3(Dir.x * Speed,Dir.y * Speed,JumpHeight);

		if(SheepType == 1)
		{
			RandomDelay = Random.Range(0.5f,2f);

		}
		else if(SheepType == 3)
		{
			RandomDelay = 0.05f;

		}
		else if(SheepType == 7)
		{
			RandomDelay = 0.05f;

		}
		else
		{
			RandomDelay = Random.Range(0.1f,1f);

		}


	}

	public void Throw()
	{
		Vector2 MousePos = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>().ScreenToWorldPoint (Input.mousePosition);

		Vector2 Dir = new Vector2(MousePos.x - SheepVisual.gameObject.transform.position.x,MousePos.y - SheepVisual.gameObject.transform.position.y + 2.5f).normalized;
		gameObject.transform.Find("ThrowBox").gameObject.SetActive(true);

		//Vector2 Dist = new Vector2(Mary.transform.position.x - gameObject.transform.position.x,Mary.transform.position.y - gameObject.transform.position.y - 1);

		float Arm = Mary.GetComponent<MaryController>().ArmStrength;

		ThreeAxisVel = new Vector3(Dir.x * Arm,Dir.y * Arm, 0.2f);

		if(SheepType == 7)
		{
			ThreeAxisVel = new Vector3(ThreeAxisVel.x * 2, ThreeAxisVel.y * 2, 0.2f);

		}

		StatusFollow = true;
		StatusHeld = false;
		OnGround = false;
		Height = 2.5f;
		gameObject.transform.position = new Vector3(SheepVisual.transform.position.x,SheepVisual.transform.position.y - 1.5f);
		gameObject.transform.position = new Vector3(gameObject.transform.position.x + ThreeAxisVel.x, gameObject.transform.position.y + ThreeAxisVel.y,0);

		SheepVisual.gameObject.transform.localPosition = new Vector3(0,Height,0);

		Shadow.gameObject.SetActive(true);
		Timer = 0;

		if(SheepType == 5 || SheepType == 6)
		{

			Timer = -0.5f;


		}

		//  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH
		gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-1 + ((gameObject.transform.position.y - 1) * 0.0001f));

		gameObject.GetComponent<SphereCollider>().enabled = false;
		Mary.GetComponent<MaryController>().SheepDetect.DetectingDesiredSheep = false;
		StartCoroutine(EnableSheep());

		Mary.GetComponent<MaryController>().Arrow.SetActive(false);

	}

	IEnumerator EnableSheep()
	{
		yield return new WaitForSeconds(1);
		gameObject.GetComponent<SphereCollider>().enabled = true;


	}


	void FlyJump()
	{
		//get direction

		Vector2 Dir = new Vector2(Mary.transform.position.x - gameObject.transform.position.x + Random.Range(-1,1),Mary.transform.position.y - gameObject.transform.position.y - 1 + Random.Range(-1,1)).normalized;

		Vector2 Dist = new Vector2(Mary.transform.position.x - gameObject.transform.position.x,Mary.transform.position.y - gameObject.transform.position.y - 1);


		if(Mathf.Abs(Dist.x) < 4 && Mathf.Abs(Dist.y) < 4)
		{
			ThreeAxisVel = ThreeAxisVel * 0.96f;
			if(SheepType == 9)
			{
				Gravity = 0.2f;
			}

			return;
		}

		Timer = 0;



		if(Dir.x < 0)
		{
			SheepVisual.GetComponent<SpriteRenderer>().flipX = true;
		}
		else
		{
			SheepVisual.GetComponent<SpriteRenderer>().flipX = false;

		}

		OnGround = false;

		ThreeAxisVel = new Vector3(Dir.x * Speed,Dir.y * Speed,0);

		RandomDelay = Random.Range(0.1f,1f);



	}

	void FlyAttack()
	{
		//get direction

		Vector2 Dir = new Vector2(FairyTarget.transform.position.x - gameObject.transform.position.x + Random.Range(-1,1),FairyTarget.transform.position.y - gameObject.transform.position.y + FairyTarget.transform.Find("Visual").gameObject.GetComponent<ZDepthMisc>().YOffset * 0.5f + Random.Range(-1,1)).normalized;



		Timer = 0;



		if(Dir.x < 0)
		{
			SheepVisual.GetComponent<SpriteRenderer>().flipX = true;
		}
		else
		{
			SheepVisual.GetComponent<SpriteRenderer>().flipX = false;

		}

		OnGround = false;

		ThreeAxisVel = new Vector3(Dir.x * Speed,Dir.y * Speed,0);

		RandomDelay = 0.5f;

		gameObject.transform.Find("ThrowBox").gameObject.SetActive(true);


	}


	public void KillSheep()
	{

		if(SheepType != 8)
		{

			SheepVisual.GetComponent<SpriteRenderer>().sprite = DED;
		}
		else
		{
			SheepVisual.GetComponent<SpriteRenderer>().sprite = null;
			Shadow.SetActive(false);

		}

		//decrement from player array

		//which array slot is this type?

		int i = 0;

		while (i < 10)
		{
			if(Mary.GetComponent<MaryController>().SheepType[i] == SheepType)
			{
				break;
			}
			i++;
		}
		//found it.
		//decrement it;
		Mary.GetComponent<MaryController>().SheepCount[i] -= 1;

		//TODO if zero, remove slot?


		DeathTimer = 2;

		Instantiate(LambDrop,gameObject.transform.position,gameObject.transform.rotation);

		Vector2 Dir = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized;

		ThreeAxisVel = new Vector3(Dir.x * Speed,Dir.y * Speed,JumpHeight);
		Gravity = 0.02f;

		StatusAttack = false;
		StatusHeld = false;
		StatusFollow = false;
		StatusRun = false;
		StatusDead = true;



	}


	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "SheepHurtbox" && !StatusHeld)
		{
			if(!KillOnce)
			{
				KillOnce = true;

			KillSheep();
			}
		}

	}


}
