using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour {

	public float Pitch;
	public float PitchMod;
	public AudioClip StepUp;
	public AudioClip StepDown;
	public AudioSource AS;

	// Use this for initialization
	void OnEnable () {

		AS.clip = Random.Range(0,2) == 0 ? StepUp : StepDown;
		Pitch = Random.Range(0.6f,1.3f);
		PitchMod = Random.Range(-0.1f,0.1f);
		AS.enabled = false;
		AS.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		Pitch += PitchMod;
		AS.pitch = Pitch;
	}
}
