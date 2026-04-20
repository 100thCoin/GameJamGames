using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasTimelineScrollbar : MonoBehaviour {

	public TASTimeline Timeline;

	public SpriteRenderer SR;

	public float HoverTimer;

	public bool Dragging;

	public Color NoHover;
	public Color Hover;
	public Color Drag;

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

		if (hoverGrace > 0) {
			HoverTimer += Time.deltaTime * 5;
			if (HoverTimer < 0) {
				HoverTimer = 1;
			}
			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				Dragging = true;

			}

		} else if (!Dragging) {
			HoverTimer -= Time.deltaTime*5;
			if (HoverTimer < 0) {
				HoverTimer = 0;
			}
		}
		if (Input.GetKeyUp (KeyCode.Mouse0)) {

			Dragging = false;

		}
		if (Dragging) {
			HoverTimer = 2;
			SR.color = Drag;
		} else {
			SR.color = new Vector4 (Mathf.Lerp (NoHover.r, Hover.r, HoverTimer), Mathf.Lerp (NoHover.g, Hover.g, HoverTimer), Mathf.Lerp (NoHover.b, Hover.b, HoverTimer), 1);
		}

		hoverGrace--;
	}
}
