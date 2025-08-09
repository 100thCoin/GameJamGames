using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour {

	public bool HasScissors;
	public bool HasKnife;
	public GameObject CantSliceSelf;
	public GameObject SliceHold;

	public Char_Movement Char;
	public LoopMain Loop;
	public Camera_Movement Cam;
	public QuadStrip Lvl2Quad;
	public QuadStrip Lvl2OtherQuad;

	public bool DoorsOpen;

	public bool OrcDead;
	public RuntimeAnimatorController OrcKnife;
	public RuntimeAnimatorController OrcDeadAnim;
	public float Length;
	public float ScaleTimer;
	public bool OnScale;

	public bool SendTip1;
	public bool HaltTip1;
	public bool SendTip2;
	public bool HaltTip2;
	public bool SendTip3;
	public bool HaltTip3;

	public bool InsideCutscene;

	public TextMesh ItemName;

	public bool Win;
	public bool AnyKey;

	public bool Calm;
	public bool Move;
	public bool Forest;

	public bool Mute;


	void Update()
	{
		Length = Loop.TheLoop.Length * 0.1f;

		if(OnScale)
		{
			ScaleTimer += Time.deltaTime*3;
		}
		else
		{
			ScaleTimer -= Time.deltaTime;
		}

		if(ScaleTimer < 0)
		{
			ScaleTimer = 0;
		}
		if(ScaleTimer > 1)
		{
			ScaleTimer = 1;
		}

		if(AnyKey)

		{

			if(Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}

		}


	}



}
