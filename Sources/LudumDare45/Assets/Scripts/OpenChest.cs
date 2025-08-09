using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour {


	public bool Active;

	public float Timer;
	public bool[] Step;

	public PlayerController P;

	public RuntimeAnimatorController Chest;
	public Animator anim;


	void Update () {

		if(Active)
		{
			Timer += Time.deltaTime;
			if(Step[0])
			{
				P.CutsceneNoMove = true;
				P.Main.LockCamera = true;
				P.anim.runtimeAnimatorController = P.Shove;
				if(Timer > 0.25f)
				{
					Timer = 0;
					Step[0] = false;
					Step[1] = true;
					P.anim.runtimeAnimatorController = P.Jump;
				}
			}

			if(Step[1])
			{
				if(Timer > 2f)
				{
					Timer = 0;
					Step[1] = false;
					Step[2] = true;
					P.anim.runtimeAnimatorController = P.Idle;
					DataHolder Main = GameObject.Find("Main").GetComponent<DataHolder>();
					Main.Aud.clip = Main.Clear;
					Main.Aud.enabled = false; Main.Aud.enabled = true;
				}
			}


			if(Step[2])
			{

				if(Timer > 6f)
				{
					Timer = 0;
					Step[2] = false;
					P.Main.WinScreen.SetActive(true);
				}
			}


		}

	}
}
