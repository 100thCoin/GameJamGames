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
public class CustomCursor
{
	public Sprite Tex;
	public Vector2 HotSpot;
}

public class DataHolder : MonoBehaviour {

	public bool paused;
	public GameObject PauseMenu;

	public Room ActiveRoom;
	public Camera MainCamera;
	public CustomCursor[] CustomCursors;
	public bool DebugMode;
	public VoiceClip CurrentVoiceClip;
	public bool GameWatchView;
	public WatchButton Watchbutton;

	public bool[] Inventory;
	public Sprite[] InventoryIcons;
	public string[] InventoryNames;
	public int[] InventoryID;

	public SpriteRenderer[] InvObjects;

	public bool[] ArbitraryChecks;
	// Check 0: Has music			(to leave the hat room)
	// Check 1: Turned in music		(To enter frontburger)
	// Check 2: Placed board		(to cross canal)
	// Check 3: Spawn Bird			(to spawn the bird)
	// Check 4: Needs burger		(to get burger)
	// Check 5: placed barstool		(to get to alley)
	// Check 6: Has Keycard			(to get into finalestairs)
	// Check 7: give coupon
	// Check 8: Unlocked Door
	// Check 9: Unlocked KeycardDoor
	// Check 10 respawn
	// check 11 open safe
	// burger placed
	public int ItemInHands = -1;
	public bool ItemIsInHands;

	public GameObject VA_ThatDidntDoAnything;

	public Animator Club4_Collector;
	public GameObject Club4_GunFlash;
	public SpriteRenderer Club4_BlackBar;
	public Animator Club4_Trivia;
	public float ScreenFlashTimer;
	public SpriteRenderer ScreenSwoop;
	public bool PlayerDead;

	public GameObject Frontburgerman;

	public bool WIN;
	public bool LOSE;

	void Start () {

	}

	void LateUpdate()
	{
		if (ArbitraryChecks [4]) {
			Frontburgerman.SetActive (true);
		}

		ScreenFlashTimer -= Time.deltaTime * 2;
		if (ItemInHands != -1) {
			ItemIsInHands = true;
		}
		if (ItemIsInHands) {
			Cursor.SetCursor (Global.Dataholder.CustomCursors [9+ItemInHands].Tex.texture, Global.Dataholder.CustomCursors [9+ItemInHands].HotSpot, CursorMode.ForceSoftware);
			if (ItemInHands == -1) {
				ItemIsInHands = false;
				Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware);
				//print ("RemoveCursor");

			}

		}
	}

	// Update is called once per frame
	void Update () 
	{
		SpeedrunTime += Time.deltaTime;
		if (Input.GetKeyDown (KeyCode.Escape)) {

			paused = !paused;
			PauseMenu.SetActive (paused);


		}

		RefreshInventory ();

	}

	public void RefreshInventory()
	{
		InvObjects [0].enabled = false;
		InvObjects [1].enabled = false;
		InvObjects [2].enabled = false;
		InvObjects [3].enabled = false;

		int j = 0;
		for (int i = 0; i < Inventory.Length; i++) {
			if (Inventory [i]) {
				InvObjects [j].enabled = true;
				InvObjects [j].sprite = InventoryIcons [i];
				InventoryID [j] = i;
				j++;
			}
			if (j == 4) {
				break;
			}
		}

	}


	void FixedUpdate()
	{

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

	public GameObject InGameGame;
	public GameObject WinGameCutscene;
	public GameObject LoseGameCutscene;

	public GameObject VictoryScreen;

	public void WinGame()
	{
		WIN = true;

		InGameGame.SetActive (false);
		WinGameCutscene.SetActive (true);
	}

	public void LoseGame()
	{
		InGameGame.SetActive (false);
		LoseGameCutscene.SetActive (true);
		LOSE = true;
	}

	public float SpeedrunTime;



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
