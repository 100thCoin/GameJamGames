using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnim : MonoBehaviour {

	public float AnimTimer;
	public bool GameCompleteScene;
	public bool SFXonce = false;
	public bool GameOpeningScene;
	public float OpeningFadeIn;
	public Animator OpeningAnim;
	public RuntimeAnimatorController Opening_RAC;
	public GameObject VA;
	// Use this for initialization
	void Start () {
		Super.Dataholder.MusicMultiplier = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameOpeningScene) {
			OpeningFadeIn += Time.deltaTime;
			if (OpeningFadeIn < 1) {
				Super.Dataholder.MusicMultiplier = Mathf.Clamp01 (OpeningFadeIn)*0.5f;
				Global.Dataholder.LevelResetBlackout.color = new Vector4 (0, 0, 0, 1 - OpeningFadeIn);
			
			} else {
				OpeningAnim.runtimeAnimatorController = Opening_RAC;
				VA.SetActive (true);
			}

		}

		AnimTimer -= Time.deltaTime;
		if (AnimTimer < 1 && !SFXonce && !GameCompleteScene && !GameOpeningScene) {
			SFXonce = true;
			Instantiate (Global.Dataholder.SFX_Bomb, transform.position, transform.rotation);
			Super.Dataholder.MusicMultiplier = 0;
		}
		if (GameCompleteScene) {
			Super.Dataholder.MusicMultiplier = 0;
		}

		if (AnimTimer < 0) {
			if (GameCompleteScene) {
				Global.Dataholder.ShowVictoryScreen = true;
			} else if (GameOpeningScene) {
				Global.Dataholder.OMenu.OnScreen = true;
				Global.Dataholder.Pmov.gameObject.SetActive (true);
				Destroy (gameObject);
			}
			else
			{
				Global.Dataholder.CutMan.OopsIExplodedCutscene = true;
				Destroy (this);
			}




		}

	}
}
