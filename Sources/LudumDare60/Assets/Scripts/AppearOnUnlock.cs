using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOnUnlock : MonoBehaviour {

	public int UnlockRequired;
	public SpriteRenderer SR;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Global.Dataholder.GenericUnlocks [UnlockRequired]) {
			SR.enabled = true;
			Destroy (this);
		}

	}
}
