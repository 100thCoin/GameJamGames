using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gando : MonoBehaviour {

	public Transform[] Teleposes;
	public float Timer;
	public float Attacktimer;

	public GameObject[] Attacks;
	float rot;
	public float radius;
	public SpriteRenderer SR;
	public int Teletimer;
	public Collider Col;
	public GameObject Hurtbox;
	public int funnytimer;

	public Sprite[] FunnyAnims;
	// Use this for initialization
	void Start () {
		
	}
	
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
				Triforce.SetActive(true);

				Destroy(gameObject);
			}


			return;
		}

		Timer += Time.fixedDeltaTime;
		if(Timer < 3)
		{

			IFrames--;
			if(IFrames >0)
			{
				SR.enabled = (IFrames/4)%2 == 1;
			}
			else
			{
				SR.enabled = true;
			}
		}
		else 
		if(Timer > 3 && radius >=0)
		{

			IFrames--;
			if(IFrames >0)
			{
				SR.enabled = (IFrames/4)%2 == 1;
			}
			else
			{
				SR.enabled = true;
			}
			funnytimer++;
			if(funnytimer > 4)
			{
				funnytimer =0;
				SR.sprite = FunnyAnims[Random.Range(0,FunnyAnims.Length)];
			}


			Attacktimer += Time.fixedDeltaTime;

			radius = Mathf.Sin(Attacktimer * Attacktimer * 0.2f)*5;

			int i = 0;
			while(i < Attacks.Length)
			{
				rot = (float)i/(float)Attacks.Length * Mathf.PI*2;

				Attacks[i].transform.localPosition = new Vector3(Mathf.Cos(rot+Attacktimer*2)*radius,Mathf.Sin(rot+Attacktimer*2)*radius,0);
				Attacks[i].SetActive(true);


				i++;
			}

			if(radius <0)
			{
				// prep teleport;
				i = 0;
				Teletimer =0;

				while(i < Attacks.Length)
				{
					
					Attacks[i].SetActive(false);
					SR.sprite = FunnyAnims[0];

					i++;
				}
			}


		}
		else if(Timer>3)
		{
			Teletimer++;

			if(Teletimer < 16)
			{
				SR.enabled = (Teletimer % 4) != 1;
			}
			if(Teletimer >16 && Teletimer < 32)
			{
				SR.enabled = (Teletimer % 2) != 1;
			}
			if(Teletimer >32 && Teletimer < 48)
			{
				SR.enabled = (Teletimer % 4) == 1;
			}
			if(Teletimer >48 && Teletimer < 64)
			{
				SR.enabled = false;
				Col.enabled = false;
				Hurtbox.SetActive(false);
			}
			if(Teletimer == 64)
			{
				int r = Random.Range(0,Teleposes.Length);
				transform.position = Teleposes[r].transform.position;
			}
			if(Teletimer >64 && Teletimer < 80)
			{
				SR.enabled = (Teletimer % 4) == 1;

			}
			if(Teletimer >80 && Teletimer < 96)
			{
				SR.enabled = (Teletimer % 2) != 1;

			}
			if(Teletimer >96 && Teletimer < 112)
			{
				SR.enabled = (Teletimer % 4) != 1;

			}
			if(Teletimer >112 && Teletimer < 128)
			{
				SR.enabled = true;

			}
			if(Teletimer > 128)
			{
				Col.enabled = true;
				Hurtbox.SetActive(true);
				Timer=2;
				Attacktimer=0;
				radius = 0.1f;
			}

		}

	}



	int IFrames;
	public int HP = 12;
	public Animator Anim;
	public RuntimeAnimatorController Explode;
	public GameObject Triforce;
	public bool Dead;
	public int DeadTimer;

	void OnTriggerStay(Collider other)
	{

		if(other.tag == "Sword" && IFrames<=0)
		{
			IFrames = 16;
			Hurt();
		}
	}

	public GameObject SFX_SwordHit;
	void Hurt()
	{
		Instantiate(SFX_SwordHit);

		HP--;
		if(HP <=0)
		{
			Anim.runtimeAnimatorController = Explode;
			Dead = true;
			Col.enabled = false;
			Main.DataHolder.LinkKills++;
			Main.DataHolder.ZeldaCurrentRoomKilledEnemies++;
		}

	}




}
