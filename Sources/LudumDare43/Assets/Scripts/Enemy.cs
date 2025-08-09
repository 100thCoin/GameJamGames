using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Sprite Norm;
	public Sprite Red;
	public float RedTimer;
	public GameObject Visual;

	public Vector2 shakepos;

	public float HP;

	public GameObject ItemDrop;
	public GameObject GoldDrop;
	public GameObject Gore;
	public GameObject Explosion;

	public bool Invincible;

	void Start()
	{
		shakepos = gameObject.transform.position;
	}

	void FixedUpdate()
	{

		if(RedTimer > 0)
		{
			RedTimer -= Time.deltaTime;

			Vector2 shake = new Vector2( Random.Range(-RedTimer,RedTimer), Random.Range(RedTimer,-RedTimer));
			gameObject.transform.position = shakepos + shake;

			if(RedTimer <= 0)
			{
				gameObject.transform.position = shakepos;
				Visual.gameObject.GetComponent<SpriteRenderer>().sprite = Norm;

			}


		}


		if(HP <= 0)
		{

			Instantiate(GoldDrop,gameObject.transform.position,gameObject.transform.rotation);
			if(ItemDrop != null)
			{
				Instantiate(ItemDrop,gameObject.transform.position,gameObject.transform.rotation);
			}

			//Instantiate(Gore,gameObject.transform.position,gameObject.transform.rotation);

			Destroy(gameObject);

		}


	}



	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.name == "ThrowBox" && !Invincible)
		{
			GameObject Sheep = other.gameObject.transform.parent.gameObject;
			Sheep.gameObject.GetComponent<AI_Sheep>().ThreeAxisVel = new Vector3(Sheep.gameObject.GetComponent<AI_Sheep>().ThreeAxisVel.x * -0.2f,Sheep.gameObject.GetComponent<AI_Sheep>().ThreeAxisVel.y * -0.2f,Sheep.gameObject.GetComponent<AI_Sheep>().ThreeAxisVel.z);
			other.gameObject.transform.parent.GetComponent<SphereCollider>().enabled = true;
			other.gameObject.SetActive(false);
			if(Sheep.GetComponent<AI_Sheep>().SheepType == 0){HP -= 25;}
			if(Sheep.GetComponent<AI_Sheep>().SheepType == 1){HP -= 40;}
			if(Sheep.GetComponent<AI_Sheep>().SheepType == 2){HP -= 75;}
			if(Sheep.GetComponent<AI_Sheep>().SheepType == 3){HP -= 75;}
			if(Sheep.GetComponent<AI_Sheep>().SheepType == 4){HP -= 150;}
			if(Sheep.GetComponent<AI_Sheep>().SheepType == 5){HP -= 55;}
			if(Sheep.GetComponent<AI_Sheep>().SheepType == 6){HP -= 30;}
			if(Sheep.GetComponent<AI_Sheep>().SheepType == 7){HP -= 45;}
			if(Sheep.GetComponent<AI_Sheep>().SheepType == 8){HP -= 200;}
			if(Sheep.GetComponent<AI_Sheep>().SheepType == 9){HP -= 30;}

			shakepos = gameObject.transform.position;

			RedTimer = 0.25f;
			Visual.gameObject.GetComponent<SpriteRenderer>().sprite = Red;
			if(Sheep.GetComponent<AI_Sheep>().SheepType == 6)
			{
				Sheep.GetComponent<AI_Sheep>().Timer = -0;
				Sheep.GetComponent<AI_Sheep>().RandomDelay = 0.5f;

				Sheep.gameObject.GetComponent<AI_Sheep>().ThreeAxisVel = new Vector3(Sheep.gameObject.GetComponent<AI_Sheep>().ThreeAxisVel.x * 2f,Sheep.gameObject.GetComponent<AI_Sheep>().ThreeAxisVel.y * 2f,Sheep.gameObject.GetComponent<AI_Sheep>().ThreeAxisVel.z);

				if(HP >0)
				{
					Sheep.GetComponent<AI_Sheep>().FairyRepeat = true;
					Sheep.GetComponent<AI_Sheep>().FairyTarget = gameObject;
				}
				else
				{
					Sheep.GetComponent<AI_Sheep>().FairyRepeat = false;
				}



			}

			if(Sheep.GetComponent<AI_Sheep>().SheepType == 8)
			{
				Instantiate(Explosion,other.gameObject.transform.transform.position,gameObject.transform.rotation);

				//Destroy(other.gameObject.transform.parent.gameObject);
				other.gameObject.transform.parent.gameObject.GetComponent<AI_Sheep>().KillSheep();

			}


		}
	
		if(other.gameObject.name == "Explosion")
		{

			RedTimer = 0.25f;
			Visual.gameObject.GetComponent<SpriteRenderer>().sprite = Red;
			HP -= 200;


		}




	}
}
