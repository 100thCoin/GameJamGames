using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceQueue : MonoBehaviour {

	public AudioSource AS;
	public float Timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Main.Data == null) {
			Destroy (gameObject);
		}

		Timer += Time.deltaTime;
		if (Timer > AS.clip.length) {
			if (Main.Data.QueuedVoiceLine != null) {
				Main.Data.CurrentVoiceLine = Instantiate (Main.Data.QueuedVoiceLine);
				Main.Data.QueuedVoiceLine = null;
			}

			Destroy (gameObject);

		}

	}
}
