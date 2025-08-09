using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAudio : MonoBehaviour {

	public Cutscene Cut;
	public AudioSource AS;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		AS.volume = Super.Dataholder.Volume_Voice;
		if (Cut.Duration <= 0) {
			AS.volume *= (1 + Cut.Duration);
		}


	}
}
