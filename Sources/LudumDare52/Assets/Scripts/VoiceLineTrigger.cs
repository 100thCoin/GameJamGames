using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineTrigger : MonoBehaviour {

	public GameObject VoicePrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Player") {

			if (Main.Data.CurrentVoiceLine == null) {
				Main.Data.CurrentVoiceLine = Instantiate (VoicePrefab);
			} else {
				Main.Data.QueuedVoiceLine = VoicePrefab;
			}
			Destroy (gameObject);
		}

	}

}
