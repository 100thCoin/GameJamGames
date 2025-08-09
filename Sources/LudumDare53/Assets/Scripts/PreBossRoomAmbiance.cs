using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreBossRoomAmbiance : MonoBehaviour {

	public bool DoOnce;

	//put this on the tv, I guess?

	// Use this for initialization
	void Start () {
		Global.DataHolder.Player.PreBossNoRun = true;
		Global.DataHolder.Player.SR.flipX = true;
		Global.DataHolder.Player.WSR.flipX = true;

	}

	public bool Lowered;
	public float Timer;

	// Update is called once per frame
	void Update () {

		if (!Lowered) {

			if (SuperMain.M.MusicMult < 0.15f) {
				SuperMain.M.MusicMult = 0.15f;
				Lowered = true;
			}
			if (SuperMain.M.MusicMult > 0.15f) {
				
				SuperMain.M.MusicMult -= Time.deltaTime;

			}


		}


		Timer += Time.deltaTime;
		if (Timer > 4) {
			if (!DoOnce) {
				SuperMain.M.VAMult = 1;
				Instantiate (Global.DataHolder.VA_WellTerryYouMadeIt);
			}

			DoOnce = true;

		}


		if (Timer > 3.7f + 4) {

			Global.DataHolder.RemoveCurrentVoiceLine ();
			Instantiate (Global.DataHolder.VA_Incoherent);


			Destroy (this);
		}

	}
}
