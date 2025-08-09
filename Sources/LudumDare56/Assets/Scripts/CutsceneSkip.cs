using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSkip : MonoBehaviour {

	public Cutscene Cut;
	public Cutscene Cut2;

	public int hoverGrace;
	public SpriteRenderer SR;
	public Sprite Hover;
	public Sprite NoHover;

	void Update () {
		if (Cut.Duration <= 0) {
			return;
		}
		if (hoverGrace > 0) {
			SR.sprite = Hover;
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				if (Global.Dataholder.InVictoryCutscene) {
					Cut2.Duration = 0;

				} else {
					Cut.Duration = 0;

				}
			}

		} else {
			SR.sprite = NoHover;
		}

		hoverGrace--;
	}

	void OnMouseOver()
	{
		hoverGrace = 2;
	}
}
