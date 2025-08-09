using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour {


	public bool InFarm;
	public bool	InCombat;

	public bool EnterFarm;
	public bool ExitFarm;

	public CamControl Cam;

	public float Progress;

	public int SeedStock;

	public EnterCombat EC;

	public bool NoMoving;

	public bool UnlockedRed;
	public bool UnlockedYellow;
	public bool UnlockedGreen;
	public bool UnlockedPurple;
	public bool UnlockedWhite;

	public bool UnlockedPink;

	public WriteLine WL;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(EnterFarm)
		{
			EnterFarm = false;
			InFarm = true;
			Cam.YTimer = 0;
		}

		if(ExitFarm)
		{
			ExitFarm = false;
			InFarm = false;
			Cam.YTimer = 0;
		}

	}
}
