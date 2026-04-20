using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelinePuaseButton : MonoBehaviour {

	public TASTimeline Timeline;

	public SpriteRenderer SR;
	public Sprite Pause;
	public Sprite Play;
	public Sprite Seek;
	public Sprite Chet;
	public Sprite EndOfTimeline;

	// Use this for initialization
	void Start () {
		
	}

	public int hoverGrace;
	void OnMouseOver()
	{
		hoverGrace = 2;
	}

	// Update is called once per frame
	void Update () {
		if (Timeline.LiveReactionButton.DoingTutorial) {
			return;
		}

		if (Timeline.CellData.Count > 2 && Timeline.CellData [Timeline.CurrentFrame].EndOfTimeline) {
			SR.sprite = EndOfTimeline;
			return;
		}

		if (Global.Dataholder.paused) {

			SR.sprite = Pause;
			return;

		}

		if (Timeline.ChetLocked) {
			SR.sprite = Chet;
		} else {
			if (Timeline.Play) {
				SR.sprite = Play;
			} else if (Timeline.Seek) {
				SR.sprite = Seek;
			} else {
				SR.sprite = Pause;
			}
		}

		if (hoverGrace > 0) {

			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				if (Timeline.Seek || Timeline.Play) {
					Timeline.Seek = false;
					Timeline.Play = false;
				} else {
					Timeline.Play = true;
				}

			}

		}
		hoverGrace--;
	}
}
