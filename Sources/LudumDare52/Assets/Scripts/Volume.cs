using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour {

	public bool Voice;
	AudioSource AC;
	// Use this for initialization
	void Start () {

		AC = GetComponent<AudioSource> ();
		AC.volume = Voice ? Main.Data.VOVolume : Main.Data.MusicVolume;

	}

	void Update()
	{
		AC.volume = Voice ? Main.Data.VOVolume : Main.Data.MusicVolume;
	}

}
