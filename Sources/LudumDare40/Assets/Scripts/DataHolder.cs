using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour {

	public bool NopMoving;
	public bool WinOnce;
	public bool Win;
	public GameObject TrueRadical;

	public GameObject Radical;
	public GameObject NotRadical;

	public int CurrentPloogies;

	public int DeadPloogies;
	public int PreviosDeadPloogies;
	public int LeftBehindPloogies;
	public int TotalDeadPloogies;
	public int RemainingPloogies;
	public bool LoadNext;
	public GameObject Level1;
	public bool Retry;

	public GameObject Level2;
	public GameObject Level3;
	public GameObject Level4;
	public GameObject Level5;
	public GameObject Level6;
	public GameObject Level7;
	public GameObject Level8;
	public GameObject Level9;
	public GameObject Level10;

	public int LostTimes;

	public GameObject CurrentLevel;

	public bool NextReadyReady;

	public bool StartGame;
	public bool StartedGame;
	public float Level;

	public bool LoseOnce;

	public bool InGame;
	public bool Quit;

	public bool Muted;

	public bool WInning;
	public bool TrueWin;
	public bool TrueWinOnce;
	public GameObject truWinObject;

	// Use this for initialization
	void Start () {
		Screen.SetResolution (1536*2/3, 1024*2/3,false);
	}
	
	// Update is called once per frame
	void Update () {

		if (WInning) {
			NopMoving = true;



		}

		if (TrueWin && !TrueWinOnce) {

			TrueWinOnce = true;
			Vector3 Camera = GameObject.Find ("Main Camera").gameObject.transform.position;
			TrueWin = false;
			truWinObject = Instantiate (TrueRadical, new Vector3 (Camera.x, Camera.y, Camera.z + 4), gameObject.transform.rotation, gameObject.gameObject.transform);
			truWinObject.name = "Lose(Clone)";
			TrueWinOnce = false;

		}


		if (InGame) {
			gameObject.transform.Find ("game").gameObject.SetActive (true);
		} else {

			gameObject.transform.Find ("game").gameObject.SetActive (false);

		}

		if (Quit) {
			Destroy (GameObject.Find ("Lose(Clone)").gameObject);
			Destroy (CurrentLevel);
			Quit = false;
			WInning = false;
			CurrentPloogies = 50;
			LoseOnce = false;
			DeadPloogies = 0;
			PreviosDeadPloogies = 0;
		}


		if (LoadNext) {
			Win = false;
			WinOnce = false;
			NopMoving = false;
			DeadPloogies = 0;

			LoadNext = false;
		}
		if (Win && !WinOnce) {
			WinOnce = true;
			Vector3 Camera = GameObject.Find ("Main Camera").gameObject.transform.position;

			Instantiate (Radical, new Vector3 (Camera.x, Camera.y, Camera.z + 4), gameObject.transform.rotation, gameObject.gameObject.transform);

			LeftBehindPloogies = 50 - DeadPloogies - PreviosDeadPloogies - RemainingPloogies; 

			TotalDeadPloogies = DeadPloogies + PreviosDeadPloogies + LeftBehindPloogies;


		}


		if (Retry) {


			PreviosDeadPloogies = TotalDeadPloogies;
			NextReadyReady = false;
			Retry = false;
			Destroy (CurrentLevel);
			if (Level == 1) {CurrentLevel = Instantiate (Level1, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}

			if (Level == 2) {CurrentLevel = Instantiate (Level2, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			if (Level == 3) {CurrentLevel = Instantiate (Level3, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			if (Level == 4) {CurrentLevel = Instantiate (Level4, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			if (Level == 5) {CurrentLevel = Instantiate (Level5, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			if (Level == 6) {CurrentLevel = Instantiate (Level6, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			if (Level == 7) {CurrentLevel = Instantiate (Level7, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			if (Level == 8) {CurrentLevel = Instantiate (Level8, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			if (Level == 9) {CurrentLevel = Instantiate (Level9, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			if (Level == 10) {CurrentLevel = Instantiate (Level10, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}

			NopMoving = false;
			CurrentPloogies = 50;
			LoseOnce = false;
			DeadPloogies = 0;
			LoadNext = true;
			Destroy (GameObject.Find ("Lose(Clone)").gameObject);
		}




		if (Win && Input.anyKeyDown && NextReadyReady) {

			LoadNext = true;
			Win = false;
			WinOnce = false;

			PreviosDeadPloogies = TotalDeadPloogies;
			NextReadyReady = false;

			Destroy (CurrentLevel);
			Level = Level + 1;
			if (Level == 2) {CurrentLevel = Instantiate (Level2, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			if (Level == 3) {CurrentLevel = Instantiate (Level3, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			if (Level == 4) {CurrentLevel = Instantiate (Level4, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			if (Level == 5) {CurrentLevel = Instantiate (Level5, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			if (Level == 6) {CurrentLevel = Instantiate (Level6, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			if (Level == 7) {CurrentLevel = Instantiate (Level7, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			if (Level == 8) {CurrentLevel = Instantiate (Level8, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			if (Level == 9) {CurrentLevel = Instantiate (Level9, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			if (Level == 10) {CurrentLevel = Instantiate (Level10, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;}
			NopMoving = false;
			CurrentPloogies = 50;
			LoseOnce = false;
		}

		if (StartGame && !StartedGame) {
			StartedGame = true;
			StartGame = false;
			InGame = true;
		}

		if (StartedGame) {
			StartedGame = false;
			LoadNext = true;

			CurrentLevel = Instantiate (Level1, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform) as GameObject;
			Level = 1;
		}


		if (CurrentPloogies < 10 && !LoseOnce) {
			NopMoving = true;
			LoseOnce = true;
			Vector3 Camera = GameObject.Find ("Main Camera").gameObject.transform.position;
			LostTimes = LostTimes + 1;
			Instantiate(NotRadical ,new Vector3 (Camera.x, Camera.y, Camera.z + 4), gameObject.transform.rotation, gameObject.gameObject.transform);


		}



	}
}
