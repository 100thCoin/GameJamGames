using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVButton : MonoBehaviour {

	public GameObject ClickSFX;
	public GameObject EndSFX;
	public Animator Screen;
	public RuntimeAnimatorController Static;
	public RuntimeAnimatorController Event;
	public GameObject Event_VA;
	public float Timer;
	public float Duration;
	public bool Pressed;
	public SpriteRenderer SR;
	public SpriteRenderer SRAlt;
	public Sprite Down;
	public Sprite DownAlt;
	public GameObject PressAnimation;
	public bool EndOnce;
	public SpriteRenderer ScreenSR;
	public Sprite ScreenOff;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Pressed) {
			Timer += Time.deltaTime;

			if (!Global.DataHolder.Player.CutsceneLock) {
				if (SuperMain.M.MusicMult > 0.15f) {
					SuperMain.M.MusicMult -= Time.deltaTime;
					if (SuperMain.M.MusicMult < 0.15f) {
						SuperMain.M.MusicMult = 0.15f;
					}
				}
			}
			if (Timer > Duration) {
				if (!EndOnce) {
					EndOnce = true;
					Screen.runtimeAnimatorController = Static;
					Instantiate (EndSFX, transform.position, transform.rotation, transform.parent.parent);				
				}
				if (!Global.DataHolder.Player.CutsceneLock) {
					if (SuperMain.M.MusicMult < 1) {
						SuperMain.M.MusicMult += Time.deltaTime * 2;
						if (SuperMain.M.MusicMult > 1) {
							SuperMain.M.MusicMult = 1;
						}
					}
				}
			}
			if (Timer > Duration+0.5f) {
				Screen.runtimeAnimatorController = null;
				ScreenSR.sprite = ScreenOff;
				this.enabled = false;
				SuperMain.M.MusicMult = 1;

			}

		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {

			if (Pressed) {
				return;
			}
			SuperMain.M.VAMult = 1;
			Pressed = true;
			SR.sprite = Down;
			SRAlt.sprite = DownAlt;
			Instantiate (ClickSFX, transform.position, transform.rotation, transform.parent.parent);
			if (Event != null) {
				Screen.runtimeAnimatorController = Event;

			}
			if (Event_VA != null) {
				Instantiate (Event_VA, new Vector3 (0, 0, 0), transform.rotation, Global.DataHolder.VAHolder);
			}
			PressAnimation.SetActive (true);
		}

	}

}
