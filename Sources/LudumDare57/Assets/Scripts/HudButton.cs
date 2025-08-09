using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudButton : MonoBehaviour {

	public bool Inactive;

	public int hoverGrace;

	public SpriteRenderer SR;
	public Sprite HUD_InactiveButton;
	public Sprite HUD_ActiveButton;
	public Sprite HUD_HoverButton;
	public Sprite HUD_Blink;

	public bool Pseudogame;
	public float pseudogametimer;

	public bool Blink;
	public float BlinkTimer;

	void OnMouseOver()
	{
		hoverGrace = 2;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Global.Dataholder.EndedPseudogame) {
			pseudogametimer += Time.deltaTime;
			Global.Dataholder.PseudogameFadeToBlack.color = new Vector4 (0, 0, 0, pseudogametimer);
			if (pseudogametimer > 1) {
				Global.Dataholder.EndingCutscene.SetActive (true);
				Global.Dataholder.Pseudogame.SetActive (false);
				Blink = false;
			}

		}

	
		if (Inactive) {
			SR.sprite = HUD_InactiveButton;
			return;
		}

		if (hoverGrace > 0) {
			SR.sprite = HUD_HoverButton;
			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				Blink = false;

				if (!Pseudogame) {
					Inactive = true;
					Global.Dataholder.PMov.ForceWarp = true;
					return;
				} else {
					Inactive = true;
					Global.Dataholder.EndedPseudogame = true;
				}
			}
		} else {
			if (!Blink) {
				SR.sprite = HUD_ActiveButton;
			} else {
				BlinkTimer += Time.deltaTime;
				if (BlinkTimer > 1) {
					BlinkTimer -= 1;
				}
				if (BlinkTimer > 0.5f) {
					SR.sprite = HUD_ActiveButton;

				} else {
					SR.sprite = HUD_Blink;

				}
			}
		}

		hoverGrace--;
	}
}
