using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomper : MonoBehaviour {

	public bool Ded;

	public bool DedGap;


	public float timer;



	// Use this for initialization
	void Start () {
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
		timer = timer - Time.deltaTime;

		if (timer <= 0) {
			if (Ded && !DedGap) {DedGap = true;}
			if (!Ded&&!DedGap) {Ded = true;timer = timer + 0.6f;}
			if (Ded && DedGap) {Ded = false;DedGap = false;timer = timer+  2.4f;}
		}

		if (Ded) {

			gameObject.tag = "SawBlade";

		} else {

			gameObject.tag = "Untagged";


		}

	}
}
