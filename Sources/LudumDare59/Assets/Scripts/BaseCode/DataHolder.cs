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

	public float SpeedrunTime;

	public PlatformerMovement PlatformMan;

	public bool paused;
	public bool HotkeyPaused;
	public GameObject PauseMenu;
	public GameObject HotkeyMenu;

	public TASTimeline Timeline;

	public GameObject TasTimeline;
	public GameObject Minigame1;
	public GameObject InternalMonologue;
	public GameObject OpeningCutscene;

	public bool ShowTimeline;
	public float TimelineAppearTimer;



	public bool WonGame;

	void Start () {

	}

	void LateUpdate()
	{

	}

	// Update is called once per frame
	void Update () 
	{
		SpeedrunTime += Time.deltaTime;

		if (Timeline.ChetLocked) {
			HotkeyMenu.SetActive (false);

		}

		if (Input.GetKeyDown (KeyCode.Escape) && !WonGame) {

			if (!HotkeyPaused) {
				paused = !paused;
				PauseMenu.SetActive (paused);
			}
			if (HotkeyPaused) {
				HotkeyPaused = false;
				HotkeyMenu.SetActive (false);
			}
		}

		TimelineAppearTimer += ShowTimeline ? Time.deltaTime : -Time.deltaTime;
		TimelineAppearTimer = Mathf.Clamp01 (TimelineAppearTimer);

		TasTimeline.transform.localPosition = new Vector3(ParabolicLerp(32,16,TimelineAppearTimer,1),0,0);
		Minigame1.transform.localPosition = new Vector3(ParabolicLerp(0,-8,TimelineAppearTimer,1),0,0);
		InternalMonologue.transform.localPosition = new Vector3(ParabolicLerp(8,0,TimelineAppearTimer,1),0,0);
		OpeningCutscene.transform.localPosition = new Vector3(ParabolicLerp(0,-8,TimelineAppearTimer,1),0,0);



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
