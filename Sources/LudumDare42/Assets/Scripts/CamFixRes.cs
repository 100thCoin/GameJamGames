using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFixRes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(Screen.currentResolution.width < 1536 || Screen.currentResolution.height < 1024)
		{
			Screen.SetResolution (768, 512, false);
		}
		else
		{
			Screen.SetResolution (1536, 1024, false);
		}			
		if(Screen.currentResolution.width < 768)
		{
			Screen.SetResolution (384, 256, false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
