using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

	public float Delay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Delay -= Time.deltaTime;
		if (Delay < 0) {
			Destroy (gameObject);
		}
	}
}
