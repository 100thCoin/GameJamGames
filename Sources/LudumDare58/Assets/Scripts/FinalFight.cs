using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFight : MonoBehaviour {

	public GameObject Voice1;
	public GameObject Voice2;

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

	public float GunTimer;
	public bool Stage1;

	public SpriteRenderer BlackBar;
	public GameObject GunFlash;
	public Animator GunFlashAnim;
	public RuntimeAnimatorController RAC_GunFlash;

	public bool Phase2_Dead;
	public bool Phase2_Live;
	public float PHase2Timer;

	public SpriteRenderer ScreenSwoop2;

	public Interactive PlayerJump;
	public bool Phase2LiveOnce;

	public GameObject LeftInteractive;
	public bool phasee3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		GW_Trivia1.enabled = PlayerJump.JumpCooldown < 0.2f;
		GW_Trivia2.enabled = PlayerJump.JumpCooldown > 0.2f;

		if (Global.Dataholder.ActiveRoom == ThisRoom) {
			if (!DoOnce) {
				DoOnce = true;
				VoiceClip VC =	Instantiate (Voice1, transform.position, transform.rotation, transform.parent).GetComponent<VoiceClip> ();
				Global.Dataholder.CurrentVoiceClip = VC;
			}
			if (Global.Dataholder.CurrentVoiceClip == null) {
				if (Stage1) {
					BossFight.runtimeAnimatorController = GunPull;
					GW_Man1.enabled = false;
					GW_Man2.enabled = true;
					GunTimer += Time.deltaTime;
					BlackBar.transform.localScale = new Vector3 (64, DataHolder.SinLerp (16, 1, (GunTimer), 0.5f), 1);
					BlackBar.color = new Vector4 (0, 0, 0, GunTimer);
					if (GunTimer > 0.5f) {
						Stage1 = false;
						GunFlash.SetActive (true);
						GunFlash.GetComponent<SpriteRenderer> ().enabled = true;
						GunFlashAnim.runtimeAnimatorController = null;
						GunFlashAnim.runtimeAnimatorController = RAC_GunFlash;
						BlackBar.color = new Vector4 (0, 0, 0, 0);
						if (PlayerJump.JumpCooldown > 0.2f) {
							Instantiate (ShotMiss_SFX, transform.position, transform.rotation, transform.parent);
							Phase2_Live = true;
							PHase2Timer = 0;

						}
						else
						{
							Instantiate (ShotDead_SFX, transform.position, transform.rotation, transform.parent);
							Phase2_Dead = true;
							PHase2Timer = 0;
							Global.Dataholder.PlayerDead = true;

						}

					}
				}
			}
			if (Phase2_Dead) {
				PHase2Timer += Time.deltaTime;
				TriviaAnim.runtimeAnimatorController = TriviaDead;
				Global.Dataholder.GameWatchView = false;


				if (PHase2Timer > 1 && PHase2Timer < 2) {

					ScreenSwoop2.color = new Vector4(0,0,0,DataHolder.SinLerp(1,149f/256f,PHase2Timer-1,1));

				}


				if (PHase2Timer > 3 && PHase2Timer < 3.5f) {

					ScreenSwoop2.color = new Vector4(0,0,0,DataHolder.SinLerp(149f/256f,0.48f,PHase2Timer-3,0.5f));

				}

				if (PHase2Timer > 4) {
					Global.Dataholder.MainCamera.transform.position = new Vector3 (10, 20, -10);
					ScreenSwoop2.color = new Vector4(0,0,0,DataHolder.SinLerp(0.48f,1,PHase2Timer-4,1f));
				}
				if (PHase2Timer > 5) {
					ScreenSwoop2.color = new Vector4(0,0,0,1);
				}
			}
			if (Phase2_Live) {
				Global.Dataholder.GameWatchView = false;
				PHase2Timer += Time.deltaTime;
				if (PHase2Timer > 1) {
					if (!Phase2LiveOnce) {
						VoiceClip VC =	Instantiate (Voice2, transform.position, transform.rotation, transform.parent).GetComponent<VoiceClip> ();
						Global.Dataholder.CurrentVoiceClip = VC;
						GW_Man1.enabled = true;
						GW_Man2.enabled = false;
						BossFight.runtimeAnimatorController = BossIdle;
						Phase2LiveOnce = true;
						LeftInteractive.SetActive (true);
						phasee3 = true;
					}
				}
			}

			if (phasee3 && Global.Dataholder.CurrentVoiceClip == null) {
				FinalGuy.transform.position -= new Vector3(Time.deltaTime*2,0,0);
			}


		} else {
			//cleanup!
			DoOnce = false;
			GunTimer = 0;
			Stage1 = true;
			PHase2Timer = -1;
			TriviaAnim.runtimeAnimatorController = TriviaIdle;
			Phase2_Dead = false;
			Phase2_Live = false;
			PHase2Timer = 0;
			ScreenSwoop2.color = new Vector4(0,0,0,1);
			BossFight.runtimeAnimatorController = BossIdle;
			Phase2LiveOnce = false;
			GW_Man1.enabled = true;
			GW_Man2.enabled = false;
		}

	}
}
