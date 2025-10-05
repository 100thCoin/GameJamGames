using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour {

	public bool SFX;
	public bool Music;
	public bool Voice;

	public AudioSource AS;

	public float ForceMultt = 1;

	// Use this for initialization
	void OnEnabled () {
		AS.volume = Super.Dataholder.Volume_Voice* ForceMultt;					

	}
	
	// Update is called once per frame
	void Update () {
		if (SFX) {
			AS.volume = Super.Dataholder.Volume_SFX* ForceMultt;						
		} else if (Music) {
			if (Global.Dataholder.CurrentVoiceClip != null) {
				AS.volume = 0;			
			} else {
				AS.volume = Super.Dataholder.Volume_Music * Super.Dataholder.MusicMultiplier * ForceMultt;			
			}
		} else if (Voice) {
			AS.volume = Super.Dataholder.Volume_Voice* ForceMultt;				

		}
	}
}
