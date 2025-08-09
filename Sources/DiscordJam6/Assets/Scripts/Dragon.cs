using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour {

	public int Health;
	public Dataholder Main;
	public SpriteRenderer White;
	public float Whitetimer;
	public GameObject BulletHit;

	public TextMesh[] HealthBar;

	public bool Dead;

	public bool Chase;

	public GameObject DeathExplosion;
	public GameObject DragonMusic;

	public Rigidbody RB;

	public int FirePattern;

	public bool Fireing;

	public float Firecountdown;

	public GameObject[] FireAttack;
	public bool AltAttack;

	public float ActionTimer;

	public GameObject RotationHelper;

	public GameObject Minidragon;
	public bool AltSpawn;

	// Use this for initialization
	void Start () {
		Main = GameObject.Find("Main").GetComponent<Dataholder>();
		Main.Cam.Dragon = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(Main.Paused)
		{
			RB.velocity = Vector3.zero;

			return;
		}
		if(!Main.Char.WaitForShotOnDragon)
		{
			ActionTimer -= Time.deltaTime;

			if(ActionTimer < 0)
			{
				ActionTimer = 3;
				int RNG = Random.Range(0,4);
				Chase = RNG == 3;
				RNG = Random.Range(0,5);
				FirePattern = RNG;

				if(!Chase)
				{
					RB.velocity = Vector3.zero;
				}


				AltSpawn = !AltSpawn;
				if(AltSpawn)
				{

					GameObject Mini = Instantiate(Minidragon,transform.position,transform.rotation,transform.parent);
					RNG = Random.Range(0,2);
					int X = RNG == 1 ? Random.Range(0,2) == 1 ? 60 : -60 : Random.Range(-60,60);
					int Y = RNG == 0 ? Random.Range(0,2) == 1 ? 60 : -60 : Random.Range(-60,60);

					Mini.transform.position = new Vector3(X,Y,0);
					Mini.GetComponent<MiniDragon>().FatherDragon = this;
				}
			}


		}

		if(Whitetimer > 0)
		{
			Whitetimer -= Time.deltaTime*4;
		}
		White.color = new Vector4(1,1,1,Whitetimer);


		if(Chase)
		{
			Vector2 TowardsPlayer = transform.position - Main.Char.transform.position;
			RB.velocity = -TowardsPlayer.normalized*3;
		}
		else
		{
			RB.velocity = Vector3.zero;

		}

		if(Fireing)
		{
			Firecountdown -= Time.deltaTime;

			if(Firecountdown < 0)
			{
				if(FirePattern == 0)
				{
					
						Firecountdown = 1f;


						float dir = Mathf.Atan2(Main.Char.transform.position.y-transform.position.y,Main.Char.transform.position.x-transform.position.x);
						RotationHelper.transform.eulerAngles = new Vector3(0,0,dir * Mathf.Rad2Deg - 90);

						AltAttack = !AltAttack;
						if(AltAttack)
						{
						GameObject Fire =Instantiate(FireAttack[0],transform.position,RotationHelper.transform.rotation,transform.parent);
						Fire.GetComponent<DestroyOnDragonDeath>().Dragon = gameObject;

						}
						else
						{
						GameObject Fire =Instantiate(FireAttack[1],transform.position,RotationHelper.transform.rotation,transform.parent);
						Fire.GetComponent<DestroyOnDragonDeath>().Dragon = gameObject;

						}

				}

				if(FirePattern == 1)
				{
					Firecountdown = 1f;

					float dir = Mathf.Atan2(Main.Char.transform.position.y-transform.position.y,Main.Char.transform.position.x-transform.position.x);
					RotationHelper.transform.eulerAngles = new Vector3(0,0,dir * Mathf.Rad2Deg - 90);

					GameObject Fire =Instantiate(FireAttack[2],transform.position,RotationHelper.transform.rotation,transform.parent);
					Fire.GetComponent<DestroyOnDragonDeath>().Dragon = gameObject;

				}

				if(FirePattern == 2)
				{
					Firecountdown = 3f;

					float dir = Mathf.Atan2(Main.Char.transform.position.y-transform.position.y,Main.Char.transform.position.x-transform.position.x);
					RotationHelper.transform.eulerAngles = new Vector3(0,0,dir * Mathf.Rad2Deg - 90);

					GameObject Fire =Instantiate(FireAttack[3],transform.position,RotationHelper.transform.rotation,transform.parent);
					Fire.GetComponent<DestroyOnDragonDeath>().Dragon = gameObject;

				}

				if(FirePattern == 3)
				{

					Firecountdown = 1f;


					float dir = Mathf.Atan2(Main.Char.transform.position.y-transform.position.y,Main.Char.transform.position.x-transform.position.x);
					RotationHelper.transform.eulerAngles = new Vector3(0,0,dir * Mathf.Rad2Deg - 90);

					AltAttack = !AltAttack;
					if(AltAttack)
					{
						GameObject Fire =Instantiate(FireAttack[4],transform.position,RotationHelper.transform.rotation,transform.parent);
						Fire.GetComponent<DestroyOnDragonDeath>().Dragon = gameObject;

					}
					else
					{
						GameObject Fire =Instantiate(FireAttack[5],transform.position,RotationHelper.transform.rotation,transform.parent);
						Fire.GetComponent<DestroyOnDragonDeath>().Dragon = gameObject;

					}

				}

				if(FirePattern == 4)
				{
					Firecountdown = 0.33f;

					float dir = Mathf.Atan2(Main.Char.transform.position.y-transform.position.y,Main.Char.transform.position.x-transform.position.x);
					RotationHelper.transform.eulerAngles = new Vector3(0,0,dir * Mathf.Rad2Deg - 90);

					GameObject Fire = Instantiate(FireAttack[6],transform.position,RotationHelper.transform.rotation,transform.parent);
					Fire.GetComponent<DestroyOnDragonDeath>().Dragon = gameObject;

				}
			}
		}



	}


	void OnTriggerStay(Collider other)
	{
		if(other.tag == "PShot")
		{
			if(Health == 80)
			{
				DragonMusic.SetActive(true);
				Fireing = true;
			}
			Health--;
			Main.Char.WaitForShotOnDragon = false;
			Instantiate(BulletHit,other.transform.position,other.transform.rotation,other.transform.parent);

			Destroy(other.gameObject);
			Whitetimer = 1;

			HealthBar[0].text = Health + "/80";
			HealthBar[1].text = Health + "/80";
			HealthBar[2].text = Health + "/80";
			HealthBar[3].text = Health + "/80";
			HealthBar[4].text = Health + "/80";
			HealthBar[5].text = Health + "/80";
			HealthBar[6].text = Health + "/80";

			if(Health < 1)
			{
				//dead
				Dead = true;
				Instantiate(DeathExplosion,transform.position,transform.rotation,transform.parent);
				Main.Win = true;
				Destroy(gameObject);
			}

		}
	}


}
