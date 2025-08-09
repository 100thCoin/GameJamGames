using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolume : MonoBehaviour {

	public AudioSource AS;
	public bool SFX;
	public bool Music;
	public bool VA;

	// Use this for initialization
	void Start () {
		if (SFX) {
			AS.volume = SuperMain.M.SFXVolume * SuperMain.M.MusicMult;
		} else if (Music) {
			AS.volume = SuperMain.M.MusicVolume * SuperMain.M.MusicMult;
		} else if (VA) {
			AS.volume = SuperMain.M.VAVolume * SuperMain.M.VAMult;
		}

	}

	[ContextMenu("Set Audio Source")]
	void SetAS()
	{
		AS = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		if (SFX) {
			AS.volume = SuperMain.M.SFXVolume * SuperMain.M.MusicMult;
		} else if (Music) {
			AS.volume = SuperMain.M.MusicVolume * SuperMain.M.MusicMult;
		} else if (VA) {
			AS.volume = SuperMain.M.VAVolume * SuperMain.M.VAMult;
		}
	}
}
