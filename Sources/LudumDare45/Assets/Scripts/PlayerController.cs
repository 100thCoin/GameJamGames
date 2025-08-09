using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float Speed;
	public float SpeedLimit;


	public float JumpHeight;

	public Rigidbody rb;

	public RuntimeAnimatorController Run;
	public RuntimeAnimatorController Idle;
	public RuntimeAnimatorController Jump;
	public RuntimeAnimatorController Shove;

	public float Shovetimer;

	public float CurrentSpeed;
	public Animator anim;
	public SpriteRenderer sr;

	public bool CantGoRight;
	public bool CantGoLeft;

	public bool jumpingup;
	public bool OnGround;

	public bool AutomovingLevelIntro;

	public bool TouchBoulder;
	public Boulder TouchedBoulder;
	public GameObject Gore;
	public bool TouchShoveOrc;
	public AI_FireOrc TouchedShoveOrc;
	public bool TouchTotem;
	public KeyTotem TouchedTotem;

	public bool TouchPickup;
	public Pickup TouchedPickup;

	public bool TouchSword;
	public SwordPull TouchedSword;


	public bool TouchChest;
	public OpenChest TouchedChest;

	public bool CutsceneNoMove;

	public DataHolder Main;


	public GameObject ThrowGuide;
	public GameObject ThrowOrigin;

	public float PickupTimeout;

	public GameObject SpawnSwordHere;
	public GameObject RotateSwordLikeThis;
	public GameObject Sword;
	public GameObject CurrentSword;

	// Use this for initialization
	void Start () {
		
	}



	void Update()
	{
		Shovetimer -= Time.deltaTime;
		PickupTimeout -= Time.deltaTime;


		if(gameObject.transform.position.y < -20)
		{
			transform.position = new Vector3(transform.position.x,0,transform.position.z);
			rb.velocity = Vector3.zero;

		}

		if(Main.UnlockedSword && !TouchChest)
		{
			if(CurrentSword == null)
			{
				if(Input.GetKeyDown(KeyCode.F))
				{
					CurrentSword = Instantiate(Sword,SpawnSwordHere.transform.position,RotateSwordLikeThis.transform.rotation,SpawnSwordHere.transform) as GameObject;
				}

			}

		}



		if(Main.HeldPickup != 0 || Main.UnlockedSword)
		{
			ThrowGuide.GetComponent<SpriteRenderer>().enabled = true;
		}
		else
		{
			ThrowGuide.GetComponent<SpriteRenderer>().enabled = false;
		}

		if(!AutomovingLevelIntro && !CutsceneNoMove)
		{
			if(Input.GetKeyDown(KeyCode.Space) && OnGround)
			{
				rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y + JumpHeight,rb.velocity.z);
				OnGround = false;
				anim.runtimeAnimatorController = Jump;
				jumpingup = true;
			}

			if(!OnGround && rb.velocity.y >0)
			{
				if(Input.GetKeyUp(KeyCode.Space) && jumpingup)
				{
					jumpingup = false;
					rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y * 0.5f,rb.velocity.z);
				}
			}

			if(TouchBoulder)
			{

				if(Input.GetKeyDown(KeyCode.F))
				{
					int D = 1;
					if(transform.position.x > TouchedBoulder.transform.position.x)
					{
						D = -1;
					}
					TouchedBoulder.Dir = D;
					TouchedBoulder.Timer = 0;
					TouchedBoulder.Active = true;
					TouchBoulder = false;
					TouchedBoulder.Speed = 0.5f;
					anim.runtimeAnimatorController = Shove;
					Shovetimer = 0.25f;
				}
			}
			else if(TouchShoveOrc)
			{
				if(Input.GetKeyDown(KeyCode.F))
				{
					TouchedShoveOrc.Active = true;
					TouchedShoveOrc.P = this;
					anim.runtimeAnimatorController = Shove;
					Shovetimer = 0.25f;
				}
			}
			else if(TouchTotem)
			{
				if(Input.GetKeyDown(KeyCode.F) && Main.HasKey)
				{
					TouchedTotem.Active = true;
					anim.runtimeAnimatorController = Shove;
					Shovetimer = 0.25f;
					Main.HasKey = false;
				}
			}
			else if(TouchSword)
			{
				if(Input.GetKeyDown(KeyCode.F))
				{
					TouchedSword.Active = true;
					TouchedSword.P = this;
					anim.runtimeAnimatorController = Shove;
					Shovetimer = 0.25f;
				}
			}
			else if(TouchChest)
			{
				if(Input.GetKeyDown(KeyCode.F) && !TouchedChest.Active)
				{
					TouchedChest.Active = true;
					TouchedChest.P = this;
					TouchedChest.Step[0] = true;
					TouchedChest.anim.runtimeAnimatorController = TouchedChest.Chest;
					anim.runtimeAnimatorController = Shove;
					Main.DragonBeat = true;
					Shovetimer = 0.25f;
				}
			}
		}
		if(PickupTimeout > 0)
		{
			Main.HeldPickup = 0;
		}


		if(Main.HeldPickup == 0)
		{
			if(Input.GetKeyDown(KeyCode.F) && TouchPickup && PickupTimeout <=0)
			{
				TouchedPickup.Active = true;
			}
		}
		else
		{
			if(Input.GetKeyDown(KeyCode.F))
			{
				GameObject Proj = Instantiate(Main.Pickups[Main.HeldPickup],ThrowOrigin.transform.position,transform.rotation,Main.CurrentLoadedLevelObject.transform);

				Vector3 Vel = new Vector3(ThrowOrigin.transform.position.x - transform.position.x,ThrowOrigin.transform.position.y - transform.position.y,0).normalized * 30;

				Proj.GetComponent<Rigidbody>().isKinematic = false;
				Proj.GetComponent<Rigidbody>().useGravity = true;

				Proj.GetComponent<Pickup>().ProjVel = Vel;

				Main.HeldPickup = 0;
				PickupTimeout = 0.25f;
				anim.runtimeAnimatorController = Shove;
				Shovetimer = 0.25f;
			}
		}

		Vector2 MousePos = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>().ScreenToWorldPoint (Input.mousePosition);

		ThrowGuide.transform.position = gameObject.transform.position;

		ThrowGuide.transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 (gameObject.transform.position.y - MousePos.y, gameObject.transform.position.x - MousePos.x) * Mathf.Rad2Deg + 180);



	}

	// Update is called once per frame
	void FixedUpdate () {



		if(!AutomovingLevelIntro)
		{

			if(Input.GetKey(KeyCode.D))
			{
				CurrentSpeed += Speed * Time.fixedDeltaTime;
				sr.flipX = false;

			}
			if(Input.GetKey(KeyCode.A))
			{
				CurrentSpeed -= Speed * Time.fixedDeltaTime;
				sr.flipX = true;
			}

			if(Input.GetKey(KeyCode.D) && CurrentSpeed < 0)
			{
				CurrentSpeed += Speed * Time.fixedDeltaTime;
			}
			if(Input.GetKey(KeyCode.A) && CurrentSpeed > 0)
			{
				CurrentSpeed -= Speed * Time.fixedDeltaTime;
			}

			if(!CutsceneNoMove)
			{
				if((!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)))
				{
					if(OnGround && Shovetimer<=0)
					{
						anim.runtimeAnimatorController = Idle;
					}
					CurrentSpeed *= 0.7f;

				}
				else
				{
					if(OnGround && !CutsceneNoMove && Shovetimer<=0)
					{
						anim.runtimeAnimatorController = Run;
					}
				}
			}
		}
		else
		{
			if(transform.position.x < -1)
			{
				CurrentSpeed = SpeedLimit;
				anim.runtimeAnimatorController = Run;

			}
			else
			{
				CurrentSpeed *= 0.7f;
				anim.runtimeAnimatorController = Idle;

			}
		}

		if(CurrentSpeed > SpeedLimit)
		{
			CurrentSpeed = SpeedLimit;
		}
		else if(CurrentSpeed < - SpeedLimit)
		{
			CurrentSpeed = -SpeedLimit;
		}
		if(!CutsceneNoMove)
		{
			if((!CantGoRight && CurrentSpeed > 0) || (!CantGoLeft && CurrentSpeed < 0))
			{
				transform.position += new Vector3(CurrentSpeed * Time.fixedDeltaTime,0,0);
			}
		}
		rb.velocity = new Vector3(rb.velocity.x * 0.9f,rb.velocity.y,0);

	}


	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Ground")
		{
			OnGround = true;
		}


		if(other.tag == "Enemy" || other.tag == "Killzone")
		{
			GameObject.Find("Main").GetComponent<DataHolder>().Dead = true;
			Instantiate(Gore,transform.position,Gore.transform.rotation,transform.parent);
			transform.position = new Vector3(0,0,-400);
			Main.SwooshControl.Step[0] = true;
			Main.SwooshControl.DeadWhoop = true;
			Main.SwooshControl.LevelEnter = false;

			Main.SwooshControl.Timer = 0;
		}


	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "CantGoRight")
		{
			CantGoRight = true;
		}
		if(other.tag == "CantGoLeft")
		{
			CantGoLeft = true;
		}
		if(other.tag == "Boulder")
		{
			OnGround = true;
			TouchBoulder = true;
			TouchedBoulder = other.GetComponent<Boulder>();
		}
		if(other.tag == "ShoveOrc")
		{
			TouchShoveOrc = true;
			TouchedShoveOrc = other.transform.parent.parent.GetComponent<AI_FireOrc>();
		}
		if(other.tag == "NeedKey")
		{
			TouchTotem = true;
			TouchedTotem = other.transform.GetComponent<KeyTotem>();
		}
		if(other.tag == "Pickup")
		{
			TouchPickup = true;
			TouchedPickup = other.transform.GetComponent<Pickup>();
		}
		if(other.tag == "Sword")
		{
			TouchSword = true;
			TouchedSword = other.transform.GetComponent<SwordPull>();
		}
		if(other.tag == "Chest")
		{
			TouchChest = true;
			TouchedChest = other.transform.GetComponent<OpenChest>();
		}

	
	}


	void OnTriggerExit(Collider other)
	{
		if(other.tag == "CantGoRight")
		{
			CantGoRight = false;
		}
		if(other.tag == "CantGoLeft")
		{
			CantGoLeft = false;
		}

		if(other.tag == "Boulder")
		{
			TouchBoulder = false;
		}
		if(other.tag == "ShoveOrc")
		{
			TouchShoveOrc = false;
		}
		if(other.tag == "NeedKey")
		{
			TouchTotem = false;
		}
		if(other.tag == "Pickup")
		{
			TouchPickup = false;
		}
		if(other.tag == "Sword")
		{
			TouchSword = false;
		}
		if(other.tag == "Chest")
		{
			TouchChest = false;
		}
	}



}
