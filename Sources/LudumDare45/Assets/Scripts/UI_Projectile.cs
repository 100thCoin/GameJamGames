using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Projectile : MonoBehaviour {


	public DataHolder Main;
	public SpriteRenderer sr;
	public SpriteRenderer sr2;
	public SpriteRenderer sr3;
	public SpriteRenderer sr4;

	void Update () {
		sr.enabled = Main.HeldPickup != 0;
		sr2.enabled =Main.HeldPickup != 0;
		sr3.enabled = Main.HeldPickup != 0;
		sr4.enabled = Main.HeldPickup != 0;

		if(Main.HeldPickup != 0)
		{
			sr.sprite = Main.Pickups[Main.HeldPickup].GetComponent<SpriteRenderer>().sprite;
			sr2.sprite = Main.Pickups[Main.HeldPickup].GetComponent<SpriteRenderer>().sprite;

		}

	}
}
