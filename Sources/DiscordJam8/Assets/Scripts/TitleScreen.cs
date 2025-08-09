using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour {

	public SpriteRenderer TitleSR;
	public Sprite TitleWithStringsUnlocked;

	// Use this for initialization
	void Start () {
		if (Super.Dataholder.UnlockedUsingLevelStrings) {
			TitleSR.sprite = TitleWithStringsUnlocked;

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
