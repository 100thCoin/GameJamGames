using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour {

	public float Speed;
	public float SpeedLimit;

	public float Gravity;

	public float JumpHeight;

	public RuntimeAnimatorController Run;
	public RuntimeAnimatorController Idle;

	public Vector3 Velocity;

	public bool CantGoLeft;
	public bool CantGoRight;
	public bool OnGround;

	public GameObject Redicle;
	public GameObject RedicleDot;
	public Material Grid;

	public float CoyoteTime;

	public bool InAir;

	public GameObject Telemode;
	public GameObject TelemodeBody;

	public GameObject TeleTimer;
	public float TelemodeDelay;
	public Sprite[] TeleDelay;


	public GameObject TeleAllItemsDropper;

	public DataHolder Main;

	public bool AtMoneyBox;
	public GameObject MoneyBoxDialogue;

	public bool OnLadder;

	// Use this for initialization
	void Start () {
		
	}


	void Update()
	{

		if(AtMoneyBox && Main.Items.Length > 0)
		{
			

			MoneyBoxDialogue.SetActive(true);

			if(Input.GetKeyDown(KeyCode.W))
			{
				int i = 0;

				while (Main.Items.Length >= 1)
				{
					if(Main.Items[0].Count > 0)
					{
						Main.Items[0].Count--;
						Main.Money += Mathf.RoundToInt(Main.Items[0].Price);
						Main.TotalEarned += Mathf.RoundToInt(Main.Items[0].Price);

						if(Main.Items[0].Name == "Ladderite")
						{
							Main.RefinedLadderite++;

						}

					}
					else
					{
						Destroy(Main.Items[0].DHMOText.transform.parent.gameObject);

						Main.RemoveItem(Main.Items[0]);
					}
						
				}

				Main.GetComponent<HeartBeat>().MoneyCount.text = "Money: " + Main.Money;


			}


		}
		else
		{
			MoneyBoxDialogue.SetActive(false);


		}


		CoyoteTime -= Time.deltaTime;
		if(Input.GetKeyDown(KeyCode.Space) && (OnGround || CoyoteTime > 0))
		{
			CoyoteTime = -0.2f;
			Velocity = new Vector3(Velocity.x,Velocity.y + JumpHeight,0);
			OnGround = false;
			InAir = true;

		}

		Vector2 MousePos = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>().ScreenToWorldPoint (Input.mousePosition);

		Redicle.gameObject.transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 (gameObject.transform.position.y - MousePos.y, gameObject.transform.position.x - MousePos.x) * Mathf.Rad2Deg + 90);
		RedicleDot.gameObject.transform.eulerAngles = new Vector3 (0, 0, 0);

		Vector2 Tri = new Vector2(transform.position.x - MousePos.x,transform.position.y - MousePos.y);
		float Dist = Mathf.Sqrt(Mathf.Pow(Tri.x,2) + Mathf.Pow(Tri.y,2));

		if(Dist < 5)
		{

			Grid.SetFloat("_Opacity",0.375f - Dist*0.125f);

		}
		else
		{
			Grid.SetFloat("_Opacity",0);

		}

		if(Dist < 2.5f)
		{
			RedicleDot.transform.position = new Vector3(MousePos.x,MousePos.y,-0.1f);

		}
		else
		{
			RedicleDot.transform.localPosition = new Vector3(0,2.5f,-0.1f);
		}


	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "RWall")
		{
			CantGoRight = false;
		}
		if(other.tag == "LWall")
		{
			CantGoLeft = false;
		}
		if(other.tag == "Floor")
		{
			OnGround = false;
			if(!InAir)
			{
				CoyoteTime = 0.2f;
			}
		}
		if(other.tag == "MoneyBox")
		{

			AtMoneyBox = false;

		}
		if(other.tag == "Ladder")
		{

			OnLadder = false;
		}
		if(other.tag == "FreezeCam")
		{
			Main.FreezeCam = false;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "RWall")
		{
			CantGoRight = true;
		}
		if(other.tag == "LWall")
		{
			CantGoLeft = true;
		}
		if(other.tag == "Floor")
		{
			OnGround = true;
		}
		if(other.tag == "Ceiling")
		{
			if(Velocity.y > 0)
			{
				Velocity = new Vector3(Velocity.x,0,0);
			}

		}
		if(other.tag == "MoneyBox")
		{

			AtMoneyBox = true;

		}
		if(other.tag == "Ladder" && Input.GetKey(KeyCode.W))
		{

			OnLadder = true;
		}
		if(other.tag == "FreezeCam")
		{
			Main.FreezeCam = true;
		}
		if(other.tag == "WinGame")
		{
			Main.WinGame = true;
		}
	}


	void OnTriggerEnter(Collider other)
	{

		if(other.tag == "Floor")
		{
			InAir = false;
		}
		if(other.name == "DoorOpen" && !other.transform.parent.GetComponent<Door>().OpenBool)
		{
			other.transform.parent.GetComponent<Door>().OpenBool = true;
			other.transform.parent.GetComponent<Door>().OpenTimer = 0;

			Main.Outside = true;
		}
		if(other.name == "DoorClose" && other.transform.parent.GetComponent<Door>().OpenBool)
		{
			other.transform.parent.GetComponent<Animator>().runtimeAnimatorController = null;

			other.transform.parent.GetComponent<SpriteRenderer>().sprite = other.transform.parent.GetComponent<Door>().Closed;
			other.transform.parent.GetComponent<Door>().OpenBool = false;
			Main.Outside = false;
		}
	}






	// Update is called once per frame
	void FixedUpdate () {



		if(Input.GetKey(KeyCode.Q) && !Main.IsPlayerTele && Main.UnlockTele && !Main.WinGame)
		{
			TelemodeDelay -= Time.fixedDeltaTime;
			TeleTimer.GetComponent<SpriteRenderer>().sprite = TeleDelay[2];
			if(TelemodeDelay <= 2)
			{
				TeleTimer.GetComponent<SpriteRenderer>().sprite = TeleDelay[1];
			}
			if(TelemodeDelay <= 1)
			{
				TeleTimer.GetComponent<SpriteRenderer>().sprite = TeleDelay[0];
			}

			if(TelemodeDelay <=0)
			{

				Main.IsPlayerTele = true;
				TeleTimer.GetComponent<SpriteRenderer>().sprite = null;

				Instantiate(TeleAllItemsDropper,transform.position,transform.rotation,transform.parent);

				Telemode.SetActive(true);
				TelemodeBody.GetComponent<TeleOrbGraphics>().Pos = gameObject.transform.position;

				TelemodeBody.GetComponent<TeleOrbGraphics>().GetReady();
				TelemodeDelay = 3;

				Main.PostTUT = true;

				gameObject.SetActive(false);
			}



		}
		else
		{
			TelemodeDelay = 3;
			TeleTimer.GetComponent<SpriteRenderer>().sprite = null;

		}







		if((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))))
		{
			Velocity = new Vector3(Velocity.x * 0.7f,Velocity.y,0);
			GetComponent<Animator>().runtimeAnimatorController = Idle;
		}
		else if ((!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)))
		{

			GetComponent<Animator>().runtimeAnimatorController = Run;
			GetComponent<SpriteRenderer>().flipX = Input.GetKey(KeyCode.A);

		}
		if(!Main.WinGame)
		{
		if(Velocity.y > -0.3f)
		{
			Velocity = new Vector3(Velocity.x,Velocity.y - Gravity,0);
		}

		if(Input.GetKey(KeyCode.D))
		{
			if(Velocity.x < 0)
			{
				Velocity = new Vector3(Velocity.x + Speed,Velocity.y,0);
			}
			if(Velocity.x < SpeedLimit)
			{
				Velocity = new Vector3(Velocity.x + Speed,Velocity.y,0);
			}
			else
			{
				Velocity = new Vector3(SpeedLimit,Velocity.y,0);
			}
		}

		if(Input.GetKey(KeyCode.A))
		{
			if(Velocity.x > 0)
			{
				Velocity = new Vector3(Velocity.x - Speed,Velocity.y,0);
			}
			if(Velocity.x > -SpeedLimit)
			{
				Velocity = new Vector3(Velocity.x - Speed,Velocity.y,0);
			}
			else
			{
				Velocity = new Vector3(-SpeedLimit,Velocity.y,0);
			}
		}

		Vector3 Prev = new Vector3(Velocity.x,Velocity.y,0);

		if(CantGoLeft && Velocity.x < 0)
		{
			Velocity = new Vector3(0,Velocity.y,0);
			Prev = new Vector3(Prev.x * 0.5f,Prev.y,0);
		}
		else if(CantGoRight && Velocity.x > 0)
		{
			Velocity = new Vector3(0,Velocity.y,0);
			Prev = new Vector3(Prev.x * 0.5f,Prev.y,0);

		}

		if(OnGround && Velocity.y < 0)
		{
			Velocity = new Vector3(Velocity.x,0,0);
		}


		if(OnLadder)
		{
			if(Input.GetKey(KeyCode.W))
			{
				Velocity = new Vector3(0,0.3f,0);

			}
			else
			{
				Velocity = new Vector3(Velocity.x,0,0);

			}


		}

		
		transform.position += Velocity;

		Velocity = new Vector3(Prev.x,Velocity.y,0);
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}
}
