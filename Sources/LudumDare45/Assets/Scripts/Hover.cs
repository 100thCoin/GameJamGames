using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

	public float Timer;
	public float Amplitude;
	public float Speed;

	public Vector3 SPos;

	// Use this for initialization
	void Start () {
		SPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		Timer += Time.deltaTime;
		transform.position = new Vector3(SPos.x,SPos.y + Mathf.Sin(Timer * Speed * Mathf.PI) * Amplitude,SPos.z);


	}
}
