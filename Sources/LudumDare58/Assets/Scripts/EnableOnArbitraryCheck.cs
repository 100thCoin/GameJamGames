using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnArbitraryCheck : MonoBehaviour {

	public Interactive Interact;
	public bool Disable;
	public int ID;
	// Use this for initialization
	void Start () {
		if (!Disable) {
			Interact.Box = Interact.GetComponent<BoxCollider> ();
			Interact.Box.enabled = false;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Global.Dataholder.ArbitraryChecks [ID]) {
			Interact.enabled = !Disable;
			if (!Disable) {
				Interact.Box.enabled = true;
			} else {
				Destroy (gameObject);
			}
			Destroy (this);
		}
	}
}
