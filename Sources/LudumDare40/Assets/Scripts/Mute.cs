using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mute : MonoBehaviour {

	public bool InGame;

	// Use this for initialization
	void Start () {


		if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Muted) {

			gameObject.GetComponent<AudioSource> ().volume = 0;

		}
			


	}
	
	// Update is called once per frame

	void Update () {
		if (!InGame) {
			if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Muted) {

				gameObject.GetComponent<AudioSource> ().volume = 0;

			} else {

				gameObject.GetComponent<AudioSource> ().volume = 1;

			}
		}
	}

	void Enable () {
		if (InGame) {
			if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Muted) {

				gameObject.GetComponent<AudioSource> ().volume = 0;

			} else {

				gameObject.GetComponent<AudioSource> ().volume = 1;

			}
		}
	}
}
