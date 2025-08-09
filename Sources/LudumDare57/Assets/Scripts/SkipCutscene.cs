using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCutscene : MonoBehaviour {

	public SpriteRenderer SR;
	public Sprite NoHovber;
	public Sprite Hover;
	public bool Skipping;
	int hoverGrace= 0;
	public Cutscene Cut;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (hoverGrace > 0) {
			SR.sprite = Hover;
			if (Input.GetKeyDown (KeyCode.Mouse0) && !Skipping) {
				Cut.Timer = Cut.Duration;
				Skipping = true;
			}
		} else {
			SR.sprite = NoHovber;

		}

		hoverGrace--;
	}

	void OnMouseOver()
	{
		hoverGrace = 2;
	}

}
