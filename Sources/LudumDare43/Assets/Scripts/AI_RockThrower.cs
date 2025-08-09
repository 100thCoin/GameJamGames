using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_RockThrower : MonoBehaviour {


	public Sprite Idle;
	public Sprite PreJump;
	public Sprite Jump;

	public Sprite Prepare;
	public Sprite Swoosh;
	public Sprite DonePunch;


	public bool DetectPlayer;
	public bool TooClose;


	public int Movenum;
	public bool JumpDelay;
	public float MoveDelay;
	public float NextMoveDelay;
	public float BigAttackDelay;
	public  bool AttackDelay;
	public bool AttackStopSwoosh;
	public bool Reset;
	public float ResetTimer;


	public GameObject Mary;
	public GameObject Visual;


	public Vector3 ThreeAxisVel;

	public float Speed;
	public float JumpHeight;

	public bool OnGround;
	public float Height;
	public float Gravity;

	public Vector2 ShakePos;

	public GameObject SheepHurtBox;

	public int RocksLeft;

	public GameObject Boulder;

	// Update is called once per frame
	void Update () {




		if(!OnGround)
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x + ThreeAxisVel.x, gameObject.transform.position.y + ThreeAxisVel.y,0);


			ThreeAxisVel = new Vector3(ThreeAxisVel.x,ThreeAxisVel.y,ThreeAxisVel.z - Gravity);

			Height = Height + ThreeAxisVel.z;

			if(ThreeAxisVel.z < 0)
			{
				Visual.gameObject.GetComponent<SpriteRenderer>().sprite = Idle;


			}

			Visual.gameObject.transform.localPosition = new Vector3(0,Height + 1,0);

			if(Height <= 0)
			{
				OnGround = true;
				Visual.gameObject.transform.localPosition = new Vector3(0,1,0);
				GetComponent<SphereCollider>().enabled = false;
				gameObject.transform.Find("ContactHurtbox").gameObject.SetActive(false);
			}

		}
		else
		{
			if(Movenum == 2)
			{
				NextMoveDelay += Time.deltaTime;
			}

			if(NextMoveDelay >= 0.5f && Movenum == 2)
			{
				Movenum++;
				Attack();


			}


		}


		if(Reset)
		{
			ResetTimer += Time.deltaTime;


			if(ResetTimer > 1)
			{
				Visual.gameObject.GetComponent<SpriteRenderer>().sprite = Idle;
			}
			if(ResetTimer > 2)
			{
				RocksLeft = 4;
				Movenum = 0;
				Reset = false;
			}
		}



		if(AttackDelay)
		{
			BigAttackDelay += Time.deltaTime;

			if(BigAttackDelay > 1)
			{
				gameObject.transform.position = new Vector2(ShakePos.x + Random.Range(BigAttackDelay-2,-BigAttackDelay+2) * 0.33f,ShakePos.y + Random.Range(BigAttackDelay-2,-BigAttackDelay+2) * 0.33f);
			}

			if(BigAttackDelay > 2)
			{
				
				ActuallyAttack();

				if(RocksLeft == 0)
				{

					BigAttackDelay = 0;
					AttackDelay = false;

					AttackStopSwoosh = true;

				}
				else
				{
					BigAttackDelay -= 0.25f;
					RocksLeft--;
				}



			}





		}


		if(AttackStopSwoosh)
		{
			BigAttackDelay += Time.deltaTime;

			if(BigAttackDelay > 0.13f)
			{
				DoneAttack();
			}
		}


		if(JumpDelay)
		{
			MoveDelay += Time.deltaTime;
			if(MoveDelay > 1.5f)
			{

				ActuallyJump();
			}
		}



		if(DetectPlayer)
		{
			Vector2 Dir = new Vector2(Mary.transform.position.x - gameObject.transform.position.x,Mary.transform.position.y - gameObject.transform.position.y - 1).normalized;

			//Vector2 Dist = new Vector2(Mary.transform.position.x - gameObject.transform.position.x,Mary.transform.position.y - gameObject.transform.position.y - 1);

			if(Dir.x < 0)
			{
				Visual.GetComponent<SpriteRenderer>().flipX = true;
			}
			else
			{
				Visual.GetComponent<SpriteRenderer>().flipX = false;

			}


			if(Movenum == 0)
			{
				DoJump();
				Movenum++;
			}



		}




		//  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH
		gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-1 + ((gameObject.transform.position.y - 1) * 0.0001f));

	}




	void DoJump()
	{

		Visual.GetComponent<SpriteRenderer>().sprite = PreJump;
		JumpDelay = true;
		MoveDelay = 0;

	}

	void ActuallyJump()
	{

		Vector2 Dir = new Vector2(Mary.transform.position.x - gameObject.transform.position.x,Mary.transform.position.y - gameObject.transform.position.y - 1).normalized;

		Vector2 Dist = new Vector2(Mary.transform.position.x - gameObject.transform.position.x,Mary.transform.position.y - gameObject.transform.position.y - 1);

		Visual.GetComponent<SpriteRenderer>().sprite = Jump;

		ThreeAxisVel = new Vector3(Dir.x * Speed,Dir.y * Speed,JumpHeight);

		MoveDelay = 0;
		JumpDelay = false;
		OnGround = false;
		Height = 0.1f;

		GetComponent<SphereCollider>().enabled = false;
		gameObject.transform.Find("ContactHurtbox").gameObject.SetActive(false);

		Movenum++;

	}


	void Attack()
	{

		Visual.GetComponent<SpriteRenderer>().sprite = Prepare;

		BigAttackDelay = 0;
		AttackDelay = true;
		ShakePos = gameObject.transform.position;

	}

	void ActuallyAttack()
	{

		Visual.GetComponent<SpriteRenderer>().sprite = Swoosh;


		gameObject.transform.position = ShakePos;
		//SheepHurtBox.SetActive(true);
		//SheepHurtBox.transform.localPosition = new Vector3(0,0,0);

		Instantiate(Boulder,gameObject.transform.position,gameObject.transform.rotation);
		Instantiate(Boulder,gameObject.transform.position,gameObject.transform.rotation);


	}


	void DoneAttack()
	{

		Visual.GetComponent<SpriteRenderer>().sprite = DonePunch;
		AttackStopSwoosh = false;
		Reset = true;
		ResetTimer = 0;
		//SheepHurtBox.SetActive(false);
		//SheepHurtBox.transform.localPosition = new Vector3(0,0,-500);

	}


}
