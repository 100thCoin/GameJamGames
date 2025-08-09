using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class DataHolder : MonoBehaviour {

	public float PlayerDamage;

	public float UnboostedDamage;


	public float Experience;
	public float REQExp;
	public float Level;

	public GameObject LVLDown;


	public GameObject ARoom1;
	public GameObject ARoom2;

	public GameObject URoom1;
	public GameObject URoom2;
	public GameObject URoom3;
	public GameObject URoom4;
	public GameObject URoom5;
	public GameObject URoom6;
	public GameObject URoom7;

	public GameObject RRoom1;
	public GameObject RRoom2;
	public GameObject RRoom3;
	public GameObject RRoom4;


	public GameObject roomText;
	public float Room;

	public float TotalDamage;
	public float TotalDefense;
	public float TotalDrain;

	public GameObject TotalText;

	public bool TwoOnlyOnce;

	public bool InGame;

	public float Volume;

	public float MenuVol;

	public float Graphics;

	public bool Paused;
	public bool PauseBridge;

	public GameObject Music;
	public GameObject SecondMusic;

	public GameObject Camera;
	public GameObject SecondCamera;

	// Use this for initialization
	void Start () {

		Screen.SetResolution (776, 640, false);

	}
	
	// Update is called once per frame
	void Update () {

		if (Graphics < 0) {
			Graphics = 0;
		}
		if (Graphics > 2) {
			Graphics = 2;
		}



		if (!Paused) {

			Music = GameObject.Find ("Ld39_1 (1)").gameObject;
			Music.gameObject.GetComponent<AudioSource> ().volume = Volume;
			SecondMusic.gameObject.GetComponent<AudioSource> ().volume = 0;

			Camera = GameObject.Find ("Main Camera").gameObject;
			Camera.gameObject.GetComponent<BloomOptimized> ().intensity = Graphics;
			SecondCamera.gameObject.GetComponent<BloomOptimized> ().intensity = 0;
		}


		if (Input.GetKeyDown (KeyCode.Escape)) {

			if (!Paused && !PauseBridge) {


				Paused = true;
				GameObject.Find ("InGame(Clone)").gameObject.SetActive (false);
				gameObject.transform.FindChild ("Pause").gameObject.SetActive (true);
				GameObject.Find ("PauseCamera").gameObject.transform.position = new Vector3 (0, 32,-10);

			}





		}






		if(MenuVol == 0){Volume = 0;}
		if(MenuVol == 1){Volume = 0.1f;}
		if(MenuVol == 2){Volume = 0.2f;}
		if(MenuVol == 3){Volume = 0.3f;}
		if(MenuVol == 4){Volume = 0.4f;}
		if(MenuVol == 5){Volume = 0.5f;}
		if(MenuVol == 6){Volume = 0.6f;}
		if(MenuVol == 7){Volume = 0.7f;}
		if(MenuVol == 8){Volume = 0.8f;}
		if(MenuVol == 9){Volume = 0.9f;}
		if(MenuVol == 10){Volume = 1;}




		if (InGame && !Paused) {
//			if (Room == 2 && !TwoOnlyOnce) {
//				Room = 1;
//				TwoOnlyOnce = true;
//			}

	
			TotalDefense = Mathf.Round (100 * (1 / GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().DefenseReduction) * (Level * 0.1f));
			TotalDamage = Mathf.Round (PlayerDamage) * 8;
			TotalDrain = Mathf.Round ((25 - GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().ManaDrainReduce) * GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().ManaDrainMult * GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().ManaDrainMore);

			TotalText = GameObject.Find ("New Text (3)").gameObject;
			roomText = GameObject.Find ("New Text (2)").gameObject;

			TotalText.gameObject.GetComponent<TextMesh> ().text = "DMG: " + TotalDamage + "\nDEF: " + TotalDefense + "\nUSG: " + TotalDrain;


			roomText.gameObject.GetComponent<TextMesh> ().text = "Room " + Room;

			PlayerDamage = UnboostedDamage * GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().DamageBoost * GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().DamageMultiplier * GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().DamageMore;

			if (Experience > REQExp) {
				StartCoroutine (LevelDown ());
				Experience = 0;
			}
		}
	}

	IEnumerator LevelDown()
	{
		yield return new WaitForEndOfFrame ();
		Experience = 0;
		REQExp = (REQExp + 50) * 1.5f;

		Level = Level - 1;

		//10
		//121
		//243
		//377.3
		//525.03
		//744.733
		//929
		//1132
		//etc
		UnboostedDamage = UnboostedDamage *0.85f;
		Instantiate (LVLDown, GameObject.Find ("Proto_Idle_1").gameObject.transform.position, gameObject.transform.rotation);

	}

	void LateUpdate()
	{
		if (Paused) {

			SecondMusic.gameObject.GetComponent<AudioSource> ().volume = Volume;
			SecondCamera.gameObject.GetComponent<BloomOptimized> ().intensity = Graphics;
		}

	}
}
