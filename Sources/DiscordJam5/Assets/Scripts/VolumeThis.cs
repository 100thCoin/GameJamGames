using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeThis : MonoBehaviour {

	public AudioSource AS;
	public DataHolder Main;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		AS.volume = Main.Volume;
	}
}
