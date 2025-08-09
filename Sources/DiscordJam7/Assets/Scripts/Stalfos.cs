using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalfos : MonoBehaviour {
	public int dir; // 1 2 3 4 : r l u d
	// direction is most recently pressed button. If it's let go... then whatever is held? cancel conflicts.
	// Spacebar is sword

	public int Xdir;
	public int Ydir;
	public SpriteRenderer SR;
	public Animator Anim;
	public Sprite HorizShield;
	public Sprite VertShield;
	public Vector2[] ShieldPoses;
	public int speed;
	public int Wall;
	public Vector3[] DirSpeeds;
	public int ChangeDirection;

	public int AnimTimer;

	public bool Knockback;
	public int KnockbackTimer;
	public RuntimeAnimatorController DeadExplode;
	public GameObject Drop;
	public bool Dead;
	public int DeadTimer;

	public int IFrames;

	public bool Spitter;

	public int HP;
	public BoxCollider Col;
	void Start()
	{
		AnimTimer = Random.Range(0,17);
	}

	// Use this for initialization
	void Update () {


	}

	float[] rotVector = {180,0,90,-90};

	// Update is called once per frame
	void FixedUpdate () {
		if(Main.DataHolder.LinkDied)
		{
			Anim.runtimeAnimatorController = null;
			return;
		}

		if(Dead)
		{
			DeadTimer++;
			if(DeadTimer > 30)
			{
				if(Drop == null)
				{
					Drop =	Main.DataHolder.LinkDrops[Main.DataHolder.LinkKills%10];

				}

				if(Drop != null)
				{
					Instantiate(Drop,transform.position,Drop.transform.rotation);
				}

				Destroy(gameObject);
			}


			return;
		}


		if(!Spitter)
		{
			AnimTimer++;
			SR.flipX = (AnimTimer/8) % 2 == 1;
		}
		ChangeDirection--;
		if(ChangeDirection <=0)
		{
			ChangeDirection = Random.Range(15,120);
			dir = Random.Range(1,5);
		}

		if(!Knockback)
		{
			transform.position += (DirSpeeds[dir-1] * speed)/128f;
		}
		else
		{
			transform.position += (-DirSpeeds[dir-1] * speed)/64;;
		}


		if(Spitter)
		{
			transform.eulerAngles = new Vector3(0,0,rotVector[dir-1]);


		}



		IFrames--;
		if(IFrames >0)
		{
			SR.enabled = (IFrames/4)%2 == 1;
		}
		else
		{
			SR.enabled = true;
		}

		KnockbackTimer--;
		if(KnockbackTimer <=0)
		{
			Knockback = false;
		}
		WallGrace--;
	}

	int WallGrace;

	void OnTriggerStay(Collider other)
	{
		if(Knockback)
		{
			if(other.tag == "floor" || other.tag == "WallRight" || other.tag == "WallLeft" || other.tag == "CeilingBonk")
			{

				transform.position += (DirSpeeds[dir-1] * speed * Wall)/64f;

				Wall = 0;
				KnockbackTimer = 0;
				Knockback = false;

			}

		}
		if(WallGrace >0)
		{

		}
		else
		{
			if(other.tag == "Floor" && dir == 3)
			{
				dir=4;
				ChangeDirection = Random.Range(5,120);
				WallGrace = 3;
			}
			if(other.tag == "WallRight" && dir == 1)
			{
				dir=2;
				ChangeDirection = Random.Range(5,120);
				WallGrace = 3;

			}
			if(other.tag == "WallLeft" && dir == 2)
			{
				dir=1;
				ChangeDirection = Random.Range(5,120);
				WallGrace = 3;

			}
			if(other.tag == "CeilingBonk" && dir == 4)
			{
				dir=3;
				ChangeDirection = Random.Range(5,120);
				WallGrace = 3;

			}
			if(other.tag == "Sword" && IFrames<=0)
			{
				if(dir == Main.DataHolder.Link.Reverse[Main.DataHolder.Link.dir-1]) // facing player
				{	
					Knockback = true;
					KnockbackTimer = 5;
					ChangeDirection = Random.Range(15,24);
				}
				IFrames = 16;
				Hurt();
			}
		}
	}

	public GameObject SFX_SwordHit;
	void Hurt()
	{
		Instantiate(SFX_SwordHit);
		HP--;
		if(HP <=0)
		{
			Anim.runtimeAnimatorController = DeadExplode;
			Dead = true;
			Col.enabled = false;
			Main.DataHolder.LinkKills++;
			Main.DataHolder.ZeldaCurrentRoomKilledEnemies++;
		}

	}

}
