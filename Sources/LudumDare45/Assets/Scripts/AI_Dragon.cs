using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Dragon : MonoBehaviour {

	public SpriteRenderer sr;

	public bool Activated;

	public DataHolder Main;
	public GameObject Player;

	public GameObject Exclaim;
	public float Dist;
	public bool ActiveOnce;

	public float DeathTimer;
	public bool Dead;

	public GameObject Gore;
	public GameObject Boom;

	public RuntimeAnimatorController Rekt;
	public RuntimeAnimatorController Panic;

	public Animator anim;

	public bool KaboomTime;
	public int BoomCount;
	public bool WaitASec;

	public GameObject FearParticles;

	public GameObject Chest;

	void Start()
	{
		Player = GameObject.Find("Player");
		Main = GameObject.Find("Main").GetComponent<DataHolder>();
	}
	
	// Update is called once per frame
	void Update () {

		if(Dead)
		{
			anim.runtimeAnimatorController = Rekt;
			DeathTimer += Time.deltaTime;
			if(!KaboomTime)
			{
				if(WaitASec)
				{
					if(DeathTimer > 1)
					{
						DeathTimer = 0;

						Chest.SetActive(true);
						Instantiate(Boom,Chest.transform.position,Chest.transform.rotation,Chest.transform);

						Destroy(gameObject);
					}


				}
				else
				{
				if(DeathTimer > 2)
				{
					KaboomTime = true;
					Instantiate(Gore,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.parent);
					DeathTimer-=3;
					transform.parent.transform.position -= new Vector3(2,2,0);
					transform.parent.localScale = new Vector3(2,1,1);
						FearParticles.SetActive(false);
				}
				}
			}
			else
			{
				if(DeathTimer > 0.125f && BoomCount < 12)
				{
					BoomCount++;
					DeathTimer-= 0.125f;
					Instantiate(Boom,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.parent);
				}
				if(BoomCount >= 12)
				{
					KaboomTime = false;
					DeathTimer -= 2;
					WaitASec = true;
				}
			}


		}


		Dist = Mathf.Abs(transform.position.x - Player.transform.position.x);

		if(Dist < 14)
		{
			Activated = true;
			if(!ActiveOnce)
			{
				anim.runtimeAnimatorController = Panic;
				ActiveOnce = true;
				FearParticles.SetActive(true);
				Instantiate(Exclaim,transform.position + new Vector3(0,6,0),transform.rotation,transform.parent);

			}
		}

		if(Activated && !Dead)
		{
			transform.localPosition = new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),0);

		}



	}



	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "EnemyDeath")
		{
			Dead = true;
			GameObject.Find("Main").GetComponent<DataHolder>().LockCamera = true;
			GameObject.Find("Main").GetComponent<DataHolder>().DragonBeat = true;

		}

	}


}
