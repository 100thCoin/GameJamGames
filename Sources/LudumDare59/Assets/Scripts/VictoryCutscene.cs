using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCutscene : MonoBehaviour {

	public float Timer;
	public GameObject VictoryScreen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Super.Dataholder.PreviousVictory_CanSkipCutscenes && Input.GetKeyDown (KeyCode.Mouse0)) {
			Timer = 99;
		}

		Timer += Time.deltaTime;
		if (Timer > 16) {
			Super.Dataholder.PreviousVictory_CanSkipCutscenes = true;
			Global.Dataholder.WonGame = true;

			VictoryScreen.SetActive (true);
			gameObject.SetActive (false);
		}
	}
}
