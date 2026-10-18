using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutscene : MonoBehaviour {

	public float Timer;

	public GameObject VictoryScreen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Global.Dataholder.WonGame) {
			Timer += Time.deltaTime;
		}

		if (Timer > 22f || ( Global.Dataholder.WonGame && (Input.GetKeyDown(KeyCode.Mouse0) && Super.Dataholder.PreviousVictory_CanSkipCutscenes))) {
			VictoryScreen.SetActive (true);
			Global.Dataholder.DialogueSource.clip = null;
		}

	}
}
