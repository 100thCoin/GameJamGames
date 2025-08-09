using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class submittoContest : MonoBehaviour {

	public SpriteRenderer SR;
	public Sprite MouseOver;
	public Sprite MouseNotOver;
	public bool MouseInside;

	public PlayerMovement Pmov;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (MouseInside) {


			bool active = false;

			if (Main.Data.InvSlotItems [Main.Data.CurrentInvSlot] == 10) {
				active = true;
			} else if (Main.Data.InvSlotItems [Main.Data.CurrentInvSlot] == 8 && Main.Data.ItemCounts[10] >0) {
				active = true;
			}

			if (active) {
				SR.sprite = MouseOver;
			} else {
				SR.sprite = MouseNotOver;

			}
			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				if (active) {
					Pmov.WinGame = true;
					Main.Data.Win = true;
					// win game
					print ("temp win game");

				}



			}

		} else {
			SR.sprite = MouseNotOver;
		}


		MouseInside = false;
	}


	void OnMouseOver () {

		MouseInside = true;
	}

}
