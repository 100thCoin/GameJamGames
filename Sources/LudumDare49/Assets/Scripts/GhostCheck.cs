using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCheck : MonoBehaviour {

	public bool YesScaffoldTop;
	public bool YesScaffoldMid;
	public bool YesBaseplate;
	public bool YesGround;
	public bool YesLadderBot;
	public bool NoBaseplate;
	public bool NoScaffoldTop;
	public bool NoScaffoldMid;
	public bool NoGround;

	public bool AlwaysPass;

	public bool Fail;
	public bool Pass;
	public bool FinalPass; // !fail && Pass

	public bool RequiredScaffoldID;
	public Transform CurrentScaffold;

	public bool Top;
	public bool Mid;
	public bool Bot;

	public GhostCheckMain Main;

	public void PassCheck()
	{

		FinalPass = Pass && !Fail;

		if(Top)
		{
			Main.FinalPassTop = FinalPass;
		}
		else if(Mid)
		{
			Main.FinalPassMid = FinalPass;
		}
		else if(Bot)
		{
			Main.FinalPassBot = FinalPass;
		}
		Pass = AlwaysPass;
		Fail = false;
		if(RequiredScaffoldID)
		{
			Main.CurrentScaffold = CurrentScaffold;
		}
		CurrentScaffold = null;
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "GroundZero")
		{
			if(YesBaseplate)
			{
				Pass = true;
			}
			if(NoBaseplate)
			{
				Fail = true;
			}
		}
		if(other.tag == "Ground")
		{
			if(YesGround)
			{
				Pass = true;
			}
			if(NoGround)
			{
				Fail = true;
			}
		}
		if(other.tag == "Scaffold")
		{
			if(YesScaffoldMid)
			{
				Pass = true;
			}
			if(NoScaffoldMid)
			{
				Fail = true;
			}
		}
		if(other.tag == "Scaffoldtop")
		{
			if(YesScaffoldTop)
			{
				Pass = true;
				if(RequiredScaffoldID)
				{
					CurrentScaffold = other.transform;
				}

			}
			if(NoScaffoldTop)
			{
				Fail = true;
			}
		}
		if(other.tag == "BottomLadder")
		{
			if(YesLadderBot)
			{
				Pass = true;
			}
		}
	}

}
