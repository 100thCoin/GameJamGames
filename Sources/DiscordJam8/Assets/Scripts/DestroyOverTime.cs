using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

	public float delay;

	public bool PauseIfPause;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!PauseIfPause || (PauseIfPause && !Global.DataHolder.GameIsPaused)) {
			delay -= Time.deltaTime;
			if (delay <= 0) {
				Destroy (gameObject);
			}
		}
	}
}
