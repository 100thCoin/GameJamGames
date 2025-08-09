using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXVariety : MonoBehaviour {

	public float Low;
	public float High;
	public AudioSource AS;

	// Use this for initialization
	void Awake () {
		AS.pitch = Random.Range (Low, High);
	}
	[ContextMenu("Set Audio Source")]
	void SetAS()
	{
		AS = GetComponent<AudioSource> ();
	}
	// Update is called once per frame
	void Update () {
		
	}
}
