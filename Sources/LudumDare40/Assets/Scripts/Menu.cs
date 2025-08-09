using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	public bool Play;
	public bool Creds;
	public bool Quit;
	public bool Story;
	public bool SuperSecret;
	public bool SecretUnlocked;

	public bool ToPrologue2;
	public bool ToPrologue3;
	public bool ActuallyStart;

	public bool TryAgain;
	public bool QuitToMenu;

	public bool BacktoMain;

	public bool Mute;
	public bool Muted;
	public bool MuteBridge;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().LostTimes >= 14) {
			SecretUnlocked = true;

		}



	}

	void OnMouseOver()
	{
		if(Input.GetKeyDown(KeyCode.Mouse0))
			{
			if (BacktoMain) {

				GameObject.Find ("Menu Camera").gameObject.transform.position = new Vector3 (0, 40, -10);

			}
			if (Story) {

				GameObject.Find ("Menu Camera").gameObject.transform.position = new Vector3 (0, 64, -10);

			}
			if (Creds) {

				GameObject.Find ("Menu Camera").gameObject.transform.position = new Vector3 (0, 88, -10);

			}
			if (SuperSecret && SecretUnlocked) {

				GameObject.Find ("Menu Camera").gameObject.transform.position = new Vector3 (0, 112, -10);

			}

			if (Play) {

				GameObject.Find ("Menu Camera").gameObject.transform.position = new Vector3 (0, 136, -10);

			}

			if (ToPrologue2) {

				GameObject.Find ("Menu Camera").gameObject.transform.position = new Vector3 (0, 160, -10);

			}

			if (ToPrologue3) {

				GameObject.Find ("Menu Camera").gameObject.transform.position = new Vector3 (0, 184, -10);

			}

			if (ActuallyStart) {

				GameObject.Find ("Menu Camera").gameObject.SetActive (false);

				GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().StartGame = true;

			}

			if (TryAgain) 
			{
				GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Retry = true;

			}

			if (QuitToMenu) {

				GameObject.Find("Menu").gameObject.transform.Find ("Menu Camera").gameObject.SetActive (true);
				GameObject.Find ("Menu Camera").gameObject.transform.position = new Vector3 (0, 40, -10);
				GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Quit = true;

				GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().InGame = false;

			}

			if (Quit) {

				Application.Quit ();

			}
			if (Mute) {

				if (Muted) {

					//bridge
					MuteBridge = true;
				}


				if (!Muted) {
					Muted = true;
					GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Muted = true;

				}

				if (MuteBridge) {
					MuteBridge = false;
					Muted = false;

					GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Muted = false;

				}


			}


			}



	}







}
