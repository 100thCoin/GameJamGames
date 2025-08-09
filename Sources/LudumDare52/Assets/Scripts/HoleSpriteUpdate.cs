using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSpriteUpdate : MonoBehaviour {

	public Sprite Old1;
	public Sprite Old2;
	public Sprite New1;
	public Sprite NeW2;

	// Update is called once per frame
	void Update () {

		if (Main.Data.UpgradedFarm) {
			SpriteRenderer SR = GetComponent<SpriteRenderer> ();
			SR.sprite = SR.sprite == Old1 ? New1 : NeW2;
		}

	}
}
