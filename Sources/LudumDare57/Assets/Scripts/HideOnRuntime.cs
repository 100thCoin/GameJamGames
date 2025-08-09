using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnRuntime : MonoBehaviour {

	public SpriteRenderer SR;

	// Use this for initialization
	void Start () {
		if (!Super.Dataholder.DEBUGGING) {
			SR.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
