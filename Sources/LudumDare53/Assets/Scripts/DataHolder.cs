using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{
	public static DataHolder DataHolder;
}

public class DataHolder : MonoBehaviour {

	public GameObject[] LevelPrefabs;
	public GameObject CurrentLoadedLevel;
	public GameObject CurrentLoadedToBeUnloaded;
	public GameObject UnloadThisDuringShiftMode;
	public GameObject DontUnloadThisDuringShiftMode;


	public Vector2 SpawnPos;

	public int LevelID;

	public Camera MainCamera;
	public Camera AltCamera;
	public Camera ForceCamera;

	public bool ScreenShiftMode;
	public GameObject ScreenShiftCursor;
	public GameObject ScreenBounds;
	public Vector3 ScreenCursorOffset;
	public Vector3 ScreenCursorPos;


	public CharMovement Player;
	public SpriteRenderer PausePlayer;
	public SpriteRenderer PausePlayerW;

	public GameObject DeadMSG;
	public SpriteRenderer WhiteFlash;
	public float FlashTimer;
	public bool Flashing;

	public Transform VAHolder;
	public Subtitles SubtitleManager;
	public TVHolder TVHold;

	public CoolTransition Trans;
	public GameObject CurrentVoiceLine;

	public GameObject SFX_MailChute;
	public GameObject SFX_OpenChute;

	public GameObject VA_WellTerryYouMadeIt;
	public GameObject VA_Incoherent;

	public AudioSource MainMusic;
	public AudioClip YoureProbablyGonnaFight;

	public SmarticusBossManager BossMan;

	public ScreenShiftCursor BossCursor;

	public bool WinGame;
	public float WinTimer;

	public bool GameIsPaused;
	public GameObject PauseMenu;

	public GameObject SFX_VictoryChime;

	public bool InGame;
	public float SpeedrunTime;

	public int ResetCount;

	public void BeginBossMusic()
	{
		MainMusic.clip = YoureProbablyGonnaFight;
		MainMusic.enabled = false;
		MainMusic.enabled = true;
	}

	public void RemoveCurrentVoiceLine()
	{
		if (CurrentVoiceLine != null) {
			Destroy (CurrentVoiceLine);
		}
		SubtitleManager.Clear ();
	}

	public void ActivateScreenShiftMode()
	{
		ScreenShiftMode = true;
		UnloadThisDuringShiftMode.SetActive (false);
		PausePlayer.sprite = Player.SR.sprite;
		PausePlayer.transform.position = Player.transform.position;
		PausePlayer.flipX = Player.SR.flipX;
		ScreenCursorOffset = ScreenBounds.transform.position - ScreenShiftCursor.transform.position;
		PausePlayerW.sprite = Player.WSR.sprite;
		PausePlayerW.flipX = Player.WSR.flipX;

	}

	public void ActivateScreenShiftModeBossVersion()
	{
		ScreenShiftMode = true;
		UnloadThisDuringShiftMode.SetActive (false);
		PausePlayer.sprite = Player.SR.sprite;
		PausePlayer.transform.position = Player.transform.position;
		PausePlayer.flipX = Player.SR.flipX;
		//ScreenCursorOffset = ScreenBounds.transform.position - ScreenShiftCursor.transform.position;
		PausePlayerW.sprite = Player.WSR.sprite;
		PausePlayerW.flipX = Player.WSR.flipX;
		BossCursor.gameObject.SetActive (true);
		BossCursor.transform.position = PausePlayerW.transform.position;
		BossCursor.BossFightTimer = 0;
	}

	public void DeactivateScreenShiftMode()
	{
		ScreenShiftMode = false;
		UnloadThisDuringShiftMode.SetActive (true);
		PausePlayer.sprite = null;
		PausePlayerW.sprite = null;
		BossCursor.gameObject.SetActive (false); //even if it wasnt boss mdoe

	}

	public void UpdateScreenBounds ()
	{

		ScreenBounds.transform.position = ScreenShiftCursor.transform.position + ScreenCursorOffset;
		ScreenBounds.transform.position = new Vector3 (ScreenBounds.transform.position.x, ScreenBounds.transform.position.y, 0);
	}

	public LevelManager TransitionMan;

	public void NextLevel()
	{
		Player.transform.position = new Vector3 (0, 0, -600);
		Player.EnableCollision ();
		LevelID++;
		TransitionMan = Instantiate (LevelPrefabs [LevelID], new Vector3 (0, -200, 0), transform.rotation, transform).GetComponent<LevelManager> ();
		TransitionMan.LevelBounds.GetComponent<BoundMatManagerForTransition> ().AssignNextLevelMat ();
		Trans.gameObject.SetActive (true);
		Trans.Active = true;
		Trans.Timer = 0;
	}

	public void FinalizeRoomTransition()
	{
		Destroy (CurrentLoadedLevel);
		Destroy (CurrentLoadedToBeUnloaded);
		TVHold.ReplaceTV (TransitionMan.TVId, TransitionMan.TV);
		TVHold.TV.transform.position += new Vector3 (0, 200, 0);
		TransitionMan.Entrance.SpawnPlayer = true;
		TransitionMan.Happen ();
		CurrentLoadedLevel.transform.position = new Vector3 (0, 0, 0);
		CurrentLoadedToBeUnloaded.transform.position = new Vector3 (0, 0, 0);
		Player.gameObject.SetActive (true);
		Player.transform.position = SpawnPos;
		//Player.CutsceneLock = false;
		//Player.SR.sortingOrder =2;
		//Player.WSR.sortingOrder = -1;
		Player.EnableCollision ();
		Player.RB.isKinematic = false;
		Player.CurrentSpeed = 0;
		Player.LevelStart = true;
		if (Player.CurrentBomb != null) {
			Destroy (Player.CurrentBomb);
		}
	}

	public bool QueueBombSFX;
	public GameObject BombSFX;

	void Awake()
	{
		Global.DataHolder = this;
	}
	void OnEnabled()
	{
		Global.DataHolder = this;
	}
	void OnEnable()
	{
		Global.DataHolder = this;
	}
	// Use this for initialization
	void Start () {
		
	}

	public GameObject Title;
	public GameObject GameHolder;
	public bool Leavingtitle;
	public float LeavingTitleTimer;

	public void BeginGame()
	{
		Leavingtitle = true;
		LeavingTitleTimer = 0;

	}

	public void BeginVictorySequence()
	{
		WinGame = true;
		PauseBossMusicBecauseYouWon ();

	}

	public void PauseBossMusicBecauseYouWon()
	{
		MainMusic.pitch = 0;

	}

	public void resumeBossMusic()
	{
		MainMusic.pitch = 1;


	}

	public void StartFlash()
	{
		FlashTimer = 1;
		WhiteFlash.color = new Vector4 (0, 0, 0, FlashTimer);
		Flashing = true;
	}

	public void ResetRoom()
	{
		if (LevelID == 7) {
			return;
		}


		Destroy (CurrentLoadedLevel);
		Destroy (CurrentLoadedToBeUnloaded);
		LevelManager LM = Instantiate (LevelPrefabs [LevelID], new Vector3 (0, 0, 0), transform.rotation, transform).GetComponent<LevelManager>();
		TVHold.ReplaceTV (LM.TVId, LM.TV);
		LM.Happen ();
		Destroy (LM.gameObject);
		ResetCount++;
		Player.SR.sortingOrder = 2;
		Player.WSR.sortingOrder = -1;
		Player.CutsceneLock = false;
		DeactivateScreenShiftMode ();
	}


	public bool VictorySFXOnce;
	public GameObject VictoryScreen;
	public TextMesh VictoryTM;

	public SpriteRenderer FadeToBlackFromTitle;
	public bool GameLoaded;
	public bool InitGame;
	// Update is called once per frame
	void Update () {

		if (Leavingtitle) {
			LeavingTitleTimer += Time.deltaTime;
			if (LeavingTitleTimer < 1) {
				FadeToBlackFromTitle.color = new Vector4 (0, 0, 0, LeavingTitleTimer);
				SuperMain.M.MusicMult = 1 - LeavingTitleTimer;

			}
			if (LeavingTitleTimer > 1 && LeavingTitleTimer < 2) {
				FadeToBlackFromTitle.color = new Vector4 (0, 0, 0, 2 - LeavingTitleTimer);
				SuperMain.M.MusicMult = -1 + LeavingTitleTimer;

				if (!GameLoaded) {
					Title.SetActive (false);
					GameHolder.SetActive (true);
				}
			}
			if (LeavingTitleTimer > 2) {
				SuperMain.M.MusicMult = 1;
				Leavingtitle = false;
				InGame = true;
				FadeToBlackFromTitle.color = new Vector4 (0, 0, 0, 0);

			}


			return;
		} else {
			if (!InitGame) {

			}

		}



		if(Flashing)
		{
			FlashTimer -= Time.deltaTime * 4;
			WhiteFlash.color = new Vector4 (0,0,0, FlashTimer);
			if (FlashTimer < 0) {
				Flashing = false;
			}
		}

		if (WinGame) {

			WinTimer += Time.deltaTime;

			if (WinTimer > 1.5f) {
				if (!VictorySFXOnce) {
					VictorySFXOnce = true;
					Instantiate (SFX_VictoryChime);
				}

			}
			if (WinTimer > 2.7f) {

				VictoryScreen.SetActive (true);
				VictoryTM.text = "Speedrun Time:\n" + StringifyTime (SpeedrunTime) + "\n\nResets: " + ResetCount;

			}

			return;
		}

		if (InGame) {
			SpeedrunTime += Time.deltaTime;
		}


		if (Input.GetKeyDown (KeyCode.Escape) && !WinGame) {

			GameIsPaused = !GameIsPaused;
			PauseMenu.SetActive (GameIsPaused);
		}


		if (QueueBombSFX) {
			QueueBombSFX = false;
			Instantiate (BombSFX);
		}

	}




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


}
