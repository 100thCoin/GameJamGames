using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceRes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(		Screen.width > 1152 && Screen.height > 768)
		{


			Screen.SetResolution(1152,768,false);

		}
		else
		{
		Screen.SetResolution(768,512,false);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
