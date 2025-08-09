using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolume : MonoBehaviour {


	public AudioSource AS;
	public float Mult;

	// Use this for initialization
	void OnEnable () {

		AS.volume = GameObject.Find("Main").GetComponent<DataHolder>().Volume * Mult;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
