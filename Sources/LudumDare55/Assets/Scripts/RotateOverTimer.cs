using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTimer : MonoBehaviour {

	public float Timer;
	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime * speed;
		transform.localEulerAngles = new Vector3 (0, 0, Timer);
	}
}
