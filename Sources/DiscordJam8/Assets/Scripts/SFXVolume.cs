using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXVolume : MonoBehaviour {


	public AudioSource AS;
	public bool VA;
	public bool SFX;
	public bool Music;
	public bool DoGlobalFade;

	public float ArbitaryMultiplier = 1;

	public bool QuietWhenTalking;
	public int TrackLayer;

	void Start () {
		if (VA) {
			AS.volume = Super.Dataholder.Volume_Voice;
		}
		if (SFX) {
			AS.volume = Super.Dataholder.Volume_SFX * ArbitaryMultiplier;
		}
		if (Music) {
			AS.volume = Super.Dataholder.Volume_Voice;
		}
	}
	// Update is called once per frame
	void Update () {

		if (VA) {
			AS.volume = Super.Dataholder.Volume_Voice;
		}
		if (SFX) {
			AS.volume = Super.Dataholder.Volume_SFX * ArbitaryMultiplier;
		}
		if (Music) {
			if (!DoGlobalFade) {
				AS.volume = Super.Dataholder.Volume_Music; 

			} else {
				AS.volume = Super.Dataholder.Volume_Music * Super.Dataholder.MusicMultiplier;

			}
		}

		if (TrackLayer == 1 || TrackLayer == 2) {
			if (TrackLayer == 1) {
				AS.volume *= Global.DataHolder.InEditorMusicShifter;
			} else {
				AS.volume *= (1-Global.DataHolder.InEditorMusicShifter);
			}

		}

		if (QuietWhenTalking) {
			if (Global.DataHolder.VQ.CurrentLoaded != null) {
				AS.volume *= 0.33f;
			}


		}
	}
}
