using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSFX : MonoBehaviour {

	public AudioSource AS;
	public AudioClip[] Ac;

	// Use this for initialization
	void OnEnable () {
		int r = Random.Range (0, Ac.Length);
		AS.clip = Ac [r];
		AS.enabled = false;
		AS.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
