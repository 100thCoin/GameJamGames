using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitchDK : MonoBehaviour {

	public float Vol;
	public bool On;
	public bool Off;
	public bool SuperOff;

	public bool OffOnce;
	public bool OffOnceButSuper;

	public GameObject DK;
	public GameObject NotDK;
	public GameObject Cam;
	public GameObject SplashPlayer;
	public AudioClip FreeFallin;
	public AudioClip Splash;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



		if(Cam.gameObject.GetComponent<CameraMover>().Event25M)
		{
			On = true;
		}
		else
		{
			On = false;
		}
		if(Cam.gameObject.GetComponent<CameraMover>().EventFreefall)
		{
			Off = true;
		}
		if(OffOnce)
		{
			if(SplashPlayer.GetComponent<PlayerMovement>().PerfectDive)
			{
				SuperOff = true;
			}
		}
		if(Off && !OffOnce)
		{
			OffOnce = true;
			NotDK.gameObject.GetComponent<AudioSource>().volume = 1;

			NotDK.gameObject.GetComponent<AudioSource>().clip = FreeFallin;
			NotDK.gameObject.SetActive(false);
			NotDK.gameObject.SetActive(true);
			NotDK.gameObject.GetComponent<AudioSource>().loop = false;

		}		
		if(SuperOff && !OffOnceButSuper)
		{
			OffOnceButSuper = true;
			NotDK.gameObject.GetComponent<AudioSource>().volume = 1;

			NotDK.gameObject.GetComponent<AudioSource>().clip = Splash;
			NotDK.gameObject.SetActive(false);
			NotDK.gameObject.SetActive(true);
			NotDK.gameObject.GetComponent<AudioSource>().loop = false;

		}


		if(On)
		{
			Vol += Time.deltaTime;
			if(Vol >= 1)
			{
				Vol =1;
			}


		}
		else
		{
			Vol -= Time.deltaTime;
			if(Vol <= 0)
			{
				Vol =0;
			}
		}

		DK.gameObject.GetComponent<AudioSource>().volume = Vol;
		NotDK.gameObject.GetComponent<AudioSource>().volume = 1 - Vol;

	}
}
