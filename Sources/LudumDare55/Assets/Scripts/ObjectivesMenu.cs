using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesMenu : MonoBehaviour {

	public bool OnScreen;
	public float Timer;

	[TextArea(5,5)]
	public string[] Objectives;
	public TextMesh TM;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (OnScreen) {
			Timer += Time.deltaTime;
			TM.text = Objectives [Global.Dataholder.Level];
		} else {
			Timer -= Time.deltaTime;
			if (!Global.Dataholder.Pmov.InCutscene) {
				Super.Dataholder.MusicMultiplier = (1 - Mathf.Clamp01 (Timer)) * 0.5f + 0.5f;
			}
		}
		Timer = Mathf.Clamp01 (Timer);

		transform.localPosition = new Vector3 (0, DataHolder.ParabolicLerp(-9,0,Timer,1), 0);


	}
}
