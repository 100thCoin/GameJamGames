using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {

	// just make a big unity animation and pray the audio syncs

	public float timerUntilCutsceneOver;

	public float FadeToWhite;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timerUntilCutsceneOver -= Time.deltaTime;

		if (Global.DataHolder.CutsceneOver) {
			Destroy (this);
		}

		if (timerUntilCutsceneOver < 0) {

			//okay cutscene over.
			Global.DataHolder.CutsceneOver = true;


		}

	}
}
