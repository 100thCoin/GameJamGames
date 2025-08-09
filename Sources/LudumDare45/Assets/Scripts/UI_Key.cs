using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Key : MonoBehaviour {

	public DataHolder Main;
	public SpriteRenderer sr;
	public SpriteRenderer sr2;
	public SpriteRenderer sr3;
	public SpriteRenderer sr4;

	void Update () {
		sr.enabled = Main.HasKey;
		sr2.enabled = Main.HasKey;
		sr3.enabled = Main.HasKey;
		sr4.enabled = Main.HasKey;

	}
}
