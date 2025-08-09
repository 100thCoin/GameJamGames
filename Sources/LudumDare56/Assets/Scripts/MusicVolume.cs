using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolume : MonoBehaviour {


	public bool Singalong;
	public AudioSource AS;
	public bool Boss;
	public float BossTimer;
	public bool title;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (title) {

			AS.volume = Super.Dataholder.Volume_Music * Super.Dataholder.MusicMultiplier;

			return;
		}

		if (!Boss ) {
			if (Global.Dataholder.LevelIndex != 9) {
				BossTimer += Time.deltaTime;
			} else {
				BossTimer -= Time.deltaTime;
			}
			BossTimer = Mathf.Clamp01 (BossTimer);
		} else {
			if (Global.Dataholder.LevelIndex != 9) {
				BossTimer -= Time.deltaTime;
			} else {
				BossTimer += Time.deltaTime;
			}
			BossTimer = Mathf.Clamp01 (BossTimer);
		}
		if (!Singalong) {
			AS.volume = Super.Dataholder.Volume_Music * Super.Dataholder.MusicMultiplier * BossTimer;
		} else {
			AS.volume = Super.Dataholder.Volume_Music * Global.Dataholder.SingalongVolume * Super.Dataholder.MusicMultiplier * BossTimer;

		}

	}
}
