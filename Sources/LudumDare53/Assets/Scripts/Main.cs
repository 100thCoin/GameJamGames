using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMain
{
	public static Main M;
}


public class Main : MonoBehaviour {

	public float MusicVolume;
	public float SFXVolume;
	public float VAVolume;

	public bool DisableSubtitles;

	public float MusicMult = 1;
	public float VAMult = 1;

	public GameObject TheFullGameToClone;
	public GameObject FullGameLoaded;

	void Awake()
	{
		SuperMain.M = this;
		if (Screen.currentResolution.width <= 768) {
			Screen.SetResolution (768, 512,true);
		}
		if (Screen.currentResolution.width > 768 && Screen.currentResolution.width <= 768*2) {
			Screen.SetResolution (768*2, 512*2,true);
		}
		if (Screen.currentResolution.width > 768*2 && Screen.currentResolution.width <= 768*3) {
			Screen.SetResolution (768*3, 512*3,true);
		}
		if (Screen.currentResolution.width > 768*3 && Screen.currentResolution.width <= 768*4) {
			Screen.SetResolution (768*4, 512*4,true);
		}

		FullGameLoaded = Instantiate (TheFullGameToClone, new Vector3 (0, 0, 0), transform.rotation);
		FullGameLoaded.SetActive (true);
		FullGameLoaded.GetComponent<DataHolder> ().enabled = true;
	}
	void OnEnabled()
	{
		SuperMain.M = this;
	}
	void OnEnable()
	{
		SuperMain.M = this;
	}

	public void LoadTitle()
	{
		Destroy (FullGameLoaded);
		FullGameLoaded = Instantiate (TheFullGameToClone, new Vector3 (0, 0, 0), transform.rotation);
		FullGameLoaded.SetActive (true);
		FullGameLoaded.GetComponent<DataHolder> ().enabled = true;
	}

}
