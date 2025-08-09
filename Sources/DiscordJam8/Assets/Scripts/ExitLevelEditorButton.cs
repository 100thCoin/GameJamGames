using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevelEditorButton : MonoBehaviour {

	public SpriteRenderer SR;
	public Sprite S_Inactive;
	public Sprite S_Active;
	public Sprite S_Hover;
	public Sprite S_Click;
	public NotEnoughEvilShake TM;

	public bool MouseOver;
	public int Grace;

	public bool clicking;
	// Use this for initialization
	void Start () {
		
	}

	void OnMouseOver () {
		MouseOver = true;
		Grace = 3;
	}

	// Update is called once per frame
	void Update () {

		if (Global.DataHolder.InLevelEditor) {

			if ((!Global.DataHolder.Level2 && Global.DataHolder.EvilPoints < Global.DataHolder.RequiredEvilPoints) || (Global.DataHolder.Level2 && Global.DataHolder.EvilPoints < Global.DataHolder.RequiredEvilPoints2)) {
				// not enough
				if(Super.Dataholder.GetReboundInputDown (KeyCode.Return))
				{
					TM.Timer = 1;
				}
				if (MouseOver) {
					TM.Timer = 0.2f;
				}
				SR.sprite = S_Inactive;
			} else {
				SR.sprite = S_Active;
				if(Super.Dataholder.GetReboundInputDown (KeyCode.Return))
				{
					EnterLevel ();
				}
				if (MouseOver || clicking) {
					SR.sprite = S_Hover;
					if (Input.GetKey (KeyCode.Mouse0)) {
						SR.sprite = S_Click;
						clicking = true;
					}
					if (clicking && !Input.GetKey (KeyCode.Mouse0)) {
						EnterLevel ();
					}
				}
			}

		}
		Grace--;
		if (Grace < 0) {
			MouseOver = false;
		}
	}

	public void EnterLevel()
	{
		clicking = false;
		Global.DataHolder.EnterLevelExitEditor ();
		MouseOver = false;
		Grace = 0;
	}

}
