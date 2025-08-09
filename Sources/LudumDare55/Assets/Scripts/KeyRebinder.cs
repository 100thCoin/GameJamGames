using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRebinder : MonoBehaviour {

	public KeyRebinderManager RebindMan;


	public Sprite Off;
	public Sprite Hover;
	public Sprite On;
	public SpriteRenderer SR;

	public int ID;
	public TextMesh TM;

	public bool MouseInvert;
	public bool PlayGame;
	public bool ShrinkText;
	public int Grace = 0;

	[ContextMenu("set")]
	void Set () {
		SR = GetComponent<SpriteRenderer> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (RebindMan.CurrentlyRebindingThisGuy != this || RebindMan.Locked) {
			Grace--;
			if (Grace < 0) {
				SR.sprite = Off;
			}
		}

	}

	void OnMouseOver()
	{
		if (RebindMan.CurrentlyRebindingThisGuy == null && !RebindMan.Locked) {
			SR.sprite = Hover;
			Grace = 2;
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				if (PlayGame) {

					RebindMan.Locked = true;

				}
				else if (MouseInvert) {

					Super.Dataholder.InvertMouse = !Super.Dataholder.InvertMouse;
					if (Super.Dataholder.InvertMouse) {
						TM.text = "YES";
					} else {
						TM.text = "NO";
					}

				} else {
					RebindMan.CurrentlyRebindingThisGuy = this;
					SR.sprite = On;
					RebindMan.Delay = 3;
				}
			}

		}
	}
}
