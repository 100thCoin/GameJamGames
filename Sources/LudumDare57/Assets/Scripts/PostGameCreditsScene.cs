using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostGameCreditsScene : MonoBehaviour {

	public TextMesh TM;
	public float timer;

	// Use this for initialization
	void Start () {

		TM.text = "Executive Producer        Chris Siebert\n\nCast List\n\n         Everyone           Chris Siebert\n\n\n\nVICTORY!!!\n\nMoon reconstructed: " + Mathf.Floor(Global.Dataholder.ReconstructedPercent*100) + "%\n\nSpeedrun Time: " + DataHolder.StringifyTime(Global.Dataholder.SpeedrunTime) + "\n\n\nPress Escape to\nreturn to the title.";



	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime*5;
		if (timer > 22) {
			timer = 22;
		}
		transform.localPosition = new Vector3 (0, -9 + timer,-1);
		if (timer >= 18) {

			if (Input.GetKeyDown (KeyCode.Escape)) {

				Super.Dataholder.ReturnToTitle ();

			}

		}

	}
}
