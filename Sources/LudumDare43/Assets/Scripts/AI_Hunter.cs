using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Hunter : MonoBehaviour {

	public RuntimeAnimatorController Idle;
	public Sprite PreJump;
	public Sprite Jump;

	public Sprite Prepare;
	public Sprite AimUp;
	public Sprite AimDown;
	public Sprite AimLR;

	public Sprite DonePunch;


	public bool DetectPlayer;
	public bool TooClose;


	public int Movenum;
	public bool AimDelay;
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


	public Vector2 ShakePos;

	public GameObject SheepHurtBox;
	public GameObject GunArrow;

	public GameObject Bang;

	// Update is called once per frame
	void Update () {


		if(Movenum == 2)
		{
			Movenum++;
			Attack();


		}



		if(Reset)
		{
			ResetTimer += Time.deltaTime;



			if(ResetTimer > 1)
			{
				Visual.gameObject.GetComponent<Animator>().runtimeAnimatorController = Idle;
			}
			if(ResetTimer > 2)
			{
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
			}
		}


		if(AttackStopSwoosh)
		{
			BigAttackDelay += Time.deltaTime;

			if(BigAttackDelay > 0.23f)
			{
				DoneAttack();
			}
		}


		if(AimDelay)
		{
			MoveDelay += Time.deltaTime;
			if(MoveDelay > 0.2f)
			{

				ActuallyAim();
			}
		}



		if(DetectPlayer && Movenum == 0)
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


				LiftGun();
				Movenum++;



		}




		//  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH
		gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-1 + ((gameObject.transform.position.y - 1) * 0.0001f));

	}




	void LiftGun()
	{
		Visual.gameObject.GetComponent<Animator>().runtimeAnimatorController = null;

		Visual.GetComponent<SpriteRenderer>().sprite = Prepare;
		AimDelay = true;
		MoveDelay = 0;

	}

	void ActuallyAim()
	{

		Vector2 Dir = new Vector2(Mary.transform.position.x - gameObject.transform.position.x,Mary.transform.position.y - gameObject.transform.position.y - 1).normalized;

		Vector2 Dist = new Vector2(Mary.transform.position.x - gameObject.transform.position.x,Mary.transform.position.y - gameObject.transform.position.y - 1);


		if(Mathf.Abs(Dist.x) > Mathf.Abs(Dist.y))
		{
			Visual.GetComponent<SpriteRenderer>().sprite = AimLR;

		}
		else if(Dist.y >0)
		{
			Visual.GetComponent<SpriteRenderer>().sprite = AimUp;

		}
		else
		{
			Visual.GetComponent<SpriteRenderer>().sprite = AimDown;

		}






		MoveDelay = 0;

		AimDelay =false;
		//GetComponent<SphereCollider>().enabled = false;
		//gameObject.transform.Find("ContactHurtbox").gameObject.SetActive(false);

		Movenum++;

	}


	void Attack()
	{


		//Visual.GetComponent<SpriteRenderer>().sprite = Prepare;

		GunArrow.SetActive(true);
		GunArrow.gameObject.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(Mary.transform.position.y - gameObject.transform.position.y -1,Mary.transform.position.x - gameObject.transform.position.x) * Mathf.Rad2Deg);


		BigAttackDelay = 0;
		AttackDelay = true;
		ShakePos = gameObject.transform.position;

	}

	void ActuallyAttack()
	{
		print("okay");

		//Visual.GetComponent<SpriteRenderer>().sprite = Swoosh;

		BigAttackDelay = 0;
		AttackDelay = false;

		AttackStopSwoosh = true;
		gameObject.transform.position = ShakePos;
		//SheepHurtBox.SetActive(true);
		SheepHurtBox.transform.localPosition = new Vector3(0,0,0);

		if(Visual.GetComponent<SpriteRenderer>().sprite == AimLR)
		{
			if(Visual.GetComponent<SpriteRenderer>().flipX)
			{
				Instantiate(Bang,new Vector3(gameObject.transform.position.x - 1.85f,gameObject.transform.position.y + 0.22f,0),gameObject.transform.rotation);
			}
			else
			{
				gameObject.transform.eulerAngles = new Vector3(0,0,180);

				Instantiate(Bang,new Vector3(gameObject.transform.position.x + 1.85f,gameObject.transform.position.y + 0.22f,0),gameObject.transform.rotation);

				gameObject.transform.eulerAngles = new Vector3(0,0,0);


			}

		}
		else if(Visual.GetComponent<SpriteRenderer>().sprite == AimUp)
		{

			gameObject.transform.eulerAngles = new Vector3(0,0,90);

			Instantiate(Bang,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 2.11f,0),gameObject.transform.rotation);

			gameObject.transform.eulerAngles = new Vector3(0,0,0);


		}
		else if(Visual.GetComponent<SpriteRenderer>().sprite == AimDown)
		{

			gameObject.transform.eulerAngles = new Vector3(0,0,-90);

			Instantiate(Bang,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y - 2.11f,0),gameObject.transform.rotation);

			gameObject.transform.eulerAngles = new Vector3(0,0,0);


		}

	}


	void DoneAttack()
	{
		print("okay");

		//Visual.GetComponent<SpriteRenderer>().sprite = DonePunch;
		AttackStopSwoosh = false;
		Reset = true;
		ResetTimer = 0;
		//SheepHurtBox.SetActive(false);
		SheepHurtBox.transform.localPosition = new Vector3(0,0,-500);
		GunArrow.SetActive(false);

	}


}
