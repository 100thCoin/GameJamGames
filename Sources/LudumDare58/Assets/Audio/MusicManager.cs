using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioSource AS;

	public bool Streets;
	public bool Acapella;
	public bool Other;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		int HaltMusic = 1;

		if (Global.Dataholder.CurrentVoiceClip != null || Global.Dataholder.PlayerDead) {
			HaltMusic = 0;
		}


		if (Streets) {
			if ((Global.Dataholder.MainCamera.transform.position.y > -5)) {
				HaltMusic = 0;

			}
		}
		if (Acapella) {
			if (!(Global.Dataholder.MainCamera.transform.position.x == 30 && Global.Dataholder.MainCamera.transform.position.y == 0)) {
				HaltMusic = 0;
			}
		}
		if (Other) {
			if ((Global.Dataholder.MainCamera.transform.position.y < -5) || (Global.Dataholder.MainCamera.transform.position.x == 30 && Global.Dataholder.MainCamera.transform.position.y == 0)) {
				HaltMusic = 0;
			}
		}

		AS.volume = Super.Dataholder.Volume_Music * Super.Dataholder.MusicMultiplier * HaltMusic;			


	}
}
