using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryButton : MonoBehaviour {

	public SpriteRenderer SR;
	public Sprite Hover;
	public Sprite NoHover;
	public int Grace;
	public bool MouseOver;

	void OnMouseOver () {
		MouseOver = true;
		Grace = 3;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		SR.sprite = MouseOver ? Hover : NoHover;

		if (Hover) {

			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					Global.Dataholder.GameLoad.REGENERATE ();
					return;
				}
				// quit to title.
				print("QUIT TO TITLE");

			}


		}

		if(Grace< 0)
		{
			MouseOver = false;
		}
		Grace--;
	}
}
