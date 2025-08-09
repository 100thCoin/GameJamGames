using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Fire : MonoBehaviour {

	public GameObject Fireballs;
	public float Timer;
	public int count;
	public float Delay;
	public bool STOP;

	public float ShakeDelay;
	public float ShakeTimer;

	public bool LeaveTheatre;
	public float leaveTimer;
	public GameObject PlayerShadow;
	public GameObject Pillars;
	bool donewiththatstep;

	public Environment Env;
	public GameObject Necromancer;
	public GameObject HandHolder;

	public CharMovement Charr;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(!STOP)
		{
			Timer-= Time.deltaTime;

			if(Timer < 0)
			{
				Timer += Delay;
				Vector2 RandPos = new Vector2(Random.Range(-11f,11f),Random.Range(-1.5f,-6.5f));
				GameObject Cfire = Instantiate(Fireballs,RandPos,Fireballs.transform.rotation,transform);
				Cfire.transform.GetChild(0).GetComponent<FireballMove>().Env = Env;
				count++;
				Delay -= 0.02f;

				if(count > 80)
				{
					STOP = true;
				}

			}
		}
		else if(!donewiththatstep)
		{
			ShakeDelay -= Time.deltaTime;
			if(ShakeDelay <= 0)
			{
				ShakeTimer -= Time.deltaTime;
				if(ShakeTimer >0)
				{	
					float Offset = Random.Range(0.2f,-0.2f);
					Env.Stage.transform.position = new Vector3(0,Offset,0);
					PlayerShadow.transform.localPosition = new Vector3(0,Offset-1,0);
				}
				else
				{
					float Offset = Random.Range(0.2f,-0.2f);
					Env.Stage.transform.position = new Vector3(0,Offset,0);
					PlayerShadow.transform.localPosition = new Vector3(0,Offset-1,0);
					LeaveTheatre = true;
					// change player animation, do dust?

					donewiththatstep = true;
					Charr.Anim.runtimeAnimatorController = Charr.Fly;
					Charr.Winged = true;
				}
			}
		}

		if(LeaveTheatre)
		{
			leaveTimer-=Time.deltaTime*1.065f;

			float Offset = Random.Range(0.2f,-0.2f);
			Env.Stage.transform.position = new Vector3(0,Offset+leaveTimer,0);
			PlayerShadow.transform.localPosition = new Vector3(0,Offset-1+leaveTimer,0);
			Pillars.transform.localScale = Vector3.one * (1-leaveTimer*0.1f);
			Env.CurtainSpread = -leaveTimer;
			Necromancer.transform.localPosition = new Vector3(0,-32-leaveTimer*2,0);
			HandHolder.transform.localPosition = new Vector3(0,12+leaveTimer,0);

			if(leaveTimer <-15)
			{
				LeaveTheatre = false;
				Env.HandsReady = true;
				if(!Charr.DoneTooltip2)
				{
					Charr.Tool2.SetActive(true);
				}

			}

		}

	}
}
