using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public Volume Acapella;
	public Volume Boss;
	public Volume MainMusic;

	public float PlayTimer;
	public float PauseTimer;
	public float BossTimer;
	public float PostBossTimer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		PlayTimer += Time.deltaTime;
		if (PlayTimer > 1) {
			PlayTimer = 1;
		}

		if (Global.Dataholder.Timeline.Play) {
			PauseTimer -= Time.deltaTime;
		} else {
			PauseTimer += Time.deltaTime;
		}

		if (Global.Dataholder.Timeline.ChetLocked) {
			BossTimer += Time.deltaTime;
			if (BossTimer > 1) {
				BossTimer = 1;
			}
		}

		if (PauseTimer < 0) {
			PauseTimer = 0;
		}
		if (PauseTimer > 1) {
			PauseTimer = 1;
		}

		Acapella.ForceMultt = ((PlayTimer * PauseTimer) * (1-BossTimer)* (1-Global.Dataholder.PlatformMan.JustShotHandTimer)) * 0.3f;
		MainMusic.ForceMultt = (((PlayTimer * (1-PauseTimer)) * (1-BossTimer))* (1-Global.Dataholder.PlatformMan.JustShotHandTimer))*0.5f;
			Boss.ForceMultt = (PlayTimer * (BossTimer * (1-Global.Dataholder.PlatformMan.JustShotHandTimer)))*0.5f;
	}
}
