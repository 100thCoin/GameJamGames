using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXVol : MonoBehaviour {

	public AudioSource AS;

	// Use this for initialization
	void OnEnable () {
		AS = GetComponent<AudioSource>();
		AS.volume = VolumeMna.Volume;

	}
	void Update () {
		AS.volume = VolumeMna.Volume;

	}

}
