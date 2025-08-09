using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mustache : MonoBehaviour {

	public Transform[] Trail;
	public Vector3[] PrevPoses;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		for (int i = PrevPoses.Length-1; i >= 0; i--) {
			if (i == 0) {
				PrevPoses [i] = transform.position;
			} else {
				PrevPoses [i] = PrevPoses [i - 1];
			}
		}
		for (int i = 0; i < Trail.Length; i++) {
			Trail [i].transform.position = (PrevPoses [i]);
		}
	}
}
