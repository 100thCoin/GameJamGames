using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkMover : MonoBehaviour {

	public int dir; // 1 2 3 4 : r l u d
	// direction is most recently pressed button. If it's let go... then whatever is held? cancel conflicts.
	// Spacebar is sword

	public int Xdir;
	public int Ydir;
	public bool MostRecentlyX;
	public SpriteRenderer SR;
	public Animator Anim;
	public RuntimeAnimatorController Idle;
	public RuntimeAnimatorController IdleSide;
	public RuntimeAnimatorController IdleBack;
	public SpriteRenderer Shield;
	public Sprite HorizShield;
	public Sprite VertShield;
	public Vector2[] ShieldPoses;
	public int speed;
	public int Wall;
	public Vector3[] DirSpeeds;

	public bool DoingSword;
	public int SwordFrame;

	public Vector3[] SwordPos;
	public float[] SwordRots;

	public SpriteRenderer Sword;

	public bool Knockback; // send direction against the way you're facing.
	public int KnockbackTimer;
	public int[] Reverse = {2,1,4,3};
	int Iframes;

	public int TransitionSpeed;
	public bool ScreenTrans;

	public bool HoldingItem;
	public int HoldingItemTimer;
	public RuntimeAnimatorController Holding;

	public GameObject SFX_LinkHurt;

	// Use this for initialization
	void Update () {
		if(!ScreenTrans)
		{
			if(Input.GetKeyDown(KeyCode.W))
			{
				MostRecentlyX = false;
			}
			if(Input.GetKeyDown(KeyCode.S))
			{
				MostRecentlyX = false;
			}
			if(Input.GetKeyDown(KeyCode.A))
			{
				MostRecentlyX = true;
			}
			if(Input.GetKeyDown(KeyCode.D))
			{
				MostRecentlyX = true;
			}
			if(Input.GetKeyDown(KeyCode.Space) &&!DoingSword && Main.DataHolder.LinkHasSword)
			{
				DoingSword = true;
				SwordFrame = 0;
			}
		}
	}

	public SpriteRenderer ItemSword;
	public SpriteRenderer ItemTriforce;
	public bool FreezeWin;
	public RuntimeAnimatorController Deadd;
	// Update is called once per frame
	void FixedUpdate () {
		if(Main.DataHolder.LinkDied)
		{
			Anim.runtimeAnimatorController = Deadd;
			return;
		}

		if(FreezeWin)
		{
			Anim.runtimeAnimatorController = Holding;
			ItemTriforce.enabled = true;
			return;
		}


		if(HoldingItem)
		{
			HoldingItemTimer++;
			Anim.runtimeAnimatorController = Holding;
			ItemSword.enabled = true;
			if(HoldingItemTimer > 60)
			{
				HoldingItem = false;
				ItemSword.enabled = false;
				Anim.runtimeAnimatorController = Idle;

			}

			return;
		}


		Xdir = 0;
		Ydir = 0;

		Xdir += Input.GetKey(KeyCode.D) ? 1 : 0;
		Xdir += Input.GetKey(KeyCode.A) ? -1 : 0;
		Ydir += Input.GetKey(KeyCode.W) ? 1 : 0;
		Ydir += Input.GetKey(KeyCode.S) ? -1 : 0;



		if(DoingSword)
		{
			Xdir = 0;
			Ydir = 0;
			SwordFrame++;

			Sword.transform.localPosition = SwordPos[dir-1];
			Sword.transform.eulerAngles = new Vector3(0,0,SwordRots[dir-1]);
				
			if(SwordFrame > 4)
			{
				Sword.gameObject.SetActive(true);
			}

			// retract sword
			// 13, 14, 15, gone.
			if(SwordFrame ==13)
			{
				Sword.transform.localPosition -= DirSpeeds[dir-1]*0.5f;
			}
			if(SwordFrame ==14)
			{
				Sword.transform.localPosition -= DirSpeeds[dir-1]*0.5f;

			}
			if(SwordFrame ==15)
			{
				Sword.transform.localPosition -= DirSpeeds[dir-1]*0.5f;

			}
			if(SwordFrame >=16)
			{
				Sword.gameObject.SetActive(false);

				DoingSword = false;
			}
		}

		if(Knockback || ScreenTrans)
		{
			Xdir = 0;
			Ydir = 0;
		}

		if(Xdir ==0 && Ydir == 0)
		{
			//
		}
		else
		{
			if(MostRecentlyX)
			{
				if(Xdir == 0)
				{
					dir = (Ydir == -1) ? 3 : (Ydir == 0) ? 0 : 4;
				}
				else
				{
					dir = (Xdir == -1) ? 2 : 1;
				}
			}
			else
			{
				if(Ydir == 0)
				{
					dir = (Xdir == -1) ? 2 : (Xdir == 0) ? 0 : 1;
				}
				else
				{
					dir = (Ydir == -1) ? 3 : 4;
				}
			}

			if(dir == 1)
			{
				Anim.runtimeAnimatorController = IdleSide;
				SR.flipX = false;
				Shield.sprite = HorizShield;

			}
			else if(dir == 2)
			{
				Anim.runtimeAnimatorController = IdleSide;
				SR.flipX = true;
				Shield.sprite = HorizShield;
			}
			else if(dir == 3)
			{
				Anim.runtimeAnimatorController = Idle;
				Shield.sprite = VertShield;

			}
			else if(dir == 4)
			{
				Anim.runtimeAnimatorController = IdleBack;
				Shield.sprite = VertShield;

			}

			Shield.transform.localPosition = ShieldPoses[dir-1];


			transform.position += (DirSpeeds[dir-1] * speed * Wall)/128f;





			// if you cross 14.5 x or 9.5 y, screen transition

		}
		if(ScreenTrans)
		{
			transform.position += (DirSpeeds[dir-1] * TransitionSpeed)/128f;

		}
		else if(Knockback)
		{
			transform.position -= (DirSpeeds[dir-1] * speed * Wall)/64f;

		}
		KnockbackTimer--;
		if(KnockbackTimer<=0)
		{
			Knockback = false;

		}

		Iframes--;
		if(Iframes >0)
		{
			SR.enabled = (Iframes/4)%2 == 1;
		}
		else
		{
			SR.enabled = true;
		}

		Wall = 1; // multiplier. set to 0 is touching a wall;
	}

	void OnTriggerStay(Collider other)
	{

		if(Knockback)
		{
			if(other.tag == "Floor" || other.tag == "WallRight" || other.tag == "WallLeft" || other.tag == "CeilingBonk")
			{
				HitWall();
			}

		}

		if(other.tag == "Floor" && dir == 3)
		{
			HitWall();


		}
		if(other.tag == "WallRight" && dir == 1)
		{
			HitWall();


		}
		if(other.tag == "WallLeft" && dir == 2)
		{
			HitWall();


		}
		if(other.tag == "CeilingBonk" && dir == 4)
		{
			HitWall();

		}
		if(other.tag == "Hurt" && Iframes <= 0)
		{
			Iframes = 60;

			Hurt();
		}
		if(other.name == "LOCK" && Main.DataHolder.LinkKeys > 0)
		{
			Destroy(other.gameObject);
			Main.DataHolder.LinkKeys--;
		}
	}

	void HitWall()
	{
		if(Knockback)
		{
			transform.position += (DirSpeeds[dir-1] * speed * Wall)/64f;
		}
		else
		{
			transform.position += (-DirSpeeds[dir-1] * speed * Wall)/128f;
		}
		Wall = 0;
		KnockbackTimer = 0;
		Knockback = false;
	}


	void Hurt()
	{
		Instantiate(SFX_LinkHurt);
		Main.DataHolder.LinkHP--;
		if(Main.DataHolder.LinkHP >0)
		{
			Knockback = true;
			KnockbackTimer = 8;
			Iframes = 60;
		}
		else
		{
			//die
		}
	}


}
