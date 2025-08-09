using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		Target = Player;

	}

	// Update is called once per frame
	void FixedUpdate () {





		if(!Main.Dead && !Main.LockCamera && !Main.OnMap)
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
			TargetXpos = LookDir*1;

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
			transform.position = new Vector3(Target.transform.position.x + LocalXPos,YBonus,-10);
			if(transform.position.x < 0)
			{
				transform.position = new Vector3(0, YBonus,-10);

			}

		}



	}
}