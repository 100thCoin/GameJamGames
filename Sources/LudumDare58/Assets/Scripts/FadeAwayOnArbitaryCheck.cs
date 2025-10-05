using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAwayOnArbitaryCheck : MonoBehaviour {

	public SpriteRenderer Interact;
	public int ID;
	public float timer;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
		if (Global.Dataholder.ArbitraryChecks [ID]) {
			if (Global.Dataholder.CurrentVoiceClip == null) {
				timer+= Time.deltaTime;
			}
			Interact.color = new Vector4 (1, 1, 1, 1 - timer);
		}
	}
}
