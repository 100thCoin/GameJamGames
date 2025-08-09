using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundeffectHaltMusic : MonoBehaviour {

	public AudioSource AS;
	public float Timer;
	// Use this for initialization
	void Start () {
	
		AS = GameObject.Find("Main").GetComponent<DataHolder>().Music;
		//AS.volume *= 0.5f;
	}
	
	// Update is called once per frame
	void Update () {

		Timer += Time.deltaTime;
		if(Timer >= 1)
		{
			//AS.volume *= 2;
			Destroy(gameObject);
		}

	}
}
