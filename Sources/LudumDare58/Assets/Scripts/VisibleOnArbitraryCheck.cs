using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleOnArbitraryCheck : MonoBehaviour {
	public SpriteRenderer Interact;
	public int ID;

	public bool Invisible;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Global.Dataholder.ArbitraryChecks [ID]) {
			Interact.enabled = !Invisible;
		}
	}
}
