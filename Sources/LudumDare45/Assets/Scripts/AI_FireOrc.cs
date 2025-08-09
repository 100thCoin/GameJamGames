using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_FireOrc : MonoBehaviour {

	public  RuntimeAnimatorController Shove;
	public RuntimeAnimatorController Flee;
	public SpriteRenderer Fire;
	public GameObject FireParts;
	public Animator anim;

	public bool Active;
	public float Timer;
	public bool[] Step;

	public float SPos;
	public float Ypos;
	public float DotPos;

	public GameObject FirepitSparks;
	public bool FirePitOnce;

	public GameObject Cam;

	public GameObject Kaboom;

	public GameObject Col;
	public PlayerController P;

	public GameObject Itemm;
	public GameObject ItemGet;

	public GameObject PostOrcFire;

	void Start () {
		SPos = transform.position.x;
		Ypos = transform.position.y;
	}
	
	void FixedUpdate () {

		if(Active)
		{
			Timer += Time.fixedDeltaTime;
			if(Step[0])
			{
				anim.runtimeAnimatorController = Shove;
				Step[1] = true;
				Step[0] = false;
				GameObject.Find("Main").GetComponent<DataHolder>().LockCamera = true;
				Cam = GameObject.Find("Main Camera");
				Timer = 0;
				Col.transform.position = new Vector3(0,0,-500);
				P.CutsceneNoMove = true;
			}
			if(Step[1])
			{
				float durationDOT = 0.25f;
				float prevscaleDOT = 0f;
				float targestscaleDOT = 1.5f;
				DotPos = (((prevscaleDOT - targestscaleDOT) * Mathf.Pow(Timer,2))/Mathf.Pow(durationDOT,2) - ((2*prevscaleDOT - 2*targestscaleDOT) * Timer)/durationDOT + prevscaleDOT);

				if(!FirePitOnce && Timer > 0.05f)
				{
					FirePitOnce = true;
					Instantiate(FirepitSparks,transform.position + new Vector3(0,-1,0),FirepitSparks.transform.rotation,transform.parent);

				}

				transform.position = new Vector3(SPos+DotPos,Ypos,transform.position.z);
				if(Timer > durationDOT)
				{
					transform.position = new Vector3(SPos+targestscaleDOT,transform.position.y,transform.position.z);
					Timer = 0;
					Step[1] = false;
					Step[2] = true;
				}
			}
			if(Step[2])
			{
				if(Timer > 1)
				{
					Step[3] = true;
					Step[2] = false;
					anim.runtimeAnimatorController = Flee;
					GetComponent<SpriteRenderer>().flipX = true;
					Fire.enabled = true;
					FireParts.SetActive(true);
				}
			}

			if(Step[3])
			{

				transform.position = transform.position - new Vector3(0.25f,0,0);

				if(transform.position.x < Cam.transform.position.x - 16)
				{
					Instantiate(Kaboom,gameObject.transform.position - new Vector3(0,1,0),Kaboom.transform.rotation,transform.parent);
					P.CutsceneNoMove = false;

					Instantiate(Itemm,new Vector3(SPos+6,Ypos+2,0),transform.rotation,transform.parent);
					Instantiate(ItemGet,new Vector3(SPos+6,Ypos+2,0),transform.rotation,transform.parent);
					PostOrcFire.SetActive(true);
					GameObject.Find("Main").GetComponent<DataHolder>().FireBG = true;
					Destroy(gameObject);
				}

			}


		}
	}
}
