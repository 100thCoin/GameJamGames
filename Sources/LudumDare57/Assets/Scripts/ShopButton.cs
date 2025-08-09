using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour {

	public bool Purchased;
	public float Requirement;
	[TextArea(5,5)]
	public string Message;

	public SpriteRenderer SR;
	public Sprite UnpurchasedUnselected;
	public Sprite UnpurchasedSelected;
	public Sprite PurchasedUnselected;
	public Sprite PurchasedSelected;

	public int SelectionIndex;


	public int Grace = 0;
	void OnMouseOver () {
		MouseOver = true;
		Global.Dataholder.SelectedShopButton = this;
		Grace = 3;
	}

	void Purchase()
	{
		Purchased = true;
		if (Global.Dataholder.Purchase_SFX != null) {
			Instantiate (Global.Dataholder.Purchase_SFX, transform.position, transform.rotation);
		} else {
			print ("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
		}

		switch (SelectionIndex) {
		case 0:
			Global.Dataholder.Upgrade_SpeedBoost = true;
			break;
		case 1:
			Global.Dataholder.Upgrade_JumpBoost = true;
			break;
		case 2:
			Global.Dataholder.Upgrade_StardustRemover = true;
			break;
		case 3:
			Global.Dataholder.Upgrade_Speed2 = true;
			break;
		case 4:
			Global.Dataholder.Upgrade_Mustache = true;
			break;
		}
	}

	// Use this for initialization
	void Start () {
		
	}

	public bool MouseOver;

	// Update is called once per frame
	void Update () {
		if (Grace > 0) {
			if (Purchased) {
				SR.sprite = PurchasedSelected;
			} else {
				SR.sprite = UnpurchasedSelected;
				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					if (Global.Dataholder.ReconstructedPercent >= Requirement) {
						Purchase ();
					}
				}
			}
		} else {
			if (Purchased) {
				SR.sprite = PurchasedUnselected;
			} else {
				SR.sprite = UnpurchasedUnselected;
			}
		}
		Grace--;
	}
}
