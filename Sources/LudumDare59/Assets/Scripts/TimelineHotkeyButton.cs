using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineHotkeyButton : MonoBehaviour {

	public SpriteRenderer SR;
	public Sprite NoHover;
	public Sprite Hover;

	public int hoverGrace;
	void OnMouseOver()
	{
		hoverGrace = 2;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (Global.Dataholder.Timeline.ChetLocked) {
			SR.sprite = NoHover;

			return;
		}

		if (hoverGrace > 0) {
			SR.sprite = Hover;

			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				if (!Global.Dataholder.paused) {
					Global.Dataholder.HotkeyPaused = !Global.Dataholder.HotkeyPaused;
					Global.Dataholder.HotkeyMenu.SetActive (Global.Dataholder.HotkeyPaused);
				}
			}
		} else {
			SR.sprite = NoHover;

		}
		hoverGrace--;
	}

}
