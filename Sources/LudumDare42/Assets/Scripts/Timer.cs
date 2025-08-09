using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	public float Timerr;


	// Use this for initialization
	void Start () {
		gameObject.transform.parent = gameObject.transform.parent.parent;
	}
	
	// Update is called once per frame
	void Update () {
		Timerr += Time.deltaTime;
	}
}
