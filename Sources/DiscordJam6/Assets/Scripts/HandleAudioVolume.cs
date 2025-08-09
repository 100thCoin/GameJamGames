using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAudioVolume : MonoBehaviour {

	public AudioSource AS;
	public bool SFX;
	public bool Music;

	public bool evenLess;
	// Use this for initialization
	void OnEnable () {
		if(evenLess)
		{
			AS.volume = GameObject.Find("Main").GetComponent<Dataholder>().AudioVolume * 0.125f;
		}
		else
		{
			AS.volume = SFX ? GameObject.Find("Main").GetComponent<Dataholder>().AudioVolume * 0.25f : GameObject.Find("Main").GetComponent<Dataholder>().AudioVolume;
		}
	}

	void Update()
	{
		if(Music)
		{
			AS.volume = SFX ? GameObject.Find("Main").GetComponent<Dataholder>().AudioVolume * 0.25f : GameObject.Find("Main").GetComponent<Dataholder>().AudioVolume;
		}
	}

}
