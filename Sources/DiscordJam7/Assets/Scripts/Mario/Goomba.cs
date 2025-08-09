using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour {
	public SpriteRenderer SR;
	public float StartYpos;
	public bool Stationary;
	public float RiseTimer;
	public float SpawnDelay;
	public bool Move;
	public int Xspeed;
	public int Xsubpixel;
	public int YSpeed;
	public int YSubPixel;
	public int XPosition;
	public int YPosition;

	public int Dir;
	public int Speed;
	public bool OnGround;
	public BoxCollider Col;

	public bool Dead;
	public int DeadTimer;
	public Sprite DeadSprite;
	public Animator Anim;
	public bool Go;

	public bool Koopa;
	public bool WingedKoopa;
	public RuntimeAnimatorController Unwinged;
	public bool Shelled;
	public GameObject Coin;
	// Use this for initialization
	void OnEnable () {
		XPosition = Mathf.RoundToInt(transform.position.x * 16);
		YPosition = Mathf.RoundToInt(transform.position.y * 16);
	}

	public int Iframes;

	// Update is called once per frame
	void FixedUpdate () {
		if(Main.DataHolder.MarioClearWalkRight)
		{
			Instantiate(Coin,transform.position,transform.rotation);
			Destroy(gameObject);
		}

		if(Main.DataHolder.PauseMario)
		{
			return;
		}
		if(!Go)
		{
			if(Main.DataHolder.MarioCamera.transform.position.x > transform.position.x - 17)
			{
				Go = true;
			}
			return;
		}

		if(HarmTimer > 0)
		{
			HarmTimer--;
			if(HarmTimer == 0)
			{
				tag = "Goomba";
			}
		}
		if(Iframes > 0)
		{
			//Dead = false;
			Iframes--;
		}

		if(Dead)
		{
			if(WingedKoopa)
			{
				if(YSpeed >0)
				{
					YSpeed =0;
				}
				Iframes= 4;
				WingedKoopa = false;
				Anim.runtimeAnimatorController = Unwinged;
				Dead = false;
				return;
			}

			if(Koopa)
			{
				Dir = 0;
				tag = "Untagged";
				Anim.runtimeAnimatorController = null;
				SR.sprite = DeadSprite;
				Dead=false;
				Iframes = 16;
				Shelled = true;
			}
			else
			{
				Col.enabled = false;
				Anim.runtimeAnimatorController = null;
				if(DeadTimer == 0)
				{
					Instantiate(Main.DataHolder.Scorebubbles[Main.DataHolder.MarioCombo],transform.position + new Vector3(0,1,0),transform.rotation);
				}
				DeadTimer++;
				SR.sprite = DeadSprite;
				if(DeadTimer > 30)
				{
					Destroy(gameObject);
				}

				return;
			}
		}





		int speed = Speed*Dir;

		SR.flipX = Dir == 1;

		Xsubpixel += speed;
		if(speed >0)
		{
			while(Xsubpixel >4)
			{
				Xsubpixel-=4;
				XPosition++;
			}
		}
		else
		{
			while(Xsubpixel <0)
			{
				Xsubpixel+=4;
				XPosition--;
			}
		}


		if(!OnGround)
		{		

			if(YSpeed <= 32)
			{
				YSpeed -= WingedKoopa ? 3 : 5;  // GRAVITY
			}
			else
			{
				YSpeed--;  
			}
		}
		else
		{
			if(WingedKoopa)
			{
				YSpeed = 47;
			}
			else
			{
				YSpeed = 0;
			}
		}


		YSubPixel += YSpeed;
		if(YSpeed >0)
		{
			while(YSubPixel >8)
			{
				YSubPixel-=8;
				YPosition++;
			}
		}
		else
		{
			while(YSubPixel <0)
			{
				YSubPixel+=8;
				YPosition--;
			}
		}
		YSpeed = Mathf.Clamp(YSpeed,-69,69); //nice

		transform.position = new Vector3(XPosition/16f,YPosition/16f,0);




		OnGround = false;
	}


	void OnTriggerStay(Collider other)
	{
		if(Move)
		{
			if(other.tag == "Floor")
			{
				OnGround = true;
				YSpeed = 0;
				while((YPosition+512) % 32 !=16)
				{
					YPosition++;
				}
				transform.position = new Vector3(XPosition/16f,YPosition/16f,0);
			}
			if(other.tag == "OneWayFloor" && YSpeed <=0 && transform.position.y > other.transform.position.y+1)
			{

				OnGround = true;
				YSpeed = 0;
				while((YPosition+512) % 32 !=16)
				{
					YPosition++;
				}
				transform.position = new Vector3(XPosition/16f,YPosition/16f,0);

			}

			if(other.tag == "WallLeft")
			{
				Dir =1;
				XPosition++;

				transform.position = new Vector3(XPosition/16f,YPosition/16f,0);
			}
			if(other.tag == "WallRight")
			{
				Dir =-1;
				XPosition--;

				transform.position = new Vector3(XPosition/16f,YPosition/16f,0);
			}
			if(other.tag == "Player" && tag == "Untagged" && Shelled && Iframes<=0)
			{
				tag = "Respawn";
				Dir = other.transform.position.x > transform.position.x ? -1 : 1;
				HarmTimer = 5;
				Instantiate(Main.DataHolder.Scorebubbles[Main.DataHolder.MarioCombo],transform.position + new Vector3(0,1,0),transform.rotation);
				Speed = 24;
				Dead = false;
				SR.sprite = DeadSprite;
			}
		}
	}
	int HarmTimer;
}
