using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Skeleton : MonoBehaviour {

	public float Timer;
	public float Dist;
	public bool Jumping;
	public bool PreJump;

	public GameObject Gibs;

	public GameObject Player;

	public RuntimeAnimatorController Idle;
	public RuntimeAnimatorController Prejump;
	public RuntimeAnimatorController Jump;

	public SpriteRenderer sr;

	public bool Activated;

	public DataHolder Main;

	void Start()
	{
		Player = GameObject.Find("Player");
		Main = GameObject.Find("Main").GetComponent<DataHolder>();
	}

	void Update () {
	
		Dist = Mathf.Abs(transform.position.x - Player.transform.position.x);

		if(Dist < 14)
		{
			Activated = true;
		}

		if(Activated)
		{
			Timer += Time.deltaTime;
			if(!Jumping && !PreJump)
			{
				sr.flipX = Player.transform.position.x < transform.position.x;

				if(Timer > 2)
				{
					PreJump = true;
					GetComponent<Animator>().runtimeAnimatorController = Prejump;
					Timer = 0;
				}
			}

			if(PreJump)
			{
				sr.flipX = Player.transform.position.x < transform.position.x;

				if(Timer > 1)
				{
					Jumping = true;
					PreJump = false;
					GetComponent<Animator>().runtimeAnimatorController = Jump;
					GetComponent<Rigidbody>().velocity = new Vector3(10 * -new Vector2(transform.position.x-Player.transform.position.x,0).normalized.x,35,0);
					Timer = 0;
				}
			}



		}


		if(Main.DragonBeat)
		{
			Instantiate(Gibs,transform.position,transform.rotation,transform.parent);

			Destroy(gameObject);

		}

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "EnemyDeath")
		{

			Instantiate(Gibs,other.transform.position,other.transform.rotation,transform.parent);

			Destroy(gameObject);

		}

		if(other.tag == "Ground")
		{

			Jumping = false;
			GetComponent<Animator>().runtimeAnimatorController = Idle;

		}

	}

}
