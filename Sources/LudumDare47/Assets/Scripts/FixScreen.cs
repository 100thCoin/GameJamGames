using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {

		if(Screen.currentResolution.width > 768*2)
		{
			Screen.SetResolution(768*2,512*2,true);

		}
		else
		{
			Screen.SetResolution(768,512,true);
		}
		//help
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
