using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPrefab : MonoBehaviour {

	public float Timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Timer += Time.deltaTime*4;
		transform.localScale = Vector3.one * (1 - Timer*Timer*Timer);
		if (Timer > 1) {
			Destroy (gameObject);
		}
	}
}
