using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_PercentTracker : MonoBehaviour {

	public TextMesh TM;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TM.text = "Moon Reconstructed: " + Mathf.Floor (Global.Dataholder.ReconstructedPercent * 100) + "%";
	}
}
