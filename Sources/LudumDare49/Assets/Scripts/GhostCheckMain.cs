using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCheckMain : MonoBehaviour {

	public bool FinalPassTop;
	public bool FinalPassMid;
	public bool FinalPassBot;

	public bool MasterPass;

	public Building Build;
	public Transform CurrentScaffold;
	public bool RequiresScaffoldID;
	public GhostCheck[] Checkem;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		int i = 0;
		while(i < Checkem.Length)
		{
			Checkem[i].PassCheck();
			i++;
		}

		MasterPass = FinalPassTop && FinalPassMid && FinalPassBot;
		Build.BuildConfirm();
	}
}
