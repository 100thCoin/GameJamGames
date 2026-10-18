using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOn3Keys : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Global.Dataholder.GenericUnlocks [20] && Global.Dataholder.GenericUnlocks [21] && Global.Dataholder.GenericUnlocks [22]) {
			gameObject.SetActive (false);
		}
	}
}
