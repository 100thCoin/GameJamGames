using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaryController : MonoBehaviour {

	public float Speed;
	public float ArmStrength;
	public SheepDetector SheepDetect;

	public int HP;

	public bool Callsheep;

	public GameObject CurrentLevel;

	public int Gold;
	public int LambMeat;

	public bool SmallLevel;

	public bool Forward;
	public bool Right;
	public bool Running;

	public RuntimeAnimatorController IdleFront;
	public RuntimeAnimatorController RunFront;

	public RuntimeAnimatorController IdleBack;
	public RuntimeAnimatorController RunBack;


	public int DesiredSheepType;

	public GameObject Arrow;

	public int[] SheepType;
	public int[] SheepCount;


	public bool HasKey;

	public int desiredsheepordernum;

	public GameObject NextLevel;
	public GameObject Cam;

	public bool LoadNext;

	public float LoadTimer;

	public GameObject Whoops;
	public GameObject WhoopPrefab;

	public GameObject Holdit;

	public  bool Win;
	public  bool Lose;
	public GameObject Winscreen;
	public GameObject UiHolder;
	public GameObject LoseScreen;

	public GameObject Game;

	public int LastHP;

	public GameObject Ouch;
	public GameObject Heal;

	public GameObject HPText;

	public float BlinkTimer;

	public void Hurt()
	{
		HP--;

		//shake screen


	}


	// Use this for initialization
	void LateUpdate() {

		if(Callsheep)
		{
			Callsheep = false;



		}

		if(LastHP > HP)
		{
			Cam.GetComponent<LimitCamera>().ShakeTimer += 0.5f;
			LastHP = HP;
			Instantiate(Ouch,gameObject.transform.position,gameObject.transform.rotation);
			BlinkTimer = 2;

		}
		if(LastHP < HP)
		{
			//Cam.GetComponent<LimitCamera>().ShakeTimer += 0.5f;
			LastHP = HP;
			Instantiate(Heal,gameObject.transform.position,gameObject.transform.rotation);
			//BlinkTimer = 2;

		}

		if(BlinkTimer <= 0)
		{
			HPText.GetComponent<TextMesh>().color = new Vector4(1,1,1,1);
		}

		if(BlinkTimer >= 0)
		{
			float b = Mathf.Sin(BlinkTimer * 3);
			HPText.GetComponent<TextMesh>().color = new Vector4(b,b,b,1);
			BlinkTimer -= Time.deltaTime;
		}




	}
	
	// Update is called once per frame

	void Update()
	{
		if(HP <= 0)
		{
			Lose = true;


		}


		if(Lose)
		{
			UiHolder.SetActive(false);
			LoseScreen.SetActive(true);
			Instantiate(Ouch,gameObject.transform.position,gameObject.transform.rotation);

			Cam.transform.parent = null;
			Destroy(Game.gameObject);
		}

		
		if(Input.GetKeyDown(KeyCode.Alpha1)){desiredsheepordernum = 0;}
		if(Input.GetKeyDown(KeyCode.Alpha2) && SheepType.Length >= 2){desiredsheepordernum = 1;}
		if(Input.GetKeyDown(KeyCode.Alpha3) && SheepType.Length >= 3){desiredsheepordernum = 2;}
		if(Input.GetKeyDown(KeyCode.Alpha4) && SheepType.Length >= 4){desiredsheepordernum = 3;}
		if(Input.GetKeyDown(KeyCode.Alpha5) && SheepType.Length >= 5){desiredsheepordernum = 4;}
		if(Input.GetKeyDown(KeyCode.Alpha6) && SheepType.Length >= 6){desiredsheepordernum = 5;}
		if(Input.GetKeyDown(KeyCode.Alpha7) && SheepType.Length >= 7){desiredsheepordernum = 6;}
		if(Input.GetKeyDown(KeyCode.Alpha8) && SheepType.Length >= 8){desiredsheepordernum = 7;}
		if(Input.GetKeyDown(KeyCode.Alpha9) && SheepType.Length >= 9){desiredsheepordernum = 8;}
		if(Input.GetKeyDown(KeyCode.Alpha0) && SheepType.Length >= 10){desiredsheepordernum = 9;}

		if (Input.GetAxis("Mouse ScrollWheel") < 0f )
		{
			desiredsheepordernum++;
			if(desiredsheepordernum > SheepType.Length -1)
			{
				desiredsheepordernum = 0;
			}

		}
		else if (Input.GetAxis("Mouse ScrollWheel") > 0f )
		{
			desiredsheepordernum--;
			if(desiredsheepordernum < 0)
			{
				desiredsheepordernum = SheepType.Length-1;
			}
		}
		DesiredSheepType = SheepType[desiredsheepordernum];



		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			if(SheepDetect.DetectingDesiredSheep)
			{
				SheepDetect.OohPickMe.GetComponent<AI_Sheep>().StatusFollow = false;
				SheepDetect.OohPickMe.GetComponent<AI_Sheep>().StatusRun = false;
				SheepDetect.OohPickMe.GetComponent<AI_Sheep>().StatusAttack = false;
				SheepDetect.OohPickMe.GetComponent<AI_Sheep>().StatusHeld = true;

			}

		}


	}


	void FixedUpdate () {


		if(LoadNext)
		{
			LoadTimer += Time.fixedDeltaTime;


			if(LoadTimer >= 0.5f)
			{

				if(Win)
				{

					Winscreen.SetActive(true);
					UiHolder.SetActive(false);
					Whoops.transform.Find("Whoop").GetComponent<Whoop>().Grow = true;

				}
				else
				{
				gameObject.transform.position = new Vector2(0,0);
				Callsheep = true;
				Destroy(CurrentLevel);
					Instantiate(NextLevel,Vector3.zero,gameObject.transform.rotation,GameObject.Find("Game").transform);
				LoadNext = false;
				Whoops.transform.Find("Whoop").GetComponent<Whoop>().Grow = true;
				}
			}



		}




		Vector2 MousePos = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>().ScreenToWorldPoint (Input.mousePosition);

		Arrow.gameObject.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(MousePos.y - gameObject.transform.position.y + 2,MousePos.x - gameObject.transform.position.x) * Mathf.Rad2Deg);




	
		Vector2 Movement = new Vector2(0,0);

		if(Input.GetKey(KeyCode.D))
		{
			Movement = new Vector2(1,0);
		}
		if(Input.GetKey(KeyCode.A))
		{
			Movement = new Vector2(Movement.x - 1,0);
		}

		if(Input.GetKey(KeyCode.W))
		{
			Movement = new Vector2(Movement.x,1);
		}
		if(Input.GetKey(KeyCode.S))
		{
			Movement = new Vector2(Movement.x,Movement.y - 1);
		}

		Movement = Movement.normalized * Speed;

		gameObject.GetComponent<Rigidbody>().velocity = Movement;


		if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
		{
			Right = true;
		}
		else if(!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
		{
			Right = false;
		}
		if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) || ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && ((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)))))
		{
			Forward = true;
		}
		else if((!Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W)))
		{
			Forward = false;
		}

		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
		{
			Running = true;
		}
		else
		{
			Running = false;
		}

		if(Right)
		{
			gameObject.GetComponent<SpriteRenderer>().flipX = false;
		}
		else
		{
			gameObject.GetComponent<SpriteRenderer>().flipX = true;
		}

		if(Forward)
		{
			if(Running)
			{
				gameObject.GetComponent<Animator>().runtimeAnimatorController = RunFront;
			}
			else
			{
				gameObject.GetComponent<Animator>().runtimeAnimatorController = IdleFront;
			}
		}
		else
		{
			if(Running)
			{
				gameObject.GetComponent<Animator>().runtimeAnimatorController = RunBack;
			}
			else
			{
				gameObject.GetComponent<Animator>().runtimeAnimatorController = IdleBack;
			}
		}

		//  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH
		gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-1 + ((gameObject.transform.position.y - 2) * 0.0001f));


	}


	void OnTriggerEnter(Collider other)
	{

		if(other.name == "EndLevel")
		{

			if(other.gameObject.GetComponent<DontEndLevel>().WinGame)
			{
				Win = true;
			}
			else
			{
				NextLevel = other.gameObject.GetComponent<DontEndLevel>().NextLevel;
			}
			gameObject.transform.position = new Vector2(500,gameObject.transform.position.y);


			LoadTimer = 0;



			//Make a delay here.




			LoadNext = true;
			Whoops = Instantiate(WhoopPrefab,Cam.transform.position,gameObject.transform.rotation,Cam.transform) as GameObject;
		

		}



	}

	void OnTriggerStay(Collider other)
	{
	if(other.name == "NoEndLevel")
	{

		gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-10,0,0);
			gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.2f,gameObject.transform.position.y,0);
			Holdit.gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1,1,1,1);



	}
	}



}
