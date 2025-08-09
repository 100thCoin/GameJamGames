using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global{
	public static DataHolder Dataholder;
}
public class G{
	public static DataHolder Main;
}

[System.Serializable]
public class RibbonStrip
{
	public int ID;
	public float StartT;
	public float StopT;

	public RibbonStrip(int id, float start, float stop)
	{
		ID = id;
		StartT = start;
		StopT = stop;
	}

}


public class DataHolder : MonoBehaviour {

	public GameLoader GameLoad;

	public float TimeSinceLeftRibbon;
	public SpriteRenderer PressShiftTooltip;

	public bool DEBUGMODE;

	public float MusicVolume;
	public float SFXVolume;

	public HUD HUDMan;
	public RibbonManager RibbonMan;
	public RibbonMover PlayerMov2D;
	public MovementThreeD PlayerMov3D;
	public CameraMover CamMov;
	public int[] RibbonCollectibles;
	public int[] ImplementedRibbonsPerType;
	public int RibbonIDForIcon;
	public float TimeSinceLastCollectible; //presumably for on-screen text if I remember this var exists.

	public RibbonCustomizerMenu CustomRibbonMan;

	public AdvancedRibbonPiecce LoneRibbon;
	public AdvancedRibbonPiecce[] TriRibbonSet;
	public GameObject TriSetHolder;

	public Material ShortRibbonRTMat;
	public Material LongRibbonRTMat;
	public MeshRenderer ThreeDRibbonMeshRenderer;

	public bool DEBUG_TestUpdatingRibbon;

	public RibbonStrip[] TriStripProperties;
	public RibbonStrip LoneStripProperties;

	public float TwoDPlayerIsAtTValue;
	public int TwoDPlayerIsTouchingID;

	public bool TriStrip;
	public float RibbonLengthInUnits;

	public GameObject GoHerePosters;

	public GameObject PauseMenu;
	public bool CanPause;

	public float SpeedrunTime;
	public int TimesEnteredTheRibbon;

	public bool InGame;

	public SpriteRenderer TUT;
	public Sprite TUTshift;

	public GameObject Sparkles;

	public bool GameEnded;
	public float EndedTimer;

	public Material ScreenTransition;
	public float ScreenTransitionT;
	public float ScreenTrantime;
	public bool DoSCreenIn;
	public bool DoScreenOut;
	public float MusicMultiplier;

	public GameObject _TwistEnding;
	public GameObject _InGame;
	public GameObject _Title;

	public float HouseMusicMult =1;

	public bool NoMoreTitleButtons;

	public GameObject SFX_SpoolCollect;
	public GameObject SFX_MinigameWin;

	public GameObject SFX_ButtonPress;
	public GameObject SFX_ButtonRelease;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (RibbonMan.PlayerIsInsideRibbon) {
			HouseMusicMult += Time.deltaTime;

		}
		else{
			HouseMusicMult -= Time.deltaTime;

		}

		HouseMusicMult = Mathf.Clamp01 (HouseMusicMult);

		if (InGame && !RibbonMan.PlayerIsInsideRibbon) {

			TimeSinceLeftRibbon += Time.deltaTime;

			if (TimeSinceLeftRibbon > 45) {
				if (TimesEnteredTheRibbon == 0) {
					PressShiftTooltip.enabled = true;

				}
			}

		}

		if (TimesEnteredTheRibbon > 0) {
			PressShiftTooltip.enabled = false;
		}


		if (InGame && !GameEnded) {
			SpeedrunTime += Time.deltaTime;
		}


		if (GameEnded) {
			DoSCreenIn = true;
			EndedTimer += Time.deltaTime;
			MusicMultiplier = 1 - EndedTimer * 0.5f;
			if (EndedTimer > 2) {

				_InGame.SetActive (false);
				_TwistEnding.SetActive (true);
				MusicMultiplier = 1;
			}

		}


		if (DoSCreenIn) {
			ScreenTrantime += Time.deltaTime;
			if (ScreenTrantime >= 1) {
				DoSCreenIn = false;
			}
		}
		if (DoScreenOut) {
			ScreenTrantime -= Time.deltaTime;
			if (ScreenTrantime <= 0) {
				DoScreenOut = false;
			}
		}
		ScreenTrantime = Mathf.Clamp01 (ScreenTrantime);

		ScreenTransitionT = SinLerp (0, 1, ScreenTrantime, 1);

		ScreenTransition.SetFloat ("_Pos", ScreenTransitionT*11.4f-10);



		TimeSinceLastCollectible += Time.deltaTime;

		if (DEBUG_TestUpdatingRibbon) {
			DEBUG_TestUpdatingRibbon = false;
			UpdateRibbonStructure ();
		}

		if (!PauseMenu.activeSelf && !PlayerMov3D.RibbonCustomizeMenu.activeSelf) {
			if (!CanPause) {

				if (Input.GetKeyUp (KeyCode.Escape)) {

					CanPause = true;
				}

			} else {
				if (Input.GetKeyDown (KeyCode.Escape)) {
					PauseMenu.SetActive (true);
					CanPause = false;
				}
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





	public void UpdateRibbonStructure()
	{
		GoHerePosters.SetActive (true);

		if (CustomRibbonMan.SecondID == -1) {
			//lone ribbon
			TriSetHolder.SetActive (false);
			LoneRibbon.gameObject.SetActive (true);
			LoneRibbon.Updoodle (CustomRibbonMan.FirstID);
			//get ID
			ThreeDRibbonMeshRenderer.material = ShortRibbonRTMat;

			RibbonMan.Updoodle (CustomRibbonMan.RibbonLengths [CustomRibbonMan.FirstID]);

			PlayerMov2D.ExtremaLeft = PlayerMov2D.SHORTExtremaLeft;
			PlayerMov2D.ExtremaRight = PlayerMov2D.SHORTExtremaRight;

			LoneStripProperties = new RibbonStrip (CustomRibbonMan.FirstID, 0, 1); //easy

			TriStrip = false;

			RibbonLengthInUnits = CustomRibbonMan.RibbonLengths [CustomRibbonMan.FirstID]*4;


		} else {
			// gotta update 3 ribbons
			TriStrip = true;

			TriSetHolder.SetActive (true);
			LoneRibbon.gameObject.SetActive (false);

			TriRibbonSet[0].Updoodle (CustomRibbonMan.FirstID);
			TriRibbonSet[1].Updoodle (CustomRibbonMan.FirstID);
			TriRibbonSet[2].Updoodle (CustomRibbonMan.SecondID);
			ThreeDRibbonMeshRenderer.material = LongRibbonRTMat;
			RibbonLengthInUnits = CustomRibbonMan.RibbonLengths [CustomRibbonMan.FirstID] + CustomRibbonMan.RibbonLengths [CustomRibbonMan.SecondID];
			RibbonMan.Updoodle (RibbonLengthInUnits);
			RibbonLengthInUnits *= 4;
			PlayerMov2D.ExtremaLeft = PlayerMov2D.LONGExtremaLeft;
			PlayerMov2D.ExtremaRight = PlayerMov2D.LONGExtremaRight;

			TriStripProperties = new RibbonStrip[3];


			float midP = TriRibbonSet[2].transform.localPosition.x;
			float len = CustomRibbonMan.RibbonLengths [CustomRibbonMan.SecondID]*4;

			//total length is 48
			float startMid = midP-len/2f + RibbonLengthInUnits/2f;
			float stopMid = midP+len/2f + RibbonLengthInUnits/2f;

			TriStripProperties[0] = new RibbonStrip (CustomRibbonMan.FirstID, 0, startMid/RibbonLengthInUnits);
			TriStripProperties[1] = new RibbonStrip (CustomRibbonMan.FirstID, stopMid/RibbonLengthInUnits, 1);
			TriStripProperties[2] = new RibbonStrip (CustomRibbonMan.SecondID, startMid/RibbonLengthInUnits, stopMid/RibbonLengthInUnits);

		}

		PlayerMov3D.CanOpenQMenu = true;


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
