using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloat : MonoBehaviour {

	public float Timer;
	public float Amplitude;
	public float Speed;

	public Vector3 Pivot;

	// Use this for initialization
	void Start () {
		Pivot = transform.position;	
	}
	
	// Update is called once per frame
	void Update () {

		Timer += Time.deltaTime * Speed;
		transform.position = new Vector3(Pivot.x,Pivot.y + (Mathf.Cos(Timer)-1)*Amplitude,Pivot.z);

	}
}
