using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnArbitraryCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public int ID;

	// Update is called once per frame
	void Update () {
		if (Global.Dataholder.ArbitraryChecks [ID]) {
			Destroy (gameObject);
		}
	}
}
