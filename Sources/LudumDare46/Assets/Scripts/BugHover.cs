using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugHover : MonoBehaviour {

	public float Timer;
	public float Speed;

	public float Base;

	// Use this for initialization
	void Start () {
		Timer = Random.Range(-10000.0f,10000.0f);
		Speed = Random.Range(0.8f,1.4f);
	}

	// Update is called once per frame
	void Update () {

		Timer += Time.deltaTime;
		transform.localPosition= new Vector3(0,Mathf.Sin(Timer * Speed) *0.15f + Mathf.Sin(Timer * Speed * 2.77f) *0.08f + Mathf.Sin(Timer * Speed * 4.21f) *0.03f + Base,0);
	}
}
