using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXVariance : MonoBehaviour {
	

	public AudioSource AS;

	// Use this for initialization
	void OnEnable () {

		AS.pitch = Random.Range(0.8f,1.2f);

	}
	

}
