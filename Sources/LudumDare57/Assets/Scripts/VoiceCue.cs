using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceCue : MonoBehaviour {

	public GameObject VoiceLine;
	public GameObject SecondaryVoiceLine;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.tag == "Player") {
			Global.Dataholder.VoiceQueue.Add (VoiceLine);
			if (SecondaryVoiceLine != null) {
				Global.Dataholder.VoiceQueue.Add (SecondaryVoiceLine);

			}
			Destroy (gameObject);
		}

	}

}
