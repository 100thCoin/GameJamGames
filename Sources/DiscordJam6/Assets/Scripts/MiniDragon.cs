using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniDragon : MonoBehaviour {

	public GameObject Fireball;
	public GameObject BulletHit;
	public GameObject DeathExplosion;

	public Dataholder Main;
	public Rigidbody RB;

	public float Timer;

	public GameObject RotationHelper;

	public Dragon FatherDragon;

	public SpriteRenderer SR;

	void Start()
	{
		Main = GameObject.Find("Main").GetComponent<Dataholder>();
		Timer = 13;
	}

	void Update()
	{
		if(Main.Paused)
		{
			return;
		}
		Vector2 TowardsPlayer = transform.position - Main.Char.transform.position;
		RB.velocity = -TowardsPlayer.normalized*5;

		if(RB.velocity.x > 0)
		{
			SR.flipX = true;
		}
		else
		{
			SR.flipX = false;
		}


		Timer -= Time.deltaTime;

		if(Timer < 0)
		{
			float dir = Mathf.Atan2(Main.Char.transform.position.y-transform.position.y,Main.Char.transform.position.x-transform.position.x);
			RotationHelper.transform.eulerAngles = new Vector3(0,0,dir * Mathf.Rad2Deg - 90);

			GameObject Fire = Instantiate(Fireball,transform.position,RotationHelper.transform.rotation,transform.parent);
			Fire.GetComponent<DestroyOnDragonDeath>().Dragon = FatherDragon.gameObject;
			Timer += 1;
		}

		if(FatherDragon == null)
		{
			//dead
			Instantiate(DeathExplosion,transform.position,transform.rotation,transform.parent);
			Destroy(gameObject);
		}


	}



	void OnTriggerStay(Collider other)
	{
		if(other.tag == "PShot")
		{
			Instantiate(BulletHit,other.transform.position,other.transform.rotation,other.transform.parent);

			Destroy(other.gameObject);

				//dead
			Instantiate(DeathExplosion,transform.position,transform.rotation,transform.parent);
			Destroy(gameObject);

		}
	}
}
