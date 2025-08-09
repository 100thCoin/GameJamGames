using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMusic : MonoBehaviour {

	public float Timer;

	public AudioSource aud;

	public DataHolder Main;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Timer+= Time.deltaTime;
		if(Timer >Main.Volume)
		{
			Timer = Main.Volume;
		}
		aud.volume = Timer;

	}
}
