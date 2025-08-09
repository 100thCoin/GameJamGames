using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LevelGoalSprite : MonoBehaviour {

	public DataHolder Main;
	public SpriteRenderer sr;

	void Update () {

		sr.sprite = Main.ItemPickupGoalLevel;


	}
}
