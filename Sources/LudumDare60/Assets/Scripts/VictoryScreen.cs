using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour {

	public TextMesh TM;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		TM.text = "Speedrun time:\n" + DataHolder.StringifyTime (Global.Dataholder.SpeedrunTime);

	}
}
