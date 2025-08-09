using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioItem : MonoBehaviour {



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
	// Use this for initialization
	void Start () {
		Dir = (transform.position.x > Main.DataHolder.Mario.transform.position.x) ? 1 : -1;
		StartYpos = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Main.DataHolder.PauseMario)
		{
			return;
		}
		if(!Move)
		{
			if(SpawnDelay >= 0)
			{
				SpawnDelay -=Time.fixedDeltaTime;
			}
			else
			{
				Col.enabled = true;

				SR.enabled = true;
				RiseTimer +=Time.fixedDeltaTime*2;
				transform.position = new Vector3(transform.position.x,StartYpos+RiseTimer,0);
				SR.sortingOrder = -1;
				if(RiseTimer >=2)
				{
					SR.sortingOrder = 1;
					Move = true;
					XPosition = Mathf.RoundToInt(transform.position.x * 16);
					YPosition = Mathf.RoundToInt(transform.position.y * 16);
				}
			}
		}
		else
		{


			int speed = Speed*Dir;

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
					YSpeed -= 5;  // GRAVITY
				}
				else
				{
					YSpeed--;  
				}
			}
			else
			{
				YSpeed = 0;
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
		}




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
		}
	}

}
