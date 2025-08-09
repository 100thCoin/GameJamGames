using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour {

	public bool Winged;
	public Vector3 speed;
	public float speedLimit;
	public Animator Anim;
	public RuntimeAnimatorController Idle;
	public RuntimeAnimatorController Run;
	public RuntimeAnimatorController Fly;
	public SpriteRenderer SR;

	public float FireballTimer;

	public GameObject Fireball;
	public GameObject ShadowlessFireball;

	public bool QueueDeath;
	public bool Dead;
	public GameObject OldBoarders;
	public bool EnabledFlightOnce;

	public int HP;

	public GameObject DeathParts;

	public SpriteRenderer[] HPHearts;
	public float HeartVis;

	public bool DoneTooltip1;
	public bool DoneTooltip2;

	public GameObject Tool1;
	public GameObject Tool2;
	// Use this for initialization
	public Environment Env;

	public GameObject PlayerDiedJingle;

	public float smoldelay = 1;

	public GameObject Hurt;

	void Start () {
		
	}


	void Update()
	{
		smoldelay-= Time.deltaTime;
		if(Input.GetKeyDown(KeyCode.Space) )
		{
			DoneTooltip2 = true;
			Tool2.SetActive(false);
		}

		if(HeartVis > 0)
		{
			HeartVis -= Time.deltaTime*0.5f;
			HPHearts[0].color = new Vector4(1,1,1,0.5f+HeartVis);
			HPHearts[1].color = new Vector4(1,1,1,0.5f+HeartVis);
			HPHearts[2].color = new Vector4(1,1,1,0.5f+HeartVis);
			HPHearts[3].color = new Vector4(1,1,1,0.5f+HeartVis);
			HPHearts[4].color = new Vector4(1,1,1,0.5f+HeartVis);
			HPHearts[5].color = new Vector4(1,1,1,0.5f+HeartVis);
			int i = 0;
			while(i < 6)
			{
				HPHearts[i].enabled = i < HP;
				i++;
			}
		}

		if(HP <= 0 && !Dead)
		{
			QueueDeath = true;

		}

		if(QueueDeath)
		{
			Dead = true;
			QueueDeath = false;
			Instantiate(DeathParts,transform.position,transform.rotation,transform.parent);
			Env.PlayerDied = true;
			Instantiate(PlayerDiedJingle);
			Destroy(Env.Music.gameObject);
			gameObject.SetActive(false);
		}

		if(FireballTimer >0 )
		{
			FireballTimer -= Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.Space) && smoldelay <=0)
		{
			if(FireballTimer <= 0)
			{
				FireballTimer += 0.25f;
				if(Winged)
				{
					Instantiate(ShadowlessFireball,transform.position + new Vector3(0,-1,0),transform.rotation,transform.parent);
				}
				else
				{
					Instantiate(Fireball,transform.position + new Vector3(0,-1,0),transform.rotation,transform.parent);
				}

			}

		}

	}


	
	// Update is called once per frame
	void FixedUpdate () {

		if(speed.magnitude >0)
		{
			DoneTooltip1 = true;
			Tool1.SetActive(false);
		}


		if(!Winged)
		{
			speed = Vector3.zero;

			if(Input.GetKey(KeyCode.W))
			{
				speed = new Vector3(0,1);
			}
			if(Input.GetKey(KeyCode.S))
			{
				speed = new Vector3(0,speed.y-1);
			}

			if(Input.GetKey(KeyCode.A))
			{
				speed = new Vector3(-1,speed.y);
			}
			if(Input.GetKey(KeyCode.D))
			{
				speed = new Vector3(speed.x+1,speed.y);
			}

			speed = speed.normalized;
			speed = new Vector3(speed.x, speed.y*0.5f)*speedLimit;

			transform.position += speed * Time.fixedDeltaTime;

			if(speed.x != 0)
			{
				SR.flipX = speed.x < 0;
			}

			Anim.runtimeAnimatorController = speed.magnitude != 0 ? Run : Idle;

			if(transform.position.y > -1)
			{
				transform.position = new Vector3(transform.position.x,-1,transform.position.z);
			}

			if(transform.position.x > 11)
			{
				transform.position = new Vector3(11,transform.position.y,transform.position.z);
			}
			if(transform.position.x < -11)
			{
				transform.position = new Vector3(-11,transform.position.y,transform.position.z);
			}

		}
		if(Winged)
		{
			if(!EnabledFlightOnce)
			{
				EnabledFlightOnce = true;
				Destroy(OldBoarders);
			}

			speed = Vector3.zero;

			if(Input.GetKey(KeyCode.W))
			{
				speed = new Vector3(0,1);
			}
			if(Input.GetKey(KeyCode.S))
			{
				speed = new Vector3(0,speed.y-1);
			}

			if(Input.GetKey(KeyCode.A))
			{
				speed = new Vector3(-1,speed.y);
			}
			if(Input.GetKey(KeyCode.D))
			{
				speed = new Vector3(speed.x+1,speed.y);
			}

			speed = speed.normalized;
			speed = new Vector3(speed.x, speed.y)*speedLimit;

			transform.position += speed * Time.fixedDeltaTime;

			if(speed.x != 0)
			{
				SR.flipX = speed.x < 0;
			}


			if(transform.position.y > -2)
			{
				transform.position = new Vector3(transform.position.x,-2,transform.position.z);
			}
			if(transform.position.y < -7)
			{
				transform.position = new Vector3(transform.position.x,-7,transform.position.z);
			}
			if(transform.position.x > 11)
			{
				transform.position = new Vector3(11,transform.position.y,transform.position.z);
			}
			if(transform.position.x < -11)
			{
				transform.position = new Vector3(-11,transform.position.y,transform.position.z);
			}

		}


	}
}
