using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoughEvilShake : MonoBehaviour {

	public float Timer;
	public Vector3 SPos;
	// Use this for initialization
	void Start () {
		SPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		Timer -= Time.deltaTime*2;
		if (Timer > 0) {
			transform.localPosition = SPos + new Vector3 (Random.Range (-Timer, Timer), Random.Range (-Timer, Timer), 0);
		} else {
			transform.localPosition = SPos;
		}

	}
}
