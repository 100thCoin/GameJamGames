using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour {

	public GameObject Player;
	public DataHolder Main;

	public GameObject Target;


	public float LocalXPos;
	public float LastXpos;
	public float TargetXpos;
	public float XDelayTimer;
	public float XDelay;
	public float XTimer;
	public float XDuration;
	public float LookDir;
	public float LastDir;

	public float YBonus;


	public float LocalYPos;
	public float LastYpos;
	public float TargetYpos;
	public float YTimer;
	public float YDuration;

	public SpriteRenderer FadeBlack;

	// Use this for initialization
	void Start () {
		Main = GameObject.Find("Main").GetComponent<DataHolder>();
		Main.Cam = this;
		Target = Player;

	}

	// Update is called once per frame
	void FixedUpdate () {



		if(!Main.WL.InDialogue)
		{

			if(!Main.InCombat)
			{
				if(Input.GetKey(KeyCode.A))
				{
					LookDir = -1;
				}
				if(Input.GetKey(KeyCode.D))
				{
					LookDir =1;
				}

				if(LastDir != LookDir)
				{
					LastXpos = LocalXPos;
					XTimer =0;
					XDelayTimer = 0;
				}

				LastDir = LookDir;
				TargetXpos = LookDir*0.5f;

				if(LocalXPos != TargetXpos)
				{
					XDelayTimer += Time.fixedDeltaTime;
					if(XDelayTimer >= XDelay)
					{
						XTimer += Time.fixedDeltaTime;

						LocalXPos = (((LastXpos - TargetXpos) * Mathf.Pow(XTimer,2))/Mathf.Pow(XDuration,2) - ((2*LastXpos - 2*TargetXpos) * XTimer)/XDuration + LastXpos);
						//	Y = (((a - h) * Mathf.Pow(timer,2))/Mathf.Pow(dur,2) - ((a - h) * timer)/dur + a);
						if(XTimer >= XDuration)
						{
							LocalXPos = TargetXpos;
						}
					}
				}
				else
				{
					XTimer = 0;
					XDelayTimer = 0;
				}


				if(Main.InFarm)
				{
					if(YTimer < YDuration)
					{
						YTimer += Time.fixedDeltaTime;
						LastYpos = 1.5f;
						TargetYpos = -3;
						if(YTimer >= YDuration)
						{
							YTimer = YDuration;
						}
						LocalYPos = (((LastYpos - TargetYpos) * Mathf.Pow(YTimer,2))/Mathf.Pow(YDuration,2) - ((2*LastYpos - 2*TargetYpos) * YTimer)/YDuration + LastYpos);
					}
				}
				else
				{
					if(YTimer < YDuration)
					{
						YTimer += Time.fixedDeltaTime;
						LastYpos = -3;
						TargetYpos = 1.5f;
						if(YTimer >= YDuration)
						{
							YTimer = YDuration;
						}
						LocalYPos = (((LastYpos - TargetYpos) * Mathf.Pow(YTimer,2))/Mathf.Pow(YDuration,2) - ((2*LastYpos - 2*TargetYpos) * YTimer)/YDuration + LastYpos);
					}
				}



				transform.position = new Vector3(Target.transform.position.x + LocalXPos,LocalYPos,-50);
				if(transform.position.x < 0)
				{
					transform.position = new Vector3(0, LocalYPos,-50);

				}

			}

		}

	}
}