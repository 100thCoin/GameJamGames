using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCeremony : MonoBehaviour {

	public CeremonyPlayer P;
	public bool Done;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame

	void OnMouseOver () {
		if(Input.GetKeyDown(KeyCode.Mouse0) &&!Done)
		{
			Done = true;
			print("YES");
			P.SKip = true;
		}


	}
}
