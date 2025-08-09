using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeldaItem : MonoBehaviour {

	public bool Key;
	public bool Heart;
	public bool Sword;
	bool doOnce = false;
	public bool DestoyOverTime;
	public int Timer=360;
	public SpriteRenderer SR;
	public bool Triforce;
	public GameObject SFX_Sword;
	void Update()
	{
		if(DestoyOverTime)
		{
			Timer--;
			if(Timer < 120)
			{
				SR.enabled = (Timer/2%2)==1;


			}
			if(Timer <0)
			{
				Destroy(gameObject);
			}

		}

	}

	void FixedUpdate()
	{
		if(!Triforce)
		{
			return;
		}

		transform.position -= (transform.position-Main.DataHolder.Link.transform.position).normalized*Time.fixedDeltaTime * 6;


	}

	void OnTriggerEnter(Collider other)
	{
		if(!doOnce)
		{
			if(other.tag == "Sword")
			{
				PickUp();
			}
			if(other.tag == "Player")
			{
				PickUp();
			}
		}


	}



	void PickUp()
	{
		doOnce = true;

		if(Key)
		{
			Main.DataHolder.LinkKeys++;
			Instantiate(SFX_Sword);

		}
		else if(Heart)
		{
			Main.DataHolder.LinkHP+=2;
			if(Main.DataHolder.LinkHP > Main.DataHolder.LinkMaxHP)
			{
				Main.DataHolder.LinkHP = Main.DataHolder.LinkMaxHP;
			}
			Instantiate(SFX_Sword);

		}
		else if(Sword)
		{
			Main.DataHolder.LinkHasSword = true;
			Main.DataHolder.Link.HoldingItem = true;
			Instantiate(Main.DataHolder.Poof,Main.DataHolder.ZeldaOldMan.transform.position,transform.rotation);
			Destroy(Main.DataHolder.ZeldaOldMan);
			Instantiate(SFX_Sword);
		}
		else if(Triforce)
		{
			Main.DataHolder.Link.FreezeWin = true;
			Main.DataHolder.ZeldaComplete = true;
		}
		else
		{
			Instantiate(SFX_Sword);

		}
		Destroy(gameObject);
	}


}
