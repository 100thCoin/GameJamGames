using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTimer : MonoBehaviour {

	public float Duration;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Duration-=Time.deltaTime;
		if (Duration < 0) {
			Destroy (gameObject);
		}
	}
}
