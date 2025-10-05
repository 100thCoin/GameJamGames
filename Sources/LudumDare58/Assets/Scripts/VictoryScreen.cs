using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour {

	public bool pausebutton;

	public int hoverGrace;
	public SpriteRenderer SR;
	public Sprite Hover;
	public Sprite NoHover;

	public TextMesh TM;

	// Use this for initialization
	void Start () {
		if (!pausebutton) {
			TM.text = "Speedrun Time: " + DataHolder.StringifyTime (Global.Dataholder.SpeedrunTime);
			if (Global.Dataholder.WIN) {
				TM.text += "\n\nThe true ending!";
			} else {
				TM.text += "\n\nThe spike pit ending!";

			}
		}
	}

	// Update is called once per frame
	void Update () {

		if (hoverGrace > 0) {
			SR.sprite = Hover;

			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				Super.Dataholder.ReturnToTitle ();
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
