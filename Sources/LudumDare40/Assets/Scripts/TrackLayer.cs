using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackLayer : MonoBehaviour {

	public bool On;
	public bool Ploogy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if (On && !GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Muted) {

			gameObject.GetComponent<AudioSource> ().volume += Time.deltaTime;

		}
		if (!On && !GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Muted) {

			gameObject.GetComponent<AudioSource> ().volume -= Time.deltaTime;

		}



		if (GameObject.Find ("Main Camera").gameObject.transform.position.x > 125) {
		
			if (!Ploogy) {
				On = false;
			}
			if (Ploogy) {
				On = true;
			}

		}
		if (GameObject.Find ("Main Camera").gameObject.transform.position.x < 125) {
			if (!Ploogy) {
				On = true;
			}
			if (Ploogy) {
				On = false;
			}		}


	}
}
