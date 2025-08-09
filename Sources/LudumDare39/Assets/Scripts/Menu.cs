using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	public bool Quit;

	public bool Tutorial;

	public bool Play;

	public bool resume;

	public bool PauseTutorial;

	public bool Volume;
	public bool VolumeUp;
	public bool VolumeDown;

	public bool Graphics;
	public bool GraphicsUp;
	public bool GraphicsDown;

	public bool Credits;

	public bool QuitToMainMenu;

	public bool GoToMainMenu;
	public bool GoToPauseMenu;

	public bool QuitToMainMenuDED;

	public GameObject InGame;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver()
	{
		if (Input.GetKeyDown (KeyCode.Mouse0)) {

			if (Play) {

				Instantiate (InGame, new Vector3 (0, 0, 0), GameObject.Find("InGameRef").gameObject.transform.rotation,GameObject.Find("Main").transform);
				GameObject.Find ("Pause").gameObject.SetActive (false);
				GameObject.Find("Main").GetComponent<DataHolder>().InGame = true;
				GameObject.Find("Main").GetComponent<DataHolder>().Paused = false;
				GameObject.Find("Main").GetComponent<DataHolder>().Room = 0;
				GameObject.Find("Main").GetComponent<DataHolder>().Level = 20;
				GameObject.Find("Main").GetComponent<DataHolder>().REQExp = 10;
				GameObject.Find("Main").GetComponent<DataHolder>().Experience = 0;
				GameObject.Find("Main").GetComponent<DataHolder>().UnboostedDamage = 25;



			}


			if (VolumeUp) {
				if (GameObject.Find("Main").GetComponent<DataHolder>().Volume < 1) {
					GameObject.Find("Main").GetComponent<DataHolder>().MenuVol = GameObject.Find("Main").GetComponent<DataHolder>().MenuVol + 1;
				}

			}
			if (VolumeDown) {
				if (GameObject.Find("Main").GetComponent<DataHolder>().Volume > 0) {
					GameObject.Find("Main").GetComponent<DataHolder>().MenuVol = GameObject.Find("Main").GetComponent<DataHolder>().MenuVol - 1;
				}

			}

			if (GoToPauseMenu) {

				GameObject.Find ("PauseCamera").gameObject.transform.position = new Vector3 (0, 32,-10);

			}

			if (QuitToMainMenu) {

				GameObject.Find ("PauseCamera").gameObject.transform.position = new Vector3 (0, 0,-10);
				Destroy(GameObject.Find("InGame(Clone)"));
				GameObject.Find("Main").GetComponent<DataHolder>().InGame = false;

			}


			if (QuitToMainMenuDED) {
				GameObject.Find ("Main").GetComponent<DataHolder> ().Paused = true;

				GameObject.Find("Main").gameObject.transform.FindChild("Pause").gameObject.SetActive (true);


				GameObject.Find ("PauseCamera").gameObject.transform.position = new Vector3 (0, 0,-10);
				Destroy(GameObject.Find("InGame(Clone)"));
				GameObject.Find("Main").GetComponent<DataHolder>().InGame = false;
				Destroy(gameObject);

			}
			if (GoToMainMenu) {

				GameObject.Find ("PauseCamera").gameObject.transform.position = new Vector3 (0, 0,-10);

			}


			if (PauseTutorial) {

				GameObject.Find ("PauseCamera").gameObject.transform.position = new Vector3 (48, 16,-10);


			}

			if (Volume) {

				GameObject.Find ("PauseCamera").gameObject.transform.position = new Vector3 (16, 16,-10);


			}

			if (Graphics) {

				GameObject.Find ("PauseCamera").gameObject.transform.position = new Vector3 (32, 16,-10);


			}

			if (Credits) {

				GameObject.Find ("PauseCamera").gameObject.transform.position = new Vector3 (0, 16,-10);


			}
			if (Tutorial) {

				GameObject.Find ("PauseCamera").gameObject.transform.position = new Vector3 (16, 0,-10);


			}
			if (GoToMainMenu) {

				GameObject.Find ("PauseCamera").gameObject.transform.position = new Vector3 (0, 0,-10);


			}
			if (resume) {
				GameObject.Find ("Pause").gameObject.SetActive (false);

				GameObject.Find("Main").gameObject.transform.FindChild ("InGame(Clone)").gameObject.SetActive (true);
				GameObject.Find("Main").GetComponent<DataHolder>().Paused = false;

			}


			if (Quit) {
				Application.Quit();
			}

			if (GraphicsUp) {

				GameObject.Find ("Main").GetComponent<DataHolder> ().Graphics = GameObject.Find ("Main").GetComponent<DataHolder> ().Graphics + 0.2f;

			}
			if (GraphicsDown) {

				GameObject.Find ("Main").GetComponent<DataHolder> ().Graphics = GameObject.Find ("Main").GetComponent<DataHolder> ().Graphics - 0.2f;

			}

		}

	}





}
