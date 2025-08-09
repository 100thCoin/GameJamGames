using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromanver : MonoBehaviour {

	public int Health;
	public float FlashTimer;
	public SpriteRenderer SR;
	public SpriteRenderer JawR;
	public Environment Env;

	public GameObject VictoryGun;

	public GameObject Explosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Health <= 0)
		{

			Env.NecroDied = true;

			Instantiate(Explosion,transform.position,transform.rotation);
			Instantiate(Explosion,Env.Hand1Pivot.transform.position,transform.rotation);
			Instantiate(Explosion,Env.Hand2Pivot.transform.position,transform.rotation);

			Instantiate(VictoryGun,transform.position,transform.rotation);

			Destroy(Env.Hand1.gameObject);
			Destroy(Env.Hand2.gameObject);
			Destroy(Env.Music.gameObject);
			Destroy(gameObject);
		}


		FlashTimer-=Time.deltaTime;
		if(FlashTimer >0)
		{
			SR.color = new Vector4(1,0,0,1);
			JawR.color = new Vector4(1,0,0,1);
		}
		else
		{
			SR.color = new Vector4(1,1,1,1);
			JawR.color = new Vector4(1,1,1,1);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "PlayerFire")
		{
			other.GetComponent<FireballMove>().Explodee();
			Health--;
			FlashTimer = 0.1f;
		}


	}


}
