using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour {

	public TextMesh TM;

	// Use this for initialization
	void Start () {
		TM.text = "Statistics: \n\n" +
			"Rerecords (Timelines Ended) : " + Global.Dataholder.Timeline.Rerecords + "\n" +
			"Speedrun Time: " + DataHolder.StringifyTime(Global.Dataholder.SpeedrunTime) + "\n" +
			"TAS Time : " + Global.Dataholder.Timeline.LastEmulatedFrame + " frames\n" +
			"                    (" + DataHolder.StringifyTime(Global.Dataholder.Timeline.LastEmulatedFrame/60f) + ")";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
