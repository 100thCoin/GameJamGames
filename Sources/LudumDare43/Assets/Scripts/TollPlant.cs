using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TollPlant : MonoBehaviour {

	public bool Fed;

	public Sprite SnapSwoop;
	public Sprite Closed;
	public Sprite VineDead;

	public float Timer;

	public GameObject Visual;

	public GameObject VineDamageer;

	void Update()
	{

		if(Visual.gameObject.GetComponent<SpriteRenderer>().sprite == SnapSwoop)
		{
			Timer += Time.deltaTime;

			if(Timer > 0.15f)
			{
				Visual.gameObject.GetComponent<SpriteRenderer>().sprite = Closed;
				gameObject.GetComponent<Enemy>().Invincible = false;
				gameObject.GetComponent<Enemy>().RedTimer = 0.25f;

				Destroy(VineDamageer.gameObject);
				gameObject.GetComponent<SpriteRenderer>().sprite = VineDead;

				Destroy(this);
			}


		}

		//  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH
		Visual.gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-1 + ((gameObject.transform.position.y - 1) * 0.0001f));


	}


	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.name == "ThrowBox" && !Fed)
		{


			if(other.transform.parent.GetComponent<AI_Sheep>().SheepType == 8)
			{
				//instadead


			}

			other.gameObject.transform.parent.gameObject.GetComponent<AI_Sheep>().KillSheep();

			//Destroy(other.gameObject.transform.parent.gameObject);
			Fed = true;
			Visual.gameObject.GetComponent<SpriteRenderer>().sprite = SnapSwoop;


		}



	}



}
