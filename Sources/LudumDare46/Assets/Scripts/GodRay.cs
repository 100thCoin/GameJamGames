using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodRay : MonoBehaviour {

	public SpriteRenderer SR;
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
		SR.color = new Vector4(SR.color.r,SR.color.g,SR.color.b,Mathf.Sin(Timer * Speed) *0.15f + Mathf.Sin(Timer * Speed * 2.77f) *0.05f + Base);

	}
}
