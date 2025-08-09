using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSpriteInPlay : MonoBehaviour {

	public SpriteRenderer SR;
	// Use this for initialization
	void OnEnable () {
		SR.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		Destroy(this);
	}
}
