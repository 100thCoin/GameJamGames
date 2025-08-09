using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixResolution : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {

			Screen.SetResolution(768,512,false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
