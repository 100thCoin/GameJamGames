using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour {

	public float Timer;
	public TextMesh TM;
	// Use this for initialization
	void Start () {

		string SRTime = DataHolder.StringifyTime(Global.Dataholder.SpeedrunTime);

		string msg = "Speedrun time:\n" + SRTime;

		TM.text = msg;

	}
	
	// Update is called once per frame
	void Update () {

		Timer += Time.deltaTime;
		Timer = Mathf.Clamp01 (Timer);

		transform.localPosition = new Vector3 (0, DataHolder.ParabolicLerp(-9,0,Timer,1), 0);



	}
}
