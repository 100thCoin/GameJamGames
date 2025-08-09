using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeFarm : MonoBehaviour {


	public int MouseInside;


	public Collider col;
	public SpriteRenderer Glow;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (Main.Data.InvSlotItems [Main.Data.CurrentInvSlot] == 7) {
			col.enabled = true;
		} else {
			col.enabled = false;
		}

		if (MouseInside > 0 && Main.Data.InvSlotItems [Main.Data.CurrentInvSlot] == 7) {
			Glow.enabled = true;

				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					Main.Data.Obj_PlantNewSoil = true;
					Main.Data.RemoveItem (7);
				Main.Data.UpgradedFarm = true;
					Main.Data.FarmPLot.sprite = Main.Data.FarmMK2;
				Glow.enabled = false;
					Destroy (gameObject);
				}

		} else {

			Glow.enabled = false;

		}
		MouseInside--;

	}


	void OnMouseOver () {

		MouseInside = 3;
	}
		
}
