using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixFont : MonoBehaviour {

	public Font F;

	// Use this for initialization
	void Start () {
		F.material.mainTexture.filterMode = FilterMode.Point;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
