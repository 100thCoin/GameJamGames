using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour {

	public bool SFX;
	public AudioSource AS;

	public bool HouseMusic;
	public bool Acapella;

	void OnEnable()
	{
		
	}

	// Use this for initialization
	void Start () {
		if (SFX) {
			AS.volume = Global.Dataholder.SFXVolume;
		} else {
			if (HouseMusic) {
				AS.volume = (Global.Dataholder.MusicVolume * Global.Dataholder.MusicMultiplier * Global.Dataholder.HouseMusicMult);

			} else {
				AS.volume = (Global.Dataholder.MusicVolume * Global.Dataholder.MusicMultiplier * (1-Global.Dataholder.HouseMusicMult));
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (SFX) {
			AS.volume = Global.Dataholder.SFXVolume;
		} else {
			if (HouseMusic) {

				bool oneHundred = false;
				if (Global.Dataholder.TriStrip) {
					if (Global.Dataholder.TriStripProperties [0].ID == 3 || Global.Dataholder.TriStripProperties [1].ID == 3 || Global.Dataholder.TriStripProperties [2].ID == 3) {
						oneHundred = true;
					}
				} else {
					if (Global.Dataholder.LoneStripProperties.ID == 3) {
						oneHundred = true;
					}
				}

				if (Acapella) {
					

					if (oneHundred) {
						AS.volume = (Global.Dataholder.MusicVolume * Global.Dataholder.MusicMultiplier * Global.Dataholder.HouseMusicMult);

					} else {
						AS.volume = 0;

					}


				} else {
					if (oneHundred) {
						AS.volume = 0;

					} else {
						AS.volume = (Global.Dataholder.MusicVolume * Global.Dataholder.MusicMultiplier * Global.Dataholder.HouseMusicMult);

					}
				}
			} else {
				AS.volume = (Global.Dataholder.MusicVolume * Global.Dataholder.MusicMultiplier * (1-Global.Dataholder.HouseMusicMult));
			}
		}
	}
}
