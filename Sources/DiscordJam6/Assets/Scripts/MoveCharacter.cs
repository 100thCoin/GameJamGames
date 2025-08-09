using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour {

	public SpriteRenderer SR;
	public Animator Anim;

	public RuntimeAnimatorController Idle;
	public RuntimeAnimatorController Run;

	public Rigidbody RB;

	public float Speed;

	public float VelX;
	public float VelY;

	public bool WaitForShotOnDragon;

	public string FinalTownName;

	public bool CanShoot;
	public float Shootdelay;

	public int Health;

	public GameObject CurrentShot;
	public GameObject BulletPrefab;

	public Camera Cam;

	public float LoseByNameRuneoutTimer;
	public bool StartRunoutTimer;
	public bool LoseByNameRuneout;

	public GameObject HurtSFX;
	public GameObject GunSFX;

	public TextMesh[] UIBattleName;

	public RuntimeAnimatorController GunFire;
	public Animator Gun;

	public SpriteRenderer[] HPHearts;

	public GameObject GunClick;

	public Dataholder Main;

	public GameObject PlayerGibs;
	public float DeadTimer;
	public bool gibOnce;
	// Use this for initialization
	void Start () {
		
	}


	void Update()
	{



		if(Main.Paused)
		{
			return;
		}

		HPHearts[0].enabled = Health >= 1;
		HPHearts[1].enabled = Health >= 2;
		HPHearts[2].enabled = Health >= 3;
		if(Health <= 0)
		{
			if(!gibOnce)
			{
				Instantiate(PlayerGibs,transform.position,transform.rotation,transform.parent);
				gibOnce = true;
				SR.enabled = false;
				WaitForShotOnDragon = true;
			}
			DeadTimer+=Time.deltaTime;
			if(DeadTimer > 2)
			{
				Main.Paused = true;
				Main.MenuOnScreen = true;
				Main.DeadScreen.SetActive(true);
				Main.Dead = true;
				DeadTimer = 0;
				Health = 3;
				gibOnce = false;
			}
			return;

		}



		if(CanShoot)
		{




			if(Shootdelay > 0)
			{
				Shootdelay -= Time.deltaTime;
			}
			if(Input.GetKey(KeyCode.Mouse0))
			{
				if(Shootdelay <= 0)
				{
					Shootdelay += 0.2f;
					Vector3 MousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
					MousePos = new Vector3(MousePos.x,MousePos.y,0);

					Vector3 Direction = -(transform.position-MousePos).normalized;

					CurrentShot = Instantiate(BulletPrefab,transform.position + Direction,transform.rotation,transform.parent);
					Instantiate(GunSFX,transform.position + Direction,transform.rotation,transform.parent);

					BulletManager BM = CurrentShot.GetComponent<BulletManager>();
					BM.RB.velocity = Direction * 24 + RB.velocity*0.3f;
					BM.TM.text = "" + FinalTownName.ToCharArray()[0];
					FinalTownName = FinalTownName.Substring(1,FinalTownName.Length-1);

					Gun.runtimeAnimatorController = null;
					Gun.runtimeAnimatorController = GunFire;

					UIBattleName[0].text = FinalTownName;
					UIBattleName[1].text = FinalTownName;
					UIBattleName[2].text = FinalTownName;
					UIBattleName[3].text = FinalTownName;
					UIBattleName[4].text = FinalTownName;
					UIBattleName[5].text = FinalTownName;
					UIBattleName[6].text = FinalTownName;



					if(FinalTownName.Length == 0)
					{

						CanShoot = false;
						StartRunoutTimer = true;
						LoseByNameRuneoutTimer = 4;
					}

				}
			}
			if(Input.GetKeyUp(KeyCode.Mouse0))
			{
				//Shootdelay = 0;
			}
		}
		else
		{
			if(WaitForShotOnDragon && FinalTownName.Length ==0)
			{
				Health = 0;

			}
			if(FinalTownName.Length == 0)
			{
				if(Input.GetKeyDown(KeyCode.Mouse0))
				{
					CurrentShot = Instantiate(GunClick,transform.position,transform.rotation,transform.parent);
				}
			}

			if(StartRunoutTimer)
			{
				LoseByNameRuneoutTimer-= Time.deltaTime;
				if(LoseByNameRuneoutTimer < 0)
				{
					LoseByNameRuneout = true;

				}

			}



		}


	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		
		RB.velocity = Vector3.zero;
		if(Health <= 0)
		{
			return;
		}
		if(Main.Paused)
		{
			return;
		}
		if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) &&!WaitForShotOnDragon)
		{
			if(VelX >0)
			{
				VelX -=0.2f;
			}
			else if(VelX > -1)
			{
				VelX -=0.1f;
			}

			Anim.runtimeAnimatorController = Run;
			SR.flipX = false;
		}
		else if(!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) &&!WaitForShotOnDragon)
		{
			if(VelX <0)
			{
				VelX +=0.2f;
			}
			else if(VelX < 1)
			{
				VelX +=0.1f;
			}
			Anim.runtimeAnimatorController = Run;
			SR.flipX = true;
		}
		else
		{
			if(VelX <0)
			{
				VelX +=0.4f;
			}
			else if(VelX > 0)
			{
				VelX -=0.4f;
			}
			if(Mathf.Abs(VelX) < 0.5)
			{
				VelX = 0;
			}
		}

		if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) &&!WaitForShotOnDragon)
		{
			if(VelY >0)
			{
				VelY -=0.2f;
			}
			else if(VelY > -1)
			{
				VelY -=0.1f;
			}

			Anim.runtimeAnimatorController = Run;
		}
		else if(!Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W) &&!WaitForShotOnDragon)
		{
			if(VelY <0)
			{
				VelY +=0.2f;
			}
			else if(VelY < 1)
			{
				VelY +=0.1f;
			}
			Anim.runtimeAnimatorController = Run;
		}
		else
		{
			if(VelY <0)
			{
				VelY +=0.4f;
			}
			else if(VelY > 0)
			{
				VelY -=0.4f;
			}
			if(Mathf.Abs(VelY) < 0.5)
			{
				VelY = 0;
			}
		}

		RB.velocity += new Vector3(VelX,0);

		RB.velocity += new Vector3(0,VelY);

		RB.velocity = RB.velocity.normalized * Speed;
		if((!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)
			|| Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)
			|| !Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)
		) || WaitForShotOnDragon)//not moving
		{
			Anim.runtimeAnimatorController = Idle;
		}

	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Town")
		{
			other.gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1,1,1,0.5f);
		}

	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Town")
		{
			other.gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1,1,1,1);

		}
	}

	public void UpdateGunName()
	{
		UIBattleName[0].text = FinalTownName;
		UIBattleName[1].text = FinalTownName;
		UIBattleName[2].text = FinalTownName;
		UIBattleName[3].text = FinalTownName;
		UIBattleName[4].text = FinalTownName;
		UIBattleName[5].text = FinalTownName;
		UIBattleName[6].text = FinalTownName;
	}


}
