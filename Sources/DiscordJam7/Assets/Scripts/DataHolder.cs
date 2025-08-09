using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main{

	public static DataHolder DataHolder;

}


public class DataHolder : MonoBehaviour {

	public GameObject Title;

	public MarioMover Mario;
	public MarioCam MarioCamera;
	public bool PauseMario; // timestop, not pause button
	public int MPauseFrames;
	public bool HoldPauseMario;
	public bool MarioReturnToMap;
	public bool MarioLevelClear;
	public float MarioReturnToMapTimer;
	public SpriteRenderer MarioFadeToBlack;
	public int MarioCombo;
	public GameObject[] Scorebubbles;
	public bool MarioClearWalkRight;
	public bool MarioLevel1Clear;
	public bool MarioLevel2Clear;
	public bool MarioOnMap;
	public GameObject[] MarioLevels;
	public bool MarioEnteringLevel;
	public float MarioEnterLevelTimer;
	public int MarioLevelID;
	public GameObject MarioMap;
	public GameObject MarioLoadedLevel;
	public Transform Mario3Holder;
	public bool MarioComplete;

	public bool LinkHasSword;
	public int LinkHP;
	public int LinkMaxHP;
	public LinkMover Link;
	public int LinkKeys;
	public int LinkKills;
	public GameObject[] LinkDrops;
	public ZeldaCam ZeldaCamera;
	public Transform ZeldaEnemyHolder;
	public GameObject[] Enemyspawners;
	public int[] ZeldaRoomRemainingenemies;
	public int ZeldaCurrentRoomKilledEnemies;
	public int CurrentRoomID;
	public Transform ZeldaHolder;
	public GameObject ZeldaOldMan;
	public GameObject Poof;
	public SpriteRenderer[] ZeldaHearts;
	public Sprite Inivisble;
	public Sprite HeartFull;
	public Sprite HEartHalf;
	public bool LinkDied;
	public float LinkDiedTimer;
	public SpriteRenderer HasKeyy;
	public SpriteRenderer LinkCover1;
	public SpriteRenderer LinkCover2;
	public bool ZeldaComplete;



	public float MarioWinWhiteTimer;
	public float ZeldaWinWhiteTimer;
	public MeshRenderer MarioScreen;
	public MeshRenderer ZeldaScreen;
	public Material MarioBW;
	public Material ZeldaBW;
	public SpriteRenderer MarioWhite;
	public SpriteRenderer ZeldaWhite;

	public TextMesh ThreatTimer;
	public float TimeRemainingUntilGameOver=60;

	public bool GameOver;
	public float GameOverTimer;

	public GameObject GameOverScreen;

	public GameObject TimeRuningOutMusic;
	public GameObject RegularMusic;

	public GameObject InGameFullHolder;

	public GameObject VictoryScreen;
	public TextMesh VictoryMesh;
	public float MarioTimer;
	public float Zeldatimer;
	public bool DoOnce;
	// Use this for initialization
	void OnEnable () {
		Main.DataHolder = this;	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(!MarioComplete)
		{
			MarioTimer+= Time.fixedDeltaTime;
		}
		if(!ZeldaComplete)
		{
			Zeldatimer += Time.fixedDeltaTime;
		}
		if(MarioComplete)
		{
			MarioScreen.material = MarioBW;
			MarioWinWhiteTimer+=Time.fixedDeltaTime;
			MarioWhite.color = new Vector4(1,1,1,1.1f-MarioWinWhiteTimer*2);
			MarioCamera.GetComponent<Camera>().enabled = false;
		}
		if(ZeldaComplete)
		{
			ZeldaScreen.material = ZeldaBW;
			ZeldaWinWhiteTimer+=Time.fixedDeltaTime;
			ZeldaWhite.color = new Vector4(1,1,1,1.1f-ZeldaWinWhiteTimer*2);
			ZeldaCamera.GetComponent<Camera>().enabled = false;
		}

		if((MarioComplete || ZeldaComplete) && TimeRemainingUntilGameOver < 70)
		{
			if(RegularMusic != null)
			{
				RegularMusic.SetActive(false);
			}
			TimeRuningOutMusic.SetActive(true);
			TimeRemainingUntilGameOver-=Time.fixedDeltaTime;
			ThreatTimer.gameObject.SetActive(true);
			string threat = "Time remaining to complete other game:\n\n0:";
			string timeeee = "" + Mathf.Round(TimeRemainingUntilGameOver*100f)/100f;
			if(TimeRemainingUntilGameOver <10)
			{
				timeeee = "0" + timeeee;
			}
			if(timeeee.Length==2)
			{timeeee+=".";

			}
			while(timeeee.Length < 5)
			{
				timeeee+="0";
			}
			threat+=timeeee;
			ThreatTimer.text = threat;
			if(TimeRemainingUntilGameOver <= 0)
			{
				TimeRemainingUntilGameOver = 0;
				GameOver = true;
				PauseMario = true;
				LinkDied = true;
				ThreatTimer.gameObject.SetActive(false);
				ZeldaCamera.GetComponent<Camera>().enabled = false;
				MarioCamera.GetComponent<Camera>().enabled = false;

			}

		}

		if(GameOver)
		{
			GameOverTimer+= Time.deltaTime;
			if(GameOverTimer >2)
			{
				GameOverScreen.SetActive(true);


				if(Input.GetKey(KeyCode.R))
				{
					if(Title != null)
					{
						Title.SetActive(true);
					}
					Destroy(InGameFullHolder.gameObject);

				}
			}
			return;
		}

		if(MarioComplete && ZeldaComplete && TimeRemainingUntilGameOver < 60)
		{
			TimeRemainingUntilGameOver = 99999;
			ThreatTimer.gameObject.SetActive(false);
		}


		if(MarioWinWhiteTimer > 2 && ZeldaWinWhiteTimer > 2)
		{

			//cUT AWAY TO VICTORY SCREEN.	
			VictoryScreen.SetActive(true);
			if(!DoOnce)
			{
				string Victory = "YOU WIN!\n\nSTATS:\n\nLeft Game Time:";
				float minutes =0;
				float MarioBackup = MarioTimer;
				float ZeldaBackup = Zeldatimer;
				while(MarioTimer >= 60)
				{
					MarioTimer-=60;
					minutes++;
				}
				float seconds = Mathf.Round(MarioTimer*100f)/100f;
				Victory += " " + minutes + ":" + seconds +"\n\nRight Game Time:";
				minutes =0;
				while(Zeldatimer >= 60)
				{
					Zeldatimer-=60;
					minutes++;
				}
				seconds = Mathf.Round(Zeldatimer*100f)/100f;
				Victory += " " + minutes + ":" + seconds +"\n\nDifference:";
				float diff = Mathf.Abs(ZeldaBackup-MarioBackup);
				Victory+= " " + diff+"\n\n\nPress Escape to quit.";
				VictoryMesh.text = Victory;
			}
			if(Input.GetKey(KeyCode.Escape))
			{
				Application.Quit();
			}

		}


		if(LinkKeys >0)
		{
			HasKeyy.enabled = true;
		}
		else
		{
			HasKeyy.enabled = false;
		}
		if(LinkHP == 6)
		{
			ZeldaHearts[0].sprite = HeartFull;
			ZeldaHearts[1].sprite = HeartFull;
			ZeldaHearts[2].sprite = HeartFull;
		}
		else if(LinkHP == 5)
		{
			ZeldaHearts[0].sprite = HEartHalf;
			ZeldaHearts[1].sprite = HeartFull;
			ZeldaHearts[2].sprite = HeartFull;
		}
		else if(LinkHP == 4)
		{
			ZeldaHearts[0].sprite = Inivisble;
			ZeldaHearts[1].sprite = HeartFull;
			ZeldaHearts[2].sprite = HeartFull;
		}
		else if(LinkHP == 3)
		{
			ZeldaHearts[0].sprite = Inivisble;
			ZeldaHearts[1].sprite = HEartHalf;
			ZeldaHearts[2].sprite = HeartFull;
		}
		else if(LinkHP == 2)
		{
			ZeldaHearts[0].sprite = Inivisble;
			ZeldaHearts[1].sprite = Inivisble;
			ZeldaHearts[2].sprite = HeartFull;
		}
		else if(LinkHP == 1)
		{
			ZeldaHearts[0].sprite = Inivisble;
			ZeldaHearts[1].sprite = Inivisble;
			ZeldaHearts[2].sprite = HEartHalf;
		}
		else if(LinkHP <= 0)
		{
			ZeldaHearts[0].sprite = Inivisble;
			ZeldaHearts[1].sprite = Inivisble;
			ZeldaHearts[2].sprite = Inivisble;
			LinkDied = true;
		}

		if(LinkDied)
		{
			LinkDiedTimer+=Time.fixedDeltaTime;
			if(LinkDiedTimer < 1)
			{
				float Y = Mathf.Lerp(27,16,LinkDiedTimer);
				float Y2 = Mathf.Lerp(-27,-16,LinkDiedTimer);
				LinkCover1.transform.localPosition = new Vector3(0,Y,10);
				LinkCover2.transform.localPosition = new Vector3(0,Y2,10);

			}
			if(LinkDiedTimer >=1 && LinkDiedTimer <= 1.1f)
			{
				LinkCover1.transform.localPosition = new Vector3(0,16,10);
				LinkCover2.transform.localPosition = new Vector3(0,-16,10);
				ZeldaClearEnemyLeaveRoom();
				ZeldaRoomRemainingenemies = new int[]{
					0,0,4,0,0,3,5,6,3,6,0,0,0,0,0,0,0
				};

				if(CurrentRoomID >= 4)
				{
					CurrentRoomID = 4;
					ZeldaCamera.transform.localPosition = new Vector3(64,22,-50);
				}
				else
				{
					CurrentRoomID =0;
					ZeldaCamera.transform.localPosition = new Vector3(0,0,-50);

				}
				LinkHP = 6;

			}
			if(LinkDiedTimer > 1.1f && LinkDiedTimer <2.1f)
			{
				float Y = Mathf.Lerp(16,27,LinkDiedTimer-1.1f);
				float Y2 = Mathf.Lerp(-16,-27,LinkDiedTimer-1.1f);
				LinkCover1.transform.localPosition = new Vector3(0,Y,10);
				LinkCover2.transform.localPosition = new Vector3(0,Y2,10);
			}
			if(LinkDiedTimer >= 2.1f)
			{
				LinkDiedTimer = 0;
				LinkCover1.transform.localPosition = new Vector3(0,27,10);
				LinkCover2.transform.localPosition = new Vector3(0,-27,10);
				LinkDied = false;
				Link.transform.localPosition = (CurrentRoomID==0) ? new Vector3(0,-5,0) : new Vector3(64,20,0);
				Link.Anim.runtimeAnimatorController = Link.Idle;
			}
		}





		if(MPauseFrames >0)
		{
			PauseMario = true;
			MPauseFrames--;
		}
		else
		{
			PauseMario = false;
		}
		if(HoldPauseMario)
		{
			PauseMario = true;
		}


		if(MarioReturnToMap)
		{
			MarioReturnToMapTimer++;
			MarioReturnToMapTimer++;

			if(MarioReturnToMapTimer <60)
			{
				MarioFadeToBlack.color = new Vector4(0,0,0,MarioReturnToMapTimer/60f);
			}
			else if(MarioReturnToMapTimer == 60)
			{
				MarioMap.SetActive(true);
				Destroy(MarioLoadedLevel);
				Main.DataHolder.MarioOnMap = true;

			}
			else if(MarioReturnToMapTimer >60)
			{
				MarioFadeToBlack.color = new Vector4(0,0,0,1-((MarioReturnToMapTimer-60)/60f));
			}
			if(MarioReturnToMapTimer >120)
			{
				MarioReturnToMap = false;
				MarioClearWalkRight = false;
			}
		}

		if(MarioEnteringLevel)
		{
			MarioEnterLevelTimer++;
			MarioEnterLevelTimer++;
			HoldPauseMario = true;
			if(MarioEnterLevelTimer <60)
			{
				MarioFadeToBlack.color = new Vector4(0,0,0,MarioEnterLevelTimer/60f);
			}
			else if(MarioEnterLevelTimer == 60)
			{
				MarioMap.SetActive(false);
				MarioLoadedLevel = Instantiate(MarioLevels[MarioLevelID],Vector3.zero,transform.rotation,Mario3Holder).gameObject;
				MarioClearWalkRight = false;

			}
			else if(MarioEnterLevelTimer >60)
			{
				MarioFadeToBlack.color = new Vector4(0,0,0,1-((MarioEnterLevelTimer-60)/60f));
				MarioClearWalkRight = false;

			}
			if(MarioEnterLevelTimer >120)
			{
				MarioEnteringLevel = false;
				MarioOnMap = false;
				HoldPauseMario = false;
			}
		}

	}


	public void ZeldaClearEnemyLeaveRoom()
	{
		ZeldaRoomRemainingenemies[CurrentRoomID]-= ZeldaCurrentRoomKilledEnemies;
		if(ZeldaEnemyHolder != null)
		{
			Destroy(ZeldaEnemyHolder.gameObject);
		}
	}

	public void ZeldaSpawnEnemies()
	{
		if(Enemyspawners[CurrentRoomID] != null)
		{
			ZeldaEnemyHolder= Instantiate(Enemyspawners[CurrentRoomID],new Vector3(ZeldaCamera.transform.position.x,ZeldaCamera.transform.position.y,0),transform.rotation,ZeldaHolder).transform;
		}
	}

	public void ZeldaCalculateNextID(int Current, int Dir)
	{
		if(Current == 0 && Dir == 1)
		{
			CurrentRoomID = 2;
		}
		else if(Current == 0 && Dir == 4)
		{
			CurrentRoomID = 1;
		}
		else if(Current == 1 && Dir == 3)
		{
			CurrentRoomID = 0;
		}
		else if(Current == 2 && Dir == 2)
		{
			CurrentRoomID = 0;
		}
		else if(Current == 2 && Dir == 1)
		{
			CurrentRoomID = 3;
		}
		else if(Current == 3 && Dir == 2)
		{
			CurrentRoomID = 2;
		}
		else if(Current == 3 && Dir == 4)
		{
			CurrentRoomID = 4;
		}
		else if(Current == 4 && Dir == 3)
		{
			CurrentRoomID = 3;
		}
		else if(Current == 4 && Dir == 4)
		{
			CurrentRoomID = 5;
		}
		else if(Current == 4 && Dir == 1)
		{
			CurrentRoomID = 6;
		}
		else if(Current == 5 && Dir == 3)
		{
			CurrentRoomID = 4;
		}
		else if(Current == 6 && Dir == 2)
		{
			CurrentRoomID = 4;
		}
		else if(Current == 6 && Dir == 1)
		{
			CurrentRoomID = 7;
		}
		else if(Current == 7 && Dir == 2)
		{
			CurrentRoomID = 6;
		}
		else if(Current == 7 && Dir == 1)
		{
			CurrentRoomID = 8;
		}
		else if(Current == 8 && Dir == 2)
		{
			CurrentRoomID = 7;
		}
		else if(Current == 8 && Dir == 1)
		{
			CurrentRoomID = 9;
		}
		else if(Current == 8 && Dir == 4)
		{
			CurrentRoomID = 10;
		}
		else if(Current == 9 && Dir == 2)
		{
			CurrentRoomID = 8;
		}
	}



}
