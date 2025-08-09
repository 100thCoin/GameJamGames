using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Mushroom : MonoBehaviour {

	public Sprite Idle;
	public Sprite Move;
	public Sprite Spore;

	public GameObject Sporelosion;

	public float Timer;
	public GameObject Visual;
	public bool OnGround;
	public float Gravity;
	public float Height;

	public Vector3 ThreeAxisVel;

	public float RandomDelay;

	public GameObject Mary;

	public float Speed;
	public float JumpHeight;

	public bool DetectPlayer;

	public bool DoOnce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(DetectPlayer)
		{

			Timer += Time.fixedDeltaTime;

			Visual.gameObject.transform.eulerAngles = new Vector3(0,0,0);

				if(OnGround && Timer >= RandomDelay)
				{
				int r = Random.Range (1,5);
				if(r == 3)
				{
					Sploosh();

				}
				else
				{
					Jump();
				}
					Timer = 0;
					if(!gameObject.GetComponent<SphereCollider>().enabled)
					{
						gameObject.GetComponent<SphereCollider>().enabled = true;

					}

				}

				if(!OnGround)
				{
					gameObject.transform.position = new Vector3(gameObject.transform.position.x + ThreeAxisVel.x, gameObject.transform.position.y + ThreeAxisVel.y,0);

						ThreeAxisVel = new Vector3(ThreeAxisVel.x,ThreeAxisVel.y,ThreeAxisVel.z - Gravity);

						Height = Height + ThreeAxisVel.z;

						Visual.gameObject.transform.localPosition = new Vector3(0,Height + 1,0);

						if(Height <= 0)
						{
							OnGround = true;
						Visual.gameObject.transform.localPosition = new Vector3(0,1,0);

						}


				}
			else if(!DoOnce)
			{
				DoOnce = true;
				Visual.GetComponent<SpriteRenderer>().sprite = Idle;

			}

				//  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH
				gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-1 + ((gameObject.transform.position.y - 1) * 0.0001f));



		}

		}

		void Jump()
		{
			//get direction

			Vector2 Dir = new Vector2(Mary.transform.position.x - gameObject.transform.position.x,Mary.transform.position.y - gameObject.transform.position.y - 1).normalized;


			if(Dir.x < 0)
			{
				Visual.GetComponent<SpriteRenderer>().flipX = true;
			}
			else
			{
				Visual.GetComponent<SpriteRenderer>().flipX = false;

			}

		Visual.GetComponent<SpriteRenderer>().sprite = Move;


			OnGround = false;

			ThreeAxisVel = new Vector3(Dir.x * Speed,Dir.y * Speed,JumpHeight);

				RandomDelay = Random.Range(0.5f,1.5f);

		DoOnce = false;


		}


	void Sploosh()
	{
		//get direction

		Vector2 Dir = new Vector2(Mary.transform.position.x - gameObject.transform.position.x,Mary.transform.position.y - gameObject.transform.position.y - 1).normalized;
		Visual.GetComponent<SpriteRenderer>().sprite = Spore;


		if(Dir.x < 0)
		{
			Visual.GetComponent<SpriteRenderer>().flipX = true;
		}
		else
		{
			Visual.GetComponent<SpriteRenderer>().flipX = false;

		}

		//OnGround = false;

		//ThreeAxisVel = new Vector3(Dir.x * Speed,Dir.y * Speed,JumpHeight);

		GameObject Sploof = Instantiate(Sporelosion,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,0.1f),gameObject.transform.rotation) as GameObject;
		Sploof.name = "SheepHurtbox";

		RandomDelay = Random.Range(1,2f);



	}


}
