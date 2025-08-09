using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!Global.DataHolder.GameIsPaused) {
			transform.localEulerAngles = new Vector3 (0, 0, transform.localEulerAngles.z + speed * Time.deltaTime);
		}
	}
}
