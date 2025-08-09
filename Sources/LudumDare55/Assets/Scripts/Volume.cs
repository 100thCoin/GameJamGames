using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour {

	public AudioSource AS;
	public bool VA;
	public bool SFX;
	public bool Music;
	public bool DoGlobalFade;

	void Start () {
		if (VA) {
			AS.volume = Super.Dataholder.Volume_Voice;
		}
		if (SFX) {
			AS.volume = Super.Dataholder.Volume_SFX;
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
			AS.volume = Super.Dataholder.Volume_SFX;
		}
		if (Music) {
			if (!DoGlobalFade) {
				AS.volume = Super.Dataholder.Volume_Music; 

			} else {
				AS.volume = Super.Dataholder.Volume_Music * Super.Dataholder.MusicMultiplier;

			}
		}
	}
}
