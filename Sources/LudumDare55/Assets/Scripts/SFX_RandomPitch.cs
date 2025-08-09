using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_RandomPitch : MonoBehaviour {

	public float Min;
	public float Max;
	public AudioSource AS;

	// Use this for initialization
	void OnEnable () {
		AS.pitch = Random.Range (Min, Max);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
