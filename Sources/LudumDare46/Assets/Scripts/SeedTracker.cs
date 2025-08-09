using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedTracker : MonoBehaviour {

	public TextMesh TM;

	public Farm F;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TM.text = ": " + F.SeedStock;

	}
}
