using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

	public bool IsPaused;
	public GameObject Menu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Global.Dataholder.Pmov.InCutscene && !Global.Dataholder.PostBoomDialogue) {
			IsPaused = false;
			Menu.SetActive (IsPaused);

			Cursor.lockState = (IsPaused || Global.Dataholder.Pmov.InCutscene || Global.Dataholder.OMenu.OnScreen) ? CursorLockMode.None : CursorLockMode.Confined;
			Cursor.visible = (IsPaused || Global.Dataholder.Pmov.InCutscene || Global.Dataholder.OMenu.OnScreen);

			return;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {

			IsPaused = !IsPaused;

			Menu.SetActive (IsPaused);

		}
		Cursor.lockState = (IsPaused || Global.Dataholder.Pmov.InCutscene || Global.Dataholder.OMenu.OnScreen) ? CursorLockMode.None : CursorLockMode.Confined;
		Cursor.visible = (IsPaused || Global.Dataholder.Pmov.InCutscene || Global.Dataholder.OMenu.OnScreen);

	}
}
