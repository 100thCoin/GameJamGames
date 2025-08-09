using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class G{
	public static Main Main;
}

public class Main : MonoBehaviour {

	public GameObject InGame;
	public CamMovement CamMov;
	public CharMovement CharMov;
	public Material[] Glows;
	public int CurrentHandState;
	public int PrevState;
	public float[] GlowTimers;
	public GameObject[] StateBlocks;
	public Transform InLevelClockHand;
	public Transform UI_Watch;
	public Transform UI_Hand;
	public bool UnlockedWatch;
	public ManualHandRotation ManHand;
	public bool WatchUp;
	public float WatchTimer;
	float WallGlow;
	public Texture2D[] Walls;
	public Material WallMat;
	public bool LevelClear;
	public float LevelClearTimer;

	public float WallAltitude;
	public float LevelAltitude;
	public Level[] LevelObjects;
	public int CurrentLevel;
	public GameObject WallObject;

	public float CharHeight;

	public float BlackBrickGlow;
	public Material BlackBRickMat;

	public GameObject HandHolder;
	public float HandHeight;
	public Transform SpinnyLights;

	public Animator Level0Watch;
	public RuntimeAnimatorController Lev0WatchTalking;

	public Animator UIWatch;
	public RuntimeAnimatorController UIWatchIdle;
	public RuntimeAnimatorController UIWatchTalking;
	public RuntimeAnimatorController UIWatchSing;

	public GameObject SnipSFX;

	public float MusicVolume;
	public float VoiceVolume;

	public bool AcapellaOST;
	public float LocalMusicVolume; // between 0 and 1
	public float LocalMusicVolumeAcapella;

	public bool ColorBlindMode;
	bool CurrentCBSetting;
	public Texture[] Bricks_NoCB;
	public Texture[] Bricks_CB;

	public bool SubtitlesActive;

	public bool WatchIsStillTalking;

	public bool PlayingTheAcapellaMusic;

	public GameObject TitleScreen;
	public bool playingTheGame;
	public float SpeedrunTimer;
	public bool LeavingTitle;
	public bool EnteringGame;
	public float LeavingTitleTimer;
	public float EnteringGameTimer;
	public SpriteRenderer TitleFade;
	public Material EnterWhoop;

	public GameObject InGameMusic;
	public GameObject AcapellaMusic;
	public GameObject ScreenTrans;

	public LCD[] FutureClockOnes;
	public LCD[] FutureClockTens;

	public float Lev5CamTimer;

	public Transform CharHand;
	public GameObject VictoryJingle;
	public bool WonTheGame;
	public GameObject VitoryScreen;
	public float VictoryTimer;
	public bool DoneTheGame;
	public TextMesh SpeedrunClock;
	public GameObject WinMusic;

	public GameObject SubsObject;
	public Subtitles Subs;

	public GameObject[] VALines;

	public bool[] ArbitrarilyLargeArrayOfBooleans;
	public float AhYesBatteriesTimer;

	public bool QueueTheCantPushDialogue;
	public GameObject ControllsPrompt;
	public void BeginGame()
	{
		LeavingTitle = true;
	}

	// Use this for initialization
	void Start () {
		
	}

	public bool[] HitAllControlls;

	float[] WallAlts = new float[]{0,-40,-80,-120,-160,-200,-240,-300};
	float[] SandAlts = new float[]{-34,-34,-22f,-34,-7.5f,-300};

	// Update is called once per frame
	void Update () {

		if(CurrentLevel ==5)
		{
			Lev5CamTimer = Mathf.Clamp01(Lev5CamTimer + Time.deltaTime*0.5f); 

			CamMov.ManualAngleAdjustment = TwoCurveLerp(20,10,Lev5CamTimer,2);

		}


		if(LeavingTitle)
		{
			LeavingTitleTimer +=Time.deltaTime;
			LocalMusicVolume = 1-LeavingTitleTimer;
			TitleFade.color = new Vector4(0,0,0,LeavingTitleTimer);
			EnterWhoop.SetFloat("_Pos", 1.3f);

			if(LeavingTitleTimer > 1.5f)
			{
				TitleScreen.SetActive(false);
				InGame.SetActive(true);
				playingTheGame = true;
				EnteringGame = true;
				LeavingTitle = false;
			}
		}

		if(EnteringGame)
		{
			EnteringGameTimer += Time.deltaTime;
			if(EnteringGameTimer < 1)
			{
				EnterWhoop.SetFloat("_Pos", TwoCurveLerp(1.3f,0.5f,EnteringGameTimer,1));
			}
			else
			{
				LocalMusicVolume = 1;
			}
			if(EnteringGameTimer >=1.5f)
			{
				InGameMusic.SetActive(true);
				EnterWhoop.SetFloat("_Pos", TwoCurveLerp(0.5f,-64,Mathf.Clamp01(EnteringGameTimer-1.5f),1));
			}
			if(EnteringGameTimer >=2)
			{

				if(!ArbitrarilyLargeArrayOfBooleans[8])
				{
					ArbitrarilyLargeArrayOfBooleans[8] = true;
					Instantiate(VALines[0]);
				}
			}

			if(EnteringGameTimer > 3)
			{
				Destroy(ScreenTrans);
				EnteringGame = false;
			}
			

		}

		SubsObject.SetActive(SubtitlesActive);

		if(playingTheGame &&!WonTheGame)
		{
			SpeedrunTimer += Time.deltaTime;
		}

		if(WonTheGame)
		{
			VictoryTimer+= Time.deltaTime;
			if(VictoryTimer > 2)
			{
				VitoryScreen.SetActive(true);
				if(!DoneTheGame)
				{
					SpeedrunClock.text = StringifyTime(SpeedrunTimer);
					DoneTheGame = true;
				}
			}
			if(VictoryTimer > 4)
			{
				WinMusic.SetActive(true);
			}

		}


		if(AcapellaOST)
		{
			LocalMusicVolume = Mathf.Clamp01(LocalMusicVolume - Time.deltaTime * 0.25f);
			LocalMusicVolumeAcapella = Mathf.Clamp01(LocalMusicVolumeAcapella + Time.deltaTime * 0.25f);
		}
		else
		{
			LocalMusicVolume = Mathf.Clamp01(LocalMusicVolume + Time.deltaTime * 0.25f);
			LocalMusicVolumeAcapella = Mathf.Clamp01(LocalMusicVolumeAcapella - Time.deltaTime * 0.25f);
		}


		if(CurrentCBSetting != ColorBlindMode)
		{
			CurrentCBSetting = ColorBlindMode;
			int j = 0;
			while(j < Glows.Length)
			{
				Glows[j].SetTexture("_MainTex",ColorBlindMode ? Bricks_CB[j] : Bricks_NoCB[j]);
				j++;
			}

		}

		if(LevelClear && CurrentLevel != 5)
		{

			LevelClearTimer +=Time.deltaTime;

			if(CurrentLevel == 0)
			{
				if(!ArbitrarilyLargeArrayOfBooleans[1] && !WatchIsStillTalking)
				{
					ArbitrarilyLargeArrayOfBooleans[1] = true;
					Instantiate(VALines[5]);
				}
			}

			if(CurrentLevel == 2)
			{
				// slow music
				AcapellaOST = true;
				if(!ArbitrarilyLargeArrayOfBooleans[0])
				{
					ArbitrarilyLargeArrayOfBooleans[0] = true;
					Instantiate(VALines[7]);
				}

			}

			if(CurrentLevel == 3)
			{
				AhYesBatteriesTimer += Time.deltaTime;
				LocalMusicVolumeAcapella -= Time.deltaTime*6;
				if(AhYesBatteriesTimer > 2.5f)
				{
					LocalMusicVolume = 1;
					PlayingTheAcapellaMusic = false;
				}
				if(!ArbitrarilyLargeArrayOfBooleans[9])
				{
					ArbitrarilyLargeArrayOfBooleans[9] = true;
					Instantiate(VALines[10]);
				}
				AcapellaOST = false;
			}

			if(LevelClearTimer > 0)
			{
				if(CharHeight == -1)
				{
					CharHeight = CharMov.transform.position.y;
				}

				float t = Mathf.Clamp01(LevelClearTimer*0.25f);
				LevelAltitude = TwoCurveLerp(0,-40,t,1);
				WallAltitude = TwoCurveLerp(WallAlts[CurrentLevel],WallAlts[CurrentLevel+1],t,1);
				HandHeight = TwoCurveLerp(SandAlts[CurrentLevel],SandAlts[CurrentLevel+1],t,1);

				LevelObjects[CurrentLevel].transform.position = new Vector3(0,LevelAltitude,0);
				WallObject.transform.position = new Vector3(0,WallAltitude,0);
				CharMov.transform.position = new Vector3(CharMov.transform.position.x,TwoCurveLerp(CharHeight,CharHeight-40,t,1),CharMov.transform.position.z);
				CharMov.UpdateCameraFalling = true;
				HandHolder.transform.position = new Vector3(0,HandHeight,0);

			}

			if(LevelClearTimer > 5)
			{
				LevelObjects[CurrentLevel].gameObject.SetActive(false);
				CurrentLevel++;
				LevelClearTimer = -2;
				LevelClear = false;
				LevelObjects[CurrentLevel].gameObject.SetActive(true);
				CharHeight=-1;
				CharMov.UpdateCameraFalling = false;
				StateBlocks = LevelObjects[CurrentLevel].LevelParts;
				// just in case: #SoftlockPrevention
				UnlockedWatch = true;
				BlackBrickGlow = 1;
				GlowTimers[0] = 0;
				GlowTimers[1] = 0;
				GlowTimers[2] = 0;
				GlowTimers[3] = 0;
				GlowTimers[4] = 0;
				GlowTimers[5] = 0;

			}

		}

		if(playingTheGame)
		{
			if(Input.GetKeyDown(KeyCode.Space)){HitAllControlls[0] = true;}
			if(Input.GetKeyDown(KeyCode.W)){HitAllControlls[1] = true;}
			if(Input.GetKeyDown(KeyCode.A)){HitAllControlls[2] = true;}
			if(Input.GetKeyDown(KeyCode.S)){HitAllControlls[3] = true;}
			if(Input.GetKeyDown(KeyCode.D)){HitAllControlls[4] = true;}

			if(HitAllControlls[0] && HitAllControlls[1] && HitAllControlls[2] && HitAllControlls[3] && HitAllControlls[4])
			{
				ControllsPrompt.SetActive(false);
			}


		}

		if(CharMov.transform.position.y < 0)
		{
			CharMov.transform.position = new Vector3(CharMov.transform.position.x,0,CharMov.transform.position.z);
		}

		float Ang = InLevelClockHand.eulerAngles.y;

		if(UnlockedWatch)
		{
			int dir = 0;
			if(ManHand != null)
			{
				if(!ManHand.TouchingPlayer && !LevelClear)
				{
					if(Input.GetKey(KeyCode.LeftArrow) && ManHand.Obstructed != -1)
					{
						dir--;
					}
					if(Input.GetKey(KeyCode.RightArrow)  && ManHand.Obstructed != 1)
					{
						dir++;
					}
				}
			}
			Ang+=Time.deltaTime*dir*130;

			//if(Input.GetKeyDown(KeyCode.LeftShift))
			//{
		    //	WatchUp = !WatchUp;
		    //}
			WatchUp = UnlockedWatch;
			WatchTimer = Mathf.Clamp(WatchTimer + (WatchUp ? Time.deltaTime : -Time.deltaTime),0,0.5f);
			UI_Watch.transform.position = new Vector3(128,SinLerp(-128,128,WatchTimer,0.5f),0);

		}


		while(Ang < 0)
		{
			Ang += 360;
		}
		while(Ang >= 360)
		{
			Ang -= 360;
		}
		Ang = Ang % 360;
		// i guess it's somehow locked between -180 and 180
		InLevelClockHand.eulerAngles = new Vector3(0,Ang,0);

		if(CurrentLevel == 0 && !UnlockedWatch)
		{
			if(Ang > 40 && Ang < 50)
			{
				QueueTheCantPushDialogue = true;
			}
			if(Ang <5)
			{
				QueueTheCantPushDialogue = false;
			}

		}





		UI_Hand.eulerAngles = new Vector3(0,0,-Ang+90);

		if(InLevelClockHand.eulerAngles.y >= 0 && InLevelClockHand.eulerAngles.y <= 30)
		{
			CurrentHandState= 4;
		}
		else if(InLevelClockHand.eulerAngles.y > 30 && InLevelClockHand.eulerAngles.y <= 90)
		{
			CurrentHandState =5;
		}
		else if(InLevelClockHand.eulerAngles.y > 90 && InLevelClockHand.eulerAngles.y <= 150)
		{
			CurrentHandState = 0;
		}
		else if(InLevelClockHand.eulerAngles.y > 150 && InLevelClockHand.eulerAngles.y <= 210)
		{
			CurrentHandState = 1;
		}
		if(InLevelClockHand.eulerAngles.y > 210 && InLevelClockHand.eulerAngles.y <= 270)
		{
			CurrentHandState = 2;
		}
		else if(InLevelClockHand.eulerAngles.y > 270 && InLevelClockHand.eulerAngles.y <= 330)
		{
			CurrentHandState = 3;
		}
		else if(InLevelClockHand.eulerAngles.y > 330 && InLevelClockHand.eulerAngles.y <= 360)
		{
			CurrentHandState = 4;
		}

		SpinnyLights.eulerAngles = new Vector3(0,Ang,0);

		float CharAng = CharMov.Flip.transform.eulerAngles.y;
		while(CharAng < 0)
		{
			CharAng += 360;
		}
		while(CharAng >= 360)
		{
			CharAng -= 360;
		}
		CharAng = CharAng % 360;

		CharHand.transform.localEulerAngles = new Vector3(0,(CharAng> 90 && CharAng < 270 ? 180 : 0),-Ang );

		int i = 0;
		while(i < GlowTimers.Length)
		{

			GlowTimers[i] = Mathf.Clamp01(GlowTimers[i] + Time.deltaTime * (CurrentHandState == i ? 4 : -5));
			Glows[i].SetFloat("_Glow",GlowTimers[i] == 0 ? 0 :1-GlowTimers[i]);

			StateBlocks[i].SetActive(GlowTimers[i] > 0);

			i++;
		}
		BlackBrickGlow = Mathf.Clamp01(BlackBrickGlow - Time.deltaTime);
		BlackBRickMat.SetFloat("_Glow",BlackBrickGlow);

		if(PrevState != CurrentHandState)
		{
			PrevState = CurrentHandState;
			WallMat.SetTexture("_MainTex",Walls[CurrentHandState]);
			WallGlow = 0.25f;
			Instantiate(SnipSFX);
		}

		WallMat.SetFloat("_Glow",WallGlow);
		WallGlow = Mathf.Clamp01(WallGlow - Time.deltaTime*2);

		if(CurrentLevel == 5)
		{
			int Timee = (Mathf.RoundToInt(Ang/6f) + 45) %60;
			int Sec = Timee%10;
			int Min = Timee/10;
		
			FutureClockOnes[0].On = (Sec == 0 || Sec == 2 || Sec == 3 || Sec == 5 || Sec == 6 || Sec == 8 || Sec == 9);
			FutureClockOnes[1].On = (Sec == 0 || Sec == 1 || Sec == 3 || Sec == 4 || Sec == 5 || Sec == 6 || Sec == 7 || Sec == 8 || Sec == 9);
			FutureClockOnes[2].On = (Sec == 2 || Sec == 3 || Sec == 4 || Sec == 5 || Sec == 6 || Sec == 8 || Sec == 9);
			FutureClockOnes[3].On = (Sec == 0 || Sec == 1 || Sec == 2 || Sec == 3 || Sec == 4 || Sec == 7 || Sec == 8 || Sec == 9);
			FutureClockOnes[4].On = (Sec == 0 || Sec == 2 || Sec == 3 || Sec == 5 || Sec == 6 || Sec == 7 || Sec == 8 || Sec == 9);
			FutureClockOnes[5].On = (Sec == 0 || Sec == 4 || Sec == 5 || Sec == 6 || Sec == 8 || Sec == 9);
			FutureClockOnes[6].On = (Sec == 2 || Sec == 4 || Sec == 5 || Sec == 6 || Sec == 8 || Sec == 9);
			FutureClockOnes[7].On = (Sec == 0 || Sec == 2 || Sec == 6 || Sec == 8);

			Sec = Min;
			FutureClockTens[0].On = (Sec == 0 || Sec == 2 || Sec == 3 || Sec == 5 || Sec == 6 || Sec == 8 || Sec == 9);
			FutureClockTens[1].On = (Sec == 0 || Sec == 1 || Sec == 3 || Sec == 4 || Sec == 5 || Sec == 6 || Sec == 7 || Sec == 8 || Sec == 9);
			FutureClockTens[2].On = (Sec == 2 || Sec == 3 || Sec == 4 || Sec == 5 || Sec == 6 || Sec == 8 || Sec == 9);
			FutureClockTens[3].On = (Sec == 0 || Sec == 1 || Sec == 2 || Sec == 3 || Sec == 4 || Sec == 7 || Sec == 8 || Sec == 9);
			FutureClockTens[4].On = (Sec == 0 || Sec == 2 || Sec == 3 || Sec == 5 || Sec == 6 || Sec == 7 || Sec == 8 || Sec == 9);
			FutureClockTens[5].On = (Sec == 0 || Sec == 4 || Sec == 5 || Sec == 6 || Sec == 8 || Sec == 9);
			FutureClockTens[6].On = (Sec == 2 || Sec == 4 || Sec == 5 || Sec == 6 || Sec == 8 || Sec == 9);
			FutureClockTens[7].On = (Sec == 0 || Sec == 2 || Sec == 6 || Sec == 8);


		}





	}

	public bool WatchGrace;

	void LateUpdate()
	{
		if(!AcapellaOST)
		{
			PlayingTheAcapellaMusic = false;
		}
		if(PlayingTheAcapellaMusic)
		{
			UIWatch.runtimeAnimatorController = UIWatchSing;
		}
		else
		{
			if(WatchIsStillTalking)
			{
				UIWatch.runtimeAnimatorController = UIWatchTalking;
				if(CurrentLevel == 0 && !UnlockedWatch)
				{
					Level0Watch.runtimeAnimatorController = Lev0WatchTalking;
				}

			}
			if(!WatchGrace)
			{
				
				if(!WatchIsStillTalking)
				{
					UIWatch.runtimeAnimatorController = UIWatchIdle;
					Level0Watch.runtimeAnimatorController = null;

					if(QueueTheCantPushDialogue)
					{
						QueueTheCantPushDialogue = false;
						if(!ArbitrarilyLargeArrayOfBooleans[10])
						{
							ArbitrarilyLargeArrayOfBooleans[10] = true;
							Instantiate(VALines[2]);
						}


					}

				}
				WatchIsStillTalking = false;
			}
			WatchGrace = false;
		}
	}




	//////////////////////////////////////////////////////////////////////////////////////////////
	//																							//
	//	PUBLIC FUNCITONS.																		//
	//																							//
	//////////////////////////////////////////////////////////////////////////////////////////////

	// Various smooth lerp functions
	public static float ParabolicLerp(float sPos, float dPos, float t, float dur)
	{
		return (((sPos-dPos)*Mathf.Pow(t,2))/Mathf.Pow(dur,2))-(2*(sPos-dPos)*(t))/(dur)+sPos;
	}
	public static float SinLerp(float sPos, float dPos, float t, float dur)
	{
		return Mathf.Sin((Mathf.PI*(t))/(2*dur))*(dPos-sPos) + sPos;
	}
	public static float TwoCurveLerp(float sPos, float dPos, float t, float dur)
	{
		return -Mathf.Cos(Mathf.PI*t*(1/dur))*0.5f*(dPos-sPos)+0.5f*(sPos+dPos);
	}

	// Converts a float in seconds to a string in MN:SC.DC format
	// example: 68.1234 becomes "1:08.12"
	public static string StringifyTime(float time)
	{
		string s = "";
		int min = 0;
		while(time >= 60){time-=60;min++;}
		time = Mathf.Round(time*100f)/100f;
		s = "" + time;
		if(!s.Contains(".")){s+=".00";}
		else{if(s.Length == s.IndexOf(".")+2){s+="0";}}
		if(s.IndexOf(".") == 1){s = "0" + s;}
		s = min + ":" + s;
		return s;
	}


	//////////////////////////////////////////////////////////////////////////////////////////////
	//																							//
	//	                																		//
	//																							//
	//////////////////////////////////////////////////////////////////////////////////////////////

	[ContextMenu("Example")]
	void InspectorFunctionExample()
	{
		print("Hello World!");
	}

	//assigns the global Main
	void Awake()
	{
		G.Main = this;
	}
	void OnEnable()
	{
		G.Main = this;
	}

}
