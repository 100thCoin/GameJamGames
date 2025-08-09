using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{
	public static DataHolder DataHolder;
}

public class DataHolder : MonoBehaviour {

	public bool DEBUG_StartFromZero;

	public GameObject VictoryScreen;
	public GameObject PauseSmall;
	public GameObject PauseLarge;

	public VoiceQueue VQ;

	public int CinematicIndex;
	public GameObject[] CinemaScenes;
	public GameObject Game;

	public GameObject PrologueLevel;
	public GameObject EpilogueLevel;

	public GameObject Level1Object;
	public GameObject Level2Object;

	public CameraController CamMov;

	public GameObject GridLevel1;
	public GameObject GridLevel2;

	public bool InGame;
	public bool GameIsPaused;
	public bool PlayerTouchedAxe;
	public bool WatchingCinematicCutscene;
	public Vector3 SpawnPos;
	public PlayerMovement PMov;
	public GameObject AxeObject;
	public LevelEditor LevEditMain;
	public bool InLevelEditor;
	public float InEditorMusicShifter;
	public TextMesh LevEditTooltip;
	public int TooltipGrace;
	public int EvilPoints;
	public bool Level2;
	public bool CutscenePreLevel1;
	public int RequiredEvilPoints;
	public int RequiredEvilPoints2;

	public RuntimeAnimatorController Funeral_Idle;
	public RuntimeAnimatorController Funeral_Run;
	public RuntimeAnimatorController Funeral_Crouch;
	public RuntimeAnimatorController Funeral_Jump;

	public bool PreLevel1;

	public GameObject TemporaryLevel;
	public GameObject EmptyLevel;

	public bool CutsceneOver;
	public float CutsceneOverFadeTowhite;
	public SpriteRenderer FadeToWhite;
	public List<LevelEditorPick> EditorPickList;
	public bool CutsceneDoOnce;

	public bool HaltTimer;
	public float SpeedrunTimer;

	public bool UnlockedUsingLevelStrings;

	public bool StringLevelMode;

	void Awake()
	{
		Global.DataHolder = this;

	}

	void OnEnable()
	{
		Global.DataHolder = this;

	}


	public void WinGame()
	{
		// enable victory screen.
		print ("YOU WIN");
		VictoryScreen.SetActive (true);
		PlayerPrefs.SetInt ("_Djam8_Vilemourn_UnlockedLevelStrings", 1);
		Super.Dataholder.UnlockedUsingLevelStrings = true;
	}


	public void ResetRoom()
	{



	}

	public void NextCinematic()
	{
		CamMov.transform.position = new Vector3 (0, 0, -10);
		CamMov.InCinematic = true;
		CinemaScenes [CinematicIndex].SetActive (true);
		WatchingCinematicCutscene = true;

		CinematicIndex++;
	}

	bool WaitAFrame = false;

	// Use this for initialization
	void Start () {



		if (DEBUG_StartFromZero) {
			Level2 = false;
			InLevelEditor = false;
			Level1Object.SetActive (false);
			Level2Object.SetActive (false);
			if (EpilogueLevel != null) {
				EpilogueLevel.SetActive (false);
			}
			PrologueLevel.SetActive (true);
			CamMov.InLargeLevel = false;
			CamMov.InCinematic = true;
			CutscenePreLevel1 = true;
			GridLevel1.SetActive (false);
			GridLevel2.SetActive (false);
			InGame = false;
			LevEditMain.gameObject.SetActive (false);
			Game.SetActive (true);
			PreLevel1 = true;
			CamMov.transform.position = new Vector3 (0, 46, -10);
		}

	}

	public Vector3 PlayerVelWhenPaused;

	// Update is called once per frame
	void Update () {
		if (StringLevelMode && WaitAFrame) {
			StringLevelMode = false;
			EnterStringLevel ();
		}
		WaitAFrame = true;

		if (InLevelEditor) {
			InEditorMusicShifter += Time.deltaTime;
		} else {
			InEditorMusicShifter -= Time.deltaTime;
		}

		if (!WatchingCinematicCutscene && !PlayerTouchedAxe) {

			if (Input.GetKeyDown (KeyCode.Escape)) {

				if (GameIsPaused) {

					GameIsPaused = false;
					PauseLarge.SetActive (false);
					PauseSmall.SetActive (false);
					PMov.RB.velocity = PlayerVelWhenPaused;
				} else {
					if (InLevelEditor) {
						PauseLarge.SetActive (true);

					} else {
						PauseSmall.SetActive (true);

					}
					PlayerVelWhenPaused = PMov.RB.velocity;
					PMov.RB.velocity = Vector3.zero;
					GameIsPaused = true;

				}
			}

		}


		InEditorMusicShifter = Mathf.Clamp01 (InEditorMusicShifter);
		if (!HaltTimer) {
			SpeedrunTimer += Time.deltaTime;
		}


		TooltipGrace--;
		if (TooltipGrace < 0) {
			LevEditTooltip.text = "";
		}
		if (CutsceneOver) {
			CutsceneOverFadeTowhite += Time.deltaTime;
			FadeToWhite.gameObject.SetActive (true);
			FadeToWhite.color = new Vector4 (1, 1, 1, CutsceneOverFadeTowhite);
			if (CutsceneOverFadeTowhite > 1) {
				if (!CutsceneDoOnce) {
					if (Level2) {
						// load finale room
						CinemaScenes [CinematicIndex - 1].SetActive (false);
						Game.SetActive (true);
						EpilogueLevel.SetActive (true);
						CamMov.InLargeLevel = false;
						Level2Object.SetActive (false);
						Level1Object.SetActive (false);
						PMov.transform.position = new Vector3 (-7, -8, 0);

						PMov.RACIdle = Funeral_Idle;
						PMov.RACJump = Funeral_Jump;
						PMov.RACRun = Funeral_Run;
						PMov.RACCrouch = Funeral_Crouch;
						CamMov.transform.position = new Vector3 (0, 46, -10);

					} else {
						// load level editor
						CinemaScenes [CinematicIndex - 1].SetActive (false);
						Game.SetActive (true);
						InLevelEditor = true;
						LevEditMain.gameObject.SetActive (true);
						if (CutscenePreLevel1) {
							Level1Object.SetActive (true);
							PrologueLevel.SetActive (false);
							GridLevel1.SetActive (true);

						} else {
							Level2Object.SetActive (true);
							Level1Object.SetActive (false);
							GridLevel1.SetActive (false);
							GridLevel2.SetActive (true);
							LevEditMain.level = 1;
						}
						CamMov.InLargeLevel = true;

					}
					CutsceneDoOnce = true;
					CamMov.InCinematic = false;
					InEditorMusicShifter = 1;
				}
				FadeToWhite.color = new Vector4 (1, 1, 1, 2-CutsceneOverFadeTowhite);


			}
			if (CutsceneOverFadeTowhite > 2) {
				CutsceneOver = false;
				CutsceneOverFadeTowhite = 0;
				if (!CutscenePreLevel1) {
					Level2 = true;
				}
				CutscenePreLevel1 = false;
				FadeToWhite.gameObject.SetActive (false);
				CutsceneDoOnce = false;
				PlayerTouchedAxe = false;
				PreLevel1 = false;
				WatchingCinematicCutscene = false;
			}

		}


	}






	public void EnterLevelExitEditor()
	{
		Global.DataHolder.InLevelEditor = false;
		// all "LevEditOBJs" must be replaced with the prefab counterparts.

		if (TemporaryLevel != null) {
			Destroy (TemporaryLevel); // This should already be destroyed when the editor is entered, but better safe than sorry.
		}
		TemporaryLevel = Instantiate (EmptyLevel,transform.position,transform.rotation,transform);

		int i = 0;
		while (i < EditorPickList.Count) {
			if (EditorPickList [i].Placed) {
				if (EditorPickList [i].PrefabObject != null) {
					Instantiate (EditorPickList [i].PrefabObject, new Vector3 (LevEditMain.transform.position.x + EditorPickList [i].PlacePos.x * 2 + EditorPickList [i].ObjectOffset.x * 2 + EditorPickList [i].FineObjectOffset.x, LevEditMain.transform.position.y + EditorPickList [i].PlacePos.y * 2 + EditorPickList [i].ObjectOffset.y * 2 + EditorPickList [i].FineObjectOffset.y, 0), transform.rotation, TemporaryLevel.transform);
				} else {
					if (EditorPickList [i].Name != "Chadford") {
						Debug.LogError ("EditorPickList[i] is MISSING the PrefabObject that loads when exiting the editor!");
					}
				}
			}
			i++;
		}

		// disable the editor junk

		LevEditMain.gameObject.SetActive (false);

	}
	public void EnterLevelEditor()
	{
		Global.DataHolder.InLevelEditor = true;
		// all "LevEditOBJs" must be replaced with the prefab counterparts.

		if (TemporaryLevel != null) {
			Destroy (TemporaryLevel); 
		}

		PMov.RB.velocity = Vector3.zero;
		PMov.CurrentSpeed = 0;
		PMov.SR.flipX = false;
		// enable the editor junk

		LevEditMain.gameObject.SetActive (true);

	}
	public void UnloadLevel()
	{
		if (TemporaryLevel != null) {
			Destroy (TemporaryLevel); 
		}
	}


	public bool InsideStringLevel;

	public void EnterStringLevel()
	{
		Global.DataHolder.InLevelEditor = false;
		// all "LevEditOBJs" must be replaced with the prefab counterparts.

		if (TemporaryLevel != null) {
			Destroy (TemporaryLevel); // This should already be destroyed when the editor is entered, but better safe than sorry.
		}
		TemporaryLevel = Instantiate (EmptyLevel,transform.position,transform.rotation,transform);

		// assemble the positions

		string IN = Super.Dataholder.LevelString;


		int p = 0;
		int i = 0;
		Global.DataHolder.EvilPoints = 0;

		while(i < Global.DataHolder.EditorPickList.Count)
		{
			int j = 0;
			while (j < Global.DataHolder.EditorPickList.Count) {
				if (Global.DataHolder.EditorPickList [j].Index == i) {
					if (IN.ToCharArray() [p] != '!' && IN.ToCharArray() [p + 1] != '!') {
						if (ReverseLut (IN.ToCharArray() [p + 1]) < 16) {
							Vector2Int position = new Vector2Int (ReverseLut (IN.ToCharArray() [p]), ReverseLut (IN.ToCharArray() [p + 1]));
							Global.DataHolder.EditorPickList [j].PlacePos = position;
							Global.DataHolder.EditorPickList [j].Placed = true;
							Global.DataHolder.EvilPoints += Global.DataHolder.EditorPickList [j].EvilPoints;
						}
					} else {
						Global.DataHolder.EditorPickList [j].Placed = false;
					}
					p += 2;
					break;
				}
				j++;
			}

			i++;
		}



		i = 0;
		while (i < EditorPickList.Count) {
			if (EditorPickList [i].Placed) {
				if (EditorPickList [i].PrefabObject != null) {
					Instantiate (EditorPickList [i].PrefabObject, new Vector3 (LevEditMain.transform.position.x + EditorPickList [i].PlacePos.x * 2 + EditorPickList [i].ObjectOffset.x * 2 + EditorPickList [i].FineObjectOffset.x, LevEditMain.transform.position.y + EditorPickList [i].PlacePos.y * 2 + EditorPickList [i].ObjectOffset.y * 2 + EditorPickList [i].FineObjectOffset.y, 0), transform.rotation, TemporaryLevel.transform);
				} else {
					if (EditorPickList [i].Name != "Chadford") {
						Debug.LogError ("EditorPickList[i] is MISSING the PrefabObject that loads when exiting the editor!");
					}
				}
			}
			i++;
		}

		// disable the editor junk

		LevEditMain.gameObject.SetActive (false);
		InsideStringLevel = true;
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

	string[] LUT = {
		"a",
		"b",
		"c",
		"d",
		"e",
		"f",
		"g",
		"h",
		"i",
		"j",
		"k",
		"l",
		"m",
		"n",
		"o",
		"p",
		"q",
		"r",
		"s",
		"t",
		"u",
		"v",
		"w",
		"x",
		"y",
		"z",
		"A",
		"B",
		"C",
		"D",
		"E",
		"F",
		"G",
		"H",
		"I",
		"J",
		"K",
		"L",
		"M",
		"N"
	};

	int ReverseLut(char IN)
	{
		string n = "" + IN;
		switch (n) {
		case "a": return 0;
		case "b": return 1;
		case "c": return 2;
		case "d": return 3;
		case "e": return 4;
		case "f": return 5;
		case "g": return 6;
		case "h": return 7;
		case "i": return 8;
		case "j": return 9;
		case "k": return 10;
		case "l": return 11;
		case "m": return 12;
		case "n": return 13;
		case "o": return 14;
		case "p": return 15;
		case "q": return 16;
		case "r": return 17;
		case "s": return 18;
		case "t": return 19;
		case "u": return 20;
		case "v": return 21;
		case "w": return 22;
		case "x": return 23;
		case "y": return 24;
		case "z": return 25;
		case "A": return 26;
		case "B": return 27;
		case "C": return 28;
		case "D": return 29;
		case "E": return 30;
		case "F": return 31;
		case "G": return 32;
		case "H": return 33;
		case "I": return 34;
		case "J": return 35;
		case "K": return 36;
		case "L": return 37;
		case "M": return 38;
		case "N": return 39;
		default:
			print ("HUUUUGE ERROR");
			return 0;
		}
	}

}
