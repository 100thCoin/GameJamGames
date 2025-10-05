using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public SpriteRenderer SR;
	public int Grace;
	public int InventorySlot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Grace <= 0) {
			if (Grace == 0) {
				Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware);
				//print ("RemoveCursor");

			}
		} else {

			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				Global.Dataholder.ItemInHands = Global.Dataholder.InventoryID [InventorySlot];
			}

		}
		Grace--;
	}

	void OnMouseOver()
	{
		if (!SR.enabled) {
			return;
		}

		if (Global.Dataholder.CurrentVoiceClip == null) {


			Grace = 2;	
			if (Global.Dataholder.ItemInHands == Global.Dataholder.InventoryID[InventorySlot]) {
				return;
			}
			Cursor.SetCursor (Global.Dataholder.CustomCursors [5].Tex.texture, Global.Dataholder.CustomCursors [5].HotSpot, CursorMode.ForceSoftware);

		}
	}

}
