using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTimer : MonoBehaviour {

	public TextMesh TM;

	// Use this for initialization
	void Start () {
		TM.text = "Speedrun Time:\n\n" + DataHolder.StringifyTime(Global.Dataholder.SpeedrunTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
