using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global{
	public static DataHolder Dataholder;
}
public class G{
	public static DataHolder Main;
}


public class DataHolder : MonoBehaviour {

	public SpriteRenderer RollingScreenTransition2;
	public float RollingTimer2;
	public bool rollingtime2;
	public bool InVictoryCutscene;

	public Level[] LevelList;

	public int LevelIndex;
	public float FadeToWhiteTimer;
	public SpriteRenderer FadeToWhite;

	public AdaptiveHUD AHUD;

	public GameObject BirdCage;
	public GameObject FrogCage;
	public GameObject RabbitCage;

	public int CageGroup;

	public bool ScreenTransitionToNextLevel;
	public bool LoadNextLevelOnce;
	public float ScreenTransitionToNextLevelTimer;
	public SpriteRenderer RollingScreenTransition;
	public Level CurrentLevel;

	public GameObject CircleBig;
	public GameObject CircleSmall;

	public GameObject LevelClearArrow;

	public bool PlayerIsCarryingGoal;
	public Vector3 MostRecentPlatformPosition;

	public PlayerMovement PMov;
	public CameraController CamMov;

	public Camera RenderTexCam;
	public Camera ScreenCam;

	public GameObject[] BirdCube_Melody;
	public GameObject[] BirdCube_Harmony;

	public GameObject[] FrogCube_Melody;

	public GameObject[] RabbitCube_Melody;
	public GameObject[] RabbitCube_Harmony;


	public bool[] BirdsAreCube;
	public Vector3[] BirdCubePos;
	public int[] birdIndex; // when birds load, I want some of them to be forming a perimeter, that's the job of these guys.
	public bool[] BirdHarmonyMode;
	public float[] HarmonySwish;
	public float[] BirdHarmonize;

	public bool[] FrogsAreCube;
	public Vector3[] FrogCubePos;
	public int[] FrogIndex; // when frogs load, I want some of them to be forming a perimeter, that's the job of these guys.
	public bool[] FrogHarmonyMode;
	public float[] FrogMelodize;
	public float[] MelodySwish;


	public bool[] RabbitsAreCube;
	public Vector3[] RabbitCubePos;
	public Vector3[] RabbitCubeDimensions;
	public bool[] RabbitAttachedToBirds;

	public int[] RabbitIndex; // when rabbits load, I want some of them to be forming a perimeter, that's the job of these guys.
	public bool[] RabbitHarmonyMode;



	public HUD_Button CurrentHUDButtonSelection;
	public int CurrentHudSelectionAnimalType;
	public bool CurrentHudSelectionHarmony;
	public int CurrentHudSelectionGroupIndex;

	public List<HUD_Button> CurrentHUDButtonsInUse;

	public int MouseOnHUDGrace;

	public bool Paused;
	public GameObject PauseScreen;
	public GameObject OtherMusicPlace;	public GameObject OtherMusicPlace2;


	public GameObject MusicHolder1;
	public GameObject MusicHolder2;

	public int CountHarmonyFrogs()
	{
		int c = 0;
		int i = 0;
		while (i < FrogHarmonyMode.Length) {
			if (FrogHarmonyMode [i] && FrogsAreCube[i]) {
				c++;
			}
			i++;
		}
		return c;
	}

	public void Pause()
	{
		PauseScreen.SetActive (true);
		MusicHolder1.transform.parent = PauseScreen.transform;
		MusicHolder2.transform.parent = PauseScreen.transform;
		OtherMusicPlace.SetActive (false);


	}

	public void UnPause()
	{
		OtherMusicPlace.SetActive (true);

		MusicHolder1.transform.parent = OtherMusicPlace.transform;
		MusicHolder2.transform.parent = OtherMusicPlace2.transform;
		PauseScreen.SetActive (false);
		Paused = false;

	}


	public Vector3 Minus500 = new Vector3(0,0,-500);

	void Start () {

	}
	public float SingalongVolume;

	public bool pauseGrace;

	void LateUpdate()
	{
		if (Paused && pauseGrace) {

			if (Input.GetKeyDown (KeyCode.Escape)) {
				UnPause ();
			}
		}
		pauseGrace = Paused;
	}

	// Update is called once per frame
	void Update () {
		
		if (PMov.InsideStage) {
			SingalongVolume += Time.deltaTime*3;
		} else {
			SingalongVolume -= Time.deltaTime*3;
		}
		SingalongVolume = Mathf.Clamp01 (SingalongVolume);


		FadeToWhiteTimer -= Time.deltaTime * 4;
		FadeToWhite.color = new Vector4 (0, 0, 0, FadeToWhiteTimer);

		if (rollingtime2) {
			RollingTimer2 += Time.deltaTime;
			RollingScreenTransition2.color = new Vector4(0,0,0,SinLerp(0.5f,1,RollingTimer2,1));

				if(RollingTimer2 > 1)
				{
					rollingtime2 = false;
				RollingTimer2 = 1;
				}
			Super.Dataholder.MusicMultiplier = RollingTimer2;

		}

		if (ScreenTransitionToNextLevel) {

			if (ScreenTransitionToNextLevelTimer < 1) {
				ScreenTransitionToNextLevelTimer += Time.deltaTime;
				RollingScreenTransition.color = new Vector4 (0, 0, 0, SinLerp(0,0.5f,ScreenTransitionToNextLevelTimer,1));
				if (ScreenTransitionToNextLevelTimer >= 1) {
					//load next stage!
					if (!LoadNextLevelOnce) {
						LoadNextLevelOnce = true;
						LevelList [LevelIndex].gameObject.SetActive (false);
						LevelIndex++;
						LevelList [LevelIndex].gameObject.SetActive (true);
						int i = 0;
						while (i < 3) {
							BirdsAreCube [i] = false;
							FrogsAreCube [i] = false;
							RabbitsAreCube [i] = false;
							FrogHarmonyMode [i] = false;
							RabbitAttachedToBirds [i] = false;
							i++;
						}
						CurrentHUDButtonSelection = null;
						CurrentHudSelectionAnimalType = -1;
						CurrentHudSelectionHarmony = false;
						PMov.BetweenLevels = false;
						PMov.onGround = false;
						PMov.PHit_Floor.Active = false;
						PMov.PHit_Floor.Col = null;
						PMov.currentYVel = 0.0f;
					}

				}
			} else {
				ScreenTransitionToNextLevelTimer += Time.deltaTime;
				RollingScreenTransition.color = new Vector4 (0, 0, 0, SinLerp(0.5f,1f,ScreenTransitionToNextLevelTimer-1,1));
				if (ScreenTransitionToNextLevelTimer >= 2) {
					RollingScreenTransition.color = Color.clear;
					ScreenTransitionToNextLevel = false;
					LoadNextLevelOnce = false;
					ScreenTransitionToNextLevelTimer = 0;
				}
			}
		}




	}

	void FixedUpdate()
	{
		MouseOnHUDGrace--;



		int i = 0;
		while (i < HarmonySwish.Length) {
			if (HarmonySwish[i] >= 0) { // if you do a bird double jump, set this value to 0
				HarmonySwish[i] += Time.deltaTime;
				BirdHarmonize[i] = ((HarmonySwish[i] - 2) * (HarmonySwish[i] - 1)) * 0.125f + 1;

				if (HarmonySwish[i] > 1) {
					HarmonySwish[i] = -1;
					BirdHarmonize[i] = 1;
				}
			}
			if (MelodySwish[i] >= 0) { // if you do a bird double jump, set this value to 0
				MelodySwish[i] += Time.deltaTime;
				FrogMelodize[i] = ((MelodySwish[i] - 2) * (MelodySwish[i] - 1)) * 0.125f + 1;

				if (MelodySwish[i] > 1) {
					MelodySwish[i] = -1;
					FrogMelodize[i] = 1;
				}
			}
			i++;
		}

		i = 0;
		while (i < BirdCube_Melody.Length) {
			if (BirdsAreCube [i]) {

				if (BirdHarmonyMode [i]) {
					BirdCube_Melody [i].transform.position = Minus500;
					BirdCube_Harmony [i].transform.position = BirdCubePos[i];
				} else {
					BirdCube_Melody [i].transform.position = BirdCubePos[i];
					BirdCube_Harmony [i].transform.position = Minus500;
				}
			} else {
				BirdCube_Melody [i].transform.position = Minus500;
				BirdCube_Harmony [i].transform.position = Minus500;
			}
			if (FrogsAreCube [i]) {

				if (FrogHarmonyMode [i]) {
					FrogCube_Melody [i].transform.position = Minus500;
				} else {
					FrogCube_Melody [i].transform.position = FrogCubePos[i];
				}
			} else {
				FrogCube_Melody [i].transform.position = Minus500;
			}
			if (RabbitsAreCube [i]) {
				if (RabbitHarmonyMode [i]) {
					RabbitCube_Melody [i].transform.position = Minus500;
					RabbitCube_Harmony [i].transform.position = RabbitCubePos[i];
					RabbitCube_Harmony [i].transform.localScale = RabbitCubeDimensions[i] + new Vector3(0,1.25f,0);
				} else {
					RabbitCube_Melody [i].transform.position = RabbitCubePos[i];
					RabbitCube_Melody [i].transform.localScale = new Vector3 (RabbitCubeDimensions[i].x, 1, 1);
					RabbitCube_Harmony [i].transform.position = Minus500;
				}
			} else {
				RabbitCube_Melody [i].transform.position = Minus500;
				RabbitCube_Harmony [i].transform.position = Minus500;
			}
			i++;
		}
			

	}

	void Awake()
	{
		Global.Dataholder = this;
		G.Main = this;

	}

	void OnEnable()
	{
		Global.Dataholder = this;
		G.Main = this;

	}

	[ContextMenu("Set Global")]
	void SetGlobal()
	{
		Global.Dataholder = this;
		G.Main = this;

	}



	public OrbRespawner OrbRes;
	public void RespawnOrb()
	{
		OrbRes.RespawnOrb ();
	}

	public void ResetLevel()
	{

		PMov.transform.position = MostRecentPlatformPosition;
		int i = 0;
		while (i < 3) {
			BirdsAreCube [i] = false;
			FrogsAreCube [i] = false;
			RabbitsAreCube [i] = false;
			FrogHarmonyMode [i] = false;
			RabbitAttachedToBirds [i] = false;
			i++;
		}
		if (PlayerIsCarryingGoal) {
			PlayerIsCarryingGoal = false;
			RespawnOrb ();
		}
		FadeToWhiteTimer = 0.75f;
		CurrentHUDButtonSelection = null;
		CurrentHudSelectionAnimalType = -1;
		CurrentHudSelectionHarmony = false;
		PMov.onGround = false;
		PMov.PHit_Floor.Active = false;
		PMov.PHit_Floor.Col = null;
		PMov.currentYVel = 0.0f;
	}

	public float SpeedrunTime;

	public GameObject CutsceneHolder;
	public GameObject VictoryCutscene;
	public GameObject StartCutscene;
	public GameObject InGameGame;
	public void WinGame()
	{
		CutsceneHolder.SetActive (true);
		VictoryCutscene.SetActive (true);
		InGameGame.SetActive (false);
		StartCutscene.SetActive (false);
	}


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

	public static string StringifyTimeInteger(float time)
	{
		time = Mathf.Ceil (time);
		string s = "";
		int min = 0;
		while(time >= 60){time-=60;min++;}
		time = Mathf.Round(time*100f)/100f;
		s = "" + time;
		if(s.Length == 1){s = "0" + s;}
		s = min + ":" + s;
		return s;
	}

	public static string StringifyTimeWithHours(float time,int minutes)
	{
		string s = "";
		int min = minutes%60;
		int hour = minutes/60;
		time = Mathf.Round(time*100f)/100f;
		s = "" + time;
		if(!s.Contains(".")){s+=".00";}
		else{if(s.Length == s.IndexOf(".")+2){s+="0";}}
		if(s.IndexOf(".") == 1){s = "0" + s;}
		s = (hour>0?(""+hour+":"):(""))+ ((min>9 || hour<1)?(""+min):("0"+min)) + ":" + s;
		return s;
	}




}
