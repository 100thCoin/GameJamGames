using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidVat : MonoBehaviour {

	public bool Hit;
	public float Timer;
	public bool DoAcid;
	public GameObject AcidSFX;
	public GameObject AcidVisuals;
	public GameObject MyFloorSpeech;
	public GameObject FloorToRemove;
	public bool MusicStarted;
	public Animator Smarticus;
	public RuntimeAnimatorController MyFloorAnimation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Hit) {
			Global.DataHolder.Player.CutsceneLock = true;
			Timer += Time.deltaTime;

			if (Timer > 2 && Timer < 3) {
				float Rotation = DataHolder.TwoCurveLerp (0, 180, Timer - 2, 1);
				transform.eulerAngles = new Vector3 (0, 0, Rotation);

			}
			if (Timer > 3) {
				transform.eulerAngles = new Vector3 (0, 0, 180);

			}

			if (Timer > 3.5f && Timer < 4.5f) {
				AcidVisuals.SetActive (true);
				FloorToRemove.SetActive (false);

				if (!DoAcid) {
					Instantiate (AcidSFX);
					Global.DataHolder.RemoveCurrentVoiceLine ();
				}
				DoAcid = true;
			}
			if (Timer > 4.5f) {
				AcidVisuals.SetActive (false);
				if (DoAcid) {
					Instantiate (MyFloorSpeech);
					Smarticus.runtimeAnimatorController = MyFloorAnimation;
				}
				DoAcid = false;
			}
			if (Timer > 4.5f+25.1f) {
				if (!MusicStarted) {
					Global.DataHolder.BeginBossMusic ();
					SuperMain.M.MusicMult = 1;

				}
				MusicStarted = true;
			}
			if (Timer > 36) {

				Global.DataHolder.Trans.Vertical = true;
				Global.DataHolder.NextLevel ();

				Destroy (this);
			}
		}



	}

	void OnTriggerEnter(Collider other)
	{
		if (Hit) {
			return;
		}

		if (other.tag == "Bomb") {

			Instantiate (Global.DataHolder.Player.ExplosionPrefab, other.transform.position, transform.rotation, transform.parent.parent);
			//SFX isnt here yet, so...
			if (Global.DataHolder.Player.BombExplosion_SFX != null) {
				Instantiate (Global.DataHolder.Player.BombExplosion_SFX, transform.position, transform.rotation);
			}

			Destroy (other.gameObject);


			Hit = true;

		}
	}


}
