using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromooncerReturnGemCutscene : MonoBehaviour {

	public SpriteRenderer FadeToBlack;
	public float Timer;
	public GameObject Gem1;
	public GameObject GemOutline;
	public GameObject RealGem;
	public GameObject HUD;
	public CameraController CamControl;
	public bool DoIt;
	public float[] MiscTimers;
	public Vector3 CutsceneCameraPos1;
	public Transform CutsceneCameraPos2;

	public GameObject StartVoiceClip;
	public GameObject EndVoiceClip;
	public GameObject Necromooncer;

	Vector3 Gem1SPos;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (DoIt) {
			if (CamControl.enabled) {
				Global.Dataholder.VoiceQueue.Add (StartVoiceClip);
			}
			CamControl.enabled = false;

			Timer += Time.deltaTime;
			if (Timer < 2) {
				Gem1.SetActive (true);
				Gem1SPos = Gem1.transform.position;
				CutsceneCameraPos1 = CamControl.transform.position;
			}
			if (Timer < 3 && Timer > 2) {
				MiscTimers [0] += Time.deltaTime;
				Gem1.transform.position = new Vector3(Gem1SPos.x,DataHolder.TwoCurveLerp(Gem1SPos.y,Gem1SPos.y+32,MiscTimers[0],1),0);
			}
			if (Timer < 3.5f && Timer > 3) {
				MiscTimers [1] += Time.deltaTime*2.4f;
				FadeToBlack.color = new Vector4 (0, 0, 0, MiscTimers [1]);
			}
			if (Timer < 4f && Timer > 3.5f) {
				HUD.SetActive (false);
				CamControl.transform.position = CutsceneCameraPos2.transform.position;
				MiscTimers [2] += Time.deltaTime*2.4f;
				FadeToBlack.color = new Vector4 (0, 0, 0, 1-MiscTimers [2]);
			}
			if (Timer < 5 && Timer > 4) {
				MiscTimers [3] += Time.deltaTime;
				Gem1.transform.position = new Vector3(GemOutline.transform.position.x,DataHolder.TwoCurveLerp(GemOutline.transform.position.y+32,GemOutline.transform.position.y,MiscTimers[3],1),0);
			}
			if (Timer < 6 && Timer > 5) {
				GemOutline.SetActive (false);
				RealGem.SetActive (true);
				Gem1.SetActive (false);
			}
			if (Timer < 6.5f && Timer > 6) {
				MiscTimers [4] += Time.deltaTime*2.4f;
				FadeToBlack.color = new Vector4 (0, 0, 0, MiscTimers [4]);
				Necromooncer.SetActive (false);
			}
			if (Timer < 7f && Timer > 6.5f) {
				HUD.SetActive (true);
				CamControl.transform.position = CutsceneCameraPos1;
				MiscTimers [5] += Time.deltaTime*2.4f;
				FadeToBlack.color = new Vector4 (0, 0, 0, 1-MiscTimers [5]);
				Global.Dataholder.NecromooncerDefeated = true;
				Global.Dataholder.CamMov.Boss1Camera = false;

			}
			if (Timer > 7f) {
				// end cutscene
				DoIt = false;
				CamControl.enabled = true;
				FadeToBlack.color = new Vector4 (0, 0, 0, 0);
				Global.Dataholder.TheHudButton.Inactive = false;
				Global.Dataholder.VoiceQueue.Add (EndVoiceClip);
				Global.Dataholder.TheHudButton.Blink = true;
				Destroy (gameObject);
			}
		}

	}
}
