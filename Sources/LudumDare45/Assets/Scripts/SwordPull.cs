using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPull : MonoBehaviour {


	public bool Active;

	public bool[] Step;

	public float Timer;

	public PlayerController P;

	public Sprite PSword_1;
	public Sprite PSword_2;

	public Vector3 PPos;

	public SpriteRenderer Bubble;
	public Sprite Ohwow;
	public Sprite PlanB;

	public RuntimeAnimatorController SwordJump;
	
	// Update is called once per frame
	void Update () {

		if(Active)
		{
			Timer+=Time.deltaTime;
			if(Step[0])
			{
				P.CutsceneNoMove = true;
				Step[1] = true;
				Step[0] = false;
				P.anim.runtimeAnimatorController = null;
				P.sr.sprite = PSword_1;
				Bubble.enabled = false;
			}

			if(Step[1])
			{
				if(Timer > 2)
				{
					P.sr.sprite = PSword_2;
					Timer =0;
					Step[2] = true;
					Step[1] = false;
					PPos = P.gameObject.transform.position;
				}
			}

			if(Step[2])
			{
				if(Timer > 1)
				{
					Timer = 0;
					Step[3] = true;
					Step[2] = false;
				}
			}

			if(Step[3])
			{
				P.transform.position = new Vector3(Random.Range(-0.2f,0.2f),Random.Range(-0.2f,0.2f),0) + PPos;

				if(Timer > 1)
				{
					Timer = 0;
					Step[4] = true;
					Step[3] = false;
					P.transform.position = PPos;
					P.anim.runtimeAnimatorController = P.Idle;
				}
			}

			if(Step[4])
			{
				if(Timer > 0.5f)
				{
					Timer = 0;
					Step[5] = true;
					Step[4] = false;
					Bubble.enabled = true;
					Bubble.sprite = Ohwow;
				}
			}

			if(Step[5])
			{
				if(Timer > 2f)
				{
					Timer = 0;
					Step[6] = true;
					Step[5] = false;
					Bubble.sprite = null;
				}
			}

			if(Step[6])
			{
				if(Timer > 0.5f)
				{
					Timer = 0;
					Step[7] = true;
					Step[6] = false;
					Bubble.sprite = PlanB;
				}
			}

			if(Step[7])
			{
				if(Timer > 1f)
				{
					Timer = 0;
					Step[8] = true;
					Step[7] = false;
					P.anim.runtimeAnimatorController = null;
					P.sr.sprite = PSword_2;
				}
			}

			if(Step[8])
			{
				P.transform.position = new Vector3(Random.Range(-0.2f,0.2f),Random.Range(-0.2f,0.2f),0) + PPos;
				if(Timer > 0.5f)
				{
					Timer = 0;
					Step[9] = true;
					Step[8] = false;
				}
			}

			if(Step[9])
			{
				P.transform.position = new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),0) + PPos;
				if(Timer > 0.5f)
				{
					Timer = 0;
					Step[10] = true;
					Step[9] = false;
				}
			}

			if(Step[10])
			{
				P.transform.position = new Vector3(Random.Range(-2.5f,2.5f),Random.Range(-2.5f,2.5f),0) + PPos;
				if(Timer > 0.5f)
				{
					Timer = 0;
					Step[11] = true;
					Step[10] = false;
					P.transform.position = PPos;
					P.anim.runtimeAnimatorController = P.Jump;
					GetComponent<Animator>().runtimeAnimatorController = SwordJump;
					Bubble.enabled = false;
				}
			}


			if(Step[11])
			{
				if(Timer > 3.5f)
				{
					Timer = 0;
					Step[12] = true;
					Step[11] = false;
					P.anim.runtimeAnimatorController = P.Idle;
				}
			}

			if(Step[12])
			{
				if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space))
				{
					GameObject.Find("Main").GetComponent<DataHolder>().UnlockedSword = true;
					P.CutsceneNoMove = false;
					Destroy(gameObject);
				}


			}

		}
	}

	void FixedUpdate () {
		if(!Active)
		{
			Bubble.enabled = false;
		}
	}

	void OnTriggerStay(Collider other)
	{

		if(other.name == "Player")
		{
			if(!Active)
			{
				Bubble.enabled = true;
			}
		}

	}

}
