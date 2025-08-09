using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Dropdown : MonoBehaviour {

	public float Timer;
	public bool GoalHUD;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!GoalHUD) {
			if (Global.Dataholder.PMov.InsideStage) {
				Timer += Time.deltaTime * 3;
			} else {
				Timer -= Time.deltaTime * 3;
			}
		} else {
			if (Global.Dataholder.PMov.InsideClearStage) {
				Timer += Time.deltaTime * 3;
			} else {
				Timer -= Time.deltaTime * 3;
			}
		}

		Timer = Mathf.Clamp01 (Timer);

		transform.localPosition = new Vector3 (0, DataHolder.SinLerp (10f, 6f, Timer, 1), 10);

	}
}
