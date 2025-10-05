using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFight2 : MonoBehaviour {


	public GameObject Voice1;

	public Transform FinalGuy;

	public SpriteRenderer GW_Trivia1;
	public SpriteRenderer GW_Trivia2;
	public SpriteRenderer GW_Man1;
	public SpriteRenderer GW_Man2;

	public RuntimeAnimatorController GunPull;
	public RuntimeAnimatorController GunHold;
	public RuntimeAnimatorController GunShoot;
	public RuntimeAnimatorController BossIdle;

	public RuntimeAnimatorController TriviaDead;
	public RuntimeAnimatorController TriviaIdle;
	public Animator TriviaAnim;
	public Animator BossFight;

	public GameObject ShotDead_SFX;
	public GameObject ShotMiss_SFX;

	public bool DoOnce;

	public bool Active;
	public Room ThisRoom;

	public bool Stage1;

	public SpriteRenderer BlackBar;
	public GameObject GunFlash;
	public Animator GunFlashAnim;
	public RuntimeAnimatorController RAC_GunFlash;

	public float Timer;


	public SpriteRenderer ScreenSwoop2;

	public Interactive PlayerJump;
	public bool Phase2LiveOnce;

	public GameObject LeftInteractive;
	public bool phasee3;
	public bool screenflkashonce;

	public SpriteRenderer CaptureHim;
	public SpriteRenderer JumpInPit;
	public float textTimer;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (Global.Dataholder.ActiveRoom == ThisRoom) {
			Global.Dataholder.GameWatchView = false;
			if (!DoOnce) {
				DoOnce = true;
				VoiceClip VC =	Instantiate (Voice1, transform.position, transform.rotation, transform.parent).GetComponent<VoiceClip> ();
				Global.Dataholder.CurrentVoiceClip = VC;
			}

			Timer += Time.deltaTime;

			if (Timer > 17.45f) {

				Global.Dataholder.MainCamera.transform.position = new Vector3 (20, 30, -10);

			}

			if (Timer > 18.97f) {

				Global.Dataholder.MainCamera.transform.position = new Vector3 (20, 40, -10);
				if (!screenflkashonce) {
					screenflkashonce = true;
					Global.Dataholder.ScreenFlashTimer = 0.75f;
				}
			}


			if (Global.Dataholder.CurrentVoiceClip == null) {

				CaptureHim.gameObject.SetActive (true);
				JumpInPit.gameObject.SetActive (true);
				textTimer += Time.deltaTime;
				CaptureHim.color = new Vector4 (1, 1, 1, textTimer);
				JumpInPit.color = new Vector4 (1, 1, 1, textTimer);

			}



		} else {

		}

	}
}
