using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTwirl : MonoBehaviour {

	public float speed;
	public bool overrideSpeed;
	public bool layer2;

	// Use this for initialization
	void Start () {
		if (!overrideSpeed) {
			speed = Random.Range (-15f, 15f);
			transform.localScale = Vector3.one * Random.Range (7f, 14f);
			if (layer2) {
				transform.localScale *= 0.2f;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (!(Global.Dataholder.CutMan.OopsIExplodedCutscene && Global.Dataholder.CutMan.OopsIExplodedTimer < 1f)) {
			transform.localEulerAngles += new Vector3 (0, speed * Time.deltaTime, 0);
		}
	}
}
