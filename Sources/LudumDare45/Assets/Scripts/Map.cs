using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

	public GameObject Player;
	public GameObject[] LevelPos;

	public int StandingAt;

	public Sprite Rubble;
	public GameObject Fire1;
	public GameObject Fire2;

	public float Timer;

	public bool MovePlayer;
	public float MoveTimer;

	public GameObject LastLoc;
	public GameObject NextLoc;

	public bool ReadyToEnter;

	public GameObject ReadyText;

	public bool Entering;

	public LevelTransitiounUISwooshing Swoosh;

	void Update () {

		Timer +=Time.deltaTime;
	
		if(Timer > 2 && Timer < 800)
		{
			MovePlayer = true;
			Timer = 1000;
		}


		if(MovePlayer)
		{
			MoveTimer+= Time.deltaTime;

			float LastXpos = LastLoc.transform.position.x;
			float LastYpos = LastLoc.transform.position.y;

			float TargetXpos = NextLoc.transform.position.x;
			float TargetYpos = NextLoc.transform.position.y;

			float XDuration = 1;

			float LocalXPos = (((LastXpos - TargetXpos) * Mathf.Pow(MoveTimer,2))/Mathf.Pow(XDuration,2) - ((2*LastXpos - 2*TargetXpos) * MoveTimer)/XDuration + LastXpos);
			float LocalYPos = (((LastYpos - TargetYpos) * Mathf.Pow(MoveTimer,2))/Mathf.Pow(XDuration,2) - ((2*LastYpos - 2*TargetYpos) * MoveTimer)/XDuration + LastYpos);

			Player.transform.position = new Vector3(LocalXPos,LocalYPos,0);

			if(MoveTimer > XDuration)
			{
				ReadyToEnter = true;
				MovePlayer = false;

			}


		}

		if(ReadyToEnter)
		{
			ReadyText.SetActive(true);

			if(Input.GetKeyDown(KeyCode.F) && !Entering)
			{
				Entering = true;

				Swoosh.Timer = 0;
				Swoosh.PrePreLevelEnter = true;
				Swoosh.Step[0] = true;
				Swoosh.transform.position = NextLoc.transform.position;
			}

		}
		else
		{
			ReadyText.SetActive(false);
		}
			


	}

	public void ReadyMap(int Level)
	{
		NextLoc = LevelPos[Level+1];
		LastLoc = LevelPos[Level];
		Timer = 0;
		MoveTimer = 0;
		Player.transform.position = LastLoc.transform.position;
		MovePlayer = false;
		ReadyToEnter = false;
		Entering = false;
	}

	void Start()
	{
		print("REMOVE THISSSSSSSSSSSSSSSSSSSSSSSSSSS");
		ReadyMap(0);

	}

}
