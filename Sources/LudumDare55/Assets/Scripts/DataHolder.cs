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

	public bool DEBUGBEGIN;
	public int DEBUGBELINLEVEL;

	public PauseManager Pausemenu;
	public bool FailedALevel;
	public GameObject MainCamera;
	public CutsceneManager CutMan;
	public PlayerController Pmov;
	public CameraController CamMov;

	public GameObject ExplosionPrefab;

	public Transform FailCutsceneParent;
	public GameObject FailCutscenePrefab;
	public GameObject CurrentFailCutscene;

	public GameObject[] FailMSG;

	public Transform HeroSpawnPoint;

	public bool PostBoomDialogue;

	public bool ExitingCutscene;
	public float ExitingCutsceneTimer;
	public Vector3 ExitPosS;
	public Vector3 ExitAnglesS;

	public bool TimerActive;
	public float SpeedrunTime;

	public GameObject WellWall;

	public ObjectivesMenu OMenu;

	public HeroController HeroMov;

	public bool ShowVictoryScreen;
	public GameObject VictoryScreen;

	public GameObject SFX_Bomb;
	public GameObject SFX_SpinBoost;

	// Use this for initialization
	void Start () {

		if (DEBUGBEGIN) {
			Level = DEBUGBELINLEVEL;
			LoadLevel (DEBUGBELINLEVEL);
		}
		
	}

	public int Level;
	public bool ResettingLevel;
	public float ResetLevelTimer;
	public bool ResetOnce;
	public SpriteRenderer LevelResetBlackout;
	// Update is called once per frame
	void Update () {
		if (ShowVictoryScreen) {
			VictoryScreen.SetActive (true);
		}
		if (TimerActive) {
			SpeedrunTime += Time.deltaTime;
		}

		if (ResettingLevel) {
			ResetLevelTimer += Time.deltaTime;
			if (ResetLevelTimer < 0.5f) {
				LevelResetBlackout.color = new Vector4 (0, 0, 0, ResetLevelTimer * 2);
			} else {
				LevelResetBlackout.color = new Vector4 (0, 0, 0, 2.5f - ResetLevelTimer * 2);
				if (!ResetOnce) {
					LoadLevel (Level);
					Pmov.transform.position = new Vector3 (0, 0.5f, -28);
					Pmov.RB.velocity = Vector3.zero;
					Pmov.Anim.runtimeAnimatorController = Pmov.RAC_Idle;
					Pmov.grounded = true;
					Pmov.transform.eulerAngles = new Vector3 (0, 0, 0);
					CamMov.PlayerVis.eulerAngles = new Vector3 (0, 0, 0);
					Pmov.Vis.eulerAngles = new Vector3 (0, 0, 0);
					CamMov.transform.eulerAngles = new Vector3 (0, 0, 0);
					Pmov.AtSpawn = true;
					ResetOnce = true;
					Pmov.Collectibles = 0;
					Global.Dataholder.CamMov.rotation = Vector2.zero;
					HeroMov.transform.position = HeroSpawnPoint.transform.position;
					if (CurrentFailCutscene != null) {
						Destroy (CurrentFailCutscene);
					}
					FailMSG[0].transform.localPosition = new Vector3 (FailMSG[0].transform.localPosition.x, -6, 4);
					FailMSG[1].transform.localPosition = new Vector3 (FailMSG[1].transform.localPosition.x, -6, 4);
					FailMSG[2].transform.localPosition = new Vector3 (FailMSG[2].transform.localPosition.x, -6, 4);
					Pmov.walkingVel = Vector2.zero;
				}
			}
			if (ResetLevelTimer > 1.5f) {
				ResettingLevel = false;
				ResetLevelTimer = 0;
				ResetOnce = false;
				OMenu.OnScreen = true;

			}
		}


		if (ExitingCutscene) {
			ExitingCutsceneTimer += Time.deltaTime;
			if (ExitingCutsceneTimer < 1) {
				Super.Dataholder.MusicMultiplier = Mathf.Clamp01(ExitingCutsceneTimer) * 0.5f + 0.5f;
				CamMov.Cam.transform.localPosition = new Vector3 (ParabolicLerp (ExitPosS.x, 0, ExitingCutsceneTimer, 1), ParabolicLerp (ExitPosS.y, 0.375f, ExitingCutsceneTimer, 1), ParabolicLerp (ExitPosS.z, -1.5f, ExitingCutsceneTimer, 1));
				CamMov.Cam.transform.localEulerAngles = new Vector3 (ParabolicLerp (ExitAnglesS.x, 0, ExitingCutsceneTimer, 1), ParabolicLerp (ExitAnglesS.y-360, 0, ExitingCutsceneTimer, 1), ParabolicLerp (ExitAnglesS.z, 0, ExitingCutsceneTimer, 1));
			} else {
				ExitingCutscene = false;
				OMenu.OnScreen = true;
				Super.Dataholder.MusicMultiplier = 1;
			}


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



	public GameObject LoadedLevel;
	public GameObject[] LevelPrefabs;
	public void LoadLevel(int i)
	{
		if (LoadedLevel != null) {
			Destroy (LoadedLevel);
		}
		LoadedLevel = Instantiate (LevelPrefabs [i], Vector3.zero, transform.rotation, transform);
		LoadedLevel.SetActive (true);
		HeroMov.CurrenLevelFilter = LoadedLevel.GetComponent<GetObjectPlayerWants> ();
		if (Level == 3) {
			WellWall.SetActive (false);
		}
	}

	public void HideLevel()
	{
		if (LoadedLevel != null) {
			Destroy (LoadedLevel);
		}
		HeroMov.transform.position = new Vector3 (0, -50000, 0);
	}

	public void DoFailCutscene()
	{
		print ("Failed");
		Pmov.InCutscene = true;
		CurrentFailCutscene = Instantiate (FailCutscenePrefab, FailCutsceneParent.position, FailCutsceneParent.rotation, FailCutsceneParent);

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



	public AudioSource InGameMusic;
	public AudioClip Acapella;

	public void ChangeToBossMusic()
	{
		InGameMusic.clip = Acapella;
		InGameMusic.enabled = false;
		InGameMusic.enabled = true;
	}

}
