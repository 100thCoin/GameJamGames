using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour {


	public float Timer;

	public GameObject Dest;

	public float Speed;
	public float Amplitude;

	// Use this for initialization
	void Start () {
		Dest = GameObject.Find("Player").transform.Find("CrownTarget").gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Timer += Time.fixedDeltaTime;

		transform.localPosition = new Vector3(0,Mathf.Sin(Timer*Speed*Mathf.PI)*Amplitude,0);
		Vector3 tpos = transform.parent.transform.position;
		transform.parent.transform.position = new Vector3((tpos.x * 2 + Dest.transform.position.x) / 3f,(tpos.y * 2 + Dest.transform.position.y) / 3f,0);

	}
}
