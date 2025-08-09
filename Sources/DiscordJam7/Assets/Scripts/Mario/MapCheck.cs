using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCheck : MonoBehaviour {

	public bool CheckLev1;
	public SpriteRenderer SR;
	public Sprite Check;

	// Update is called once per frame
	void Update () {

		if(CheckLev1)
		{
			if(Main.DataHolder.MarioLevel1Clear)
			{
				SR.sprite = Check;
			}
		}
		else
		{
			if(Main.DataHolder.MarioLevel2Clear)
			{
				SR.sprite = Check;
			}
		}

	}
}
